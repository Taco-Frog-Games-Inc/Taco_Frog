using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

/*
 * Source File Name: PlayerController.cs
 * Author Name: Alexander Maynard
 * Student Number: 301170707
 * Creation Date: October 2nd, 2024
 * 
 * Last Modified by: Audrey Bernier Larose
 * Last Modified Date: December 6th, 2024
 * 
 * Program Description: 
 *      
 *      This script handles the player inputs and translates that to in game functions for the playeble character.
 * 
 * Revision History:
 *      -> October 2nd, 2024:
 *          -Created this script and fully implemented it.
 *      -> October 14th, 2024:
 *          - Implemented the IRewardTaker & IDamageTaker    
 *      -> October 15th, 2024:
 *          -Added tongue attack functionality
 *          -Added p1 and p2 player ability differences.
 *      -> November 7th, 2024:
 *          -Added player 1 and 2 designs. Modified code as needed to accomodate this.
 *          -Also added animation transitions to this scrpt.
 *      -> November 10th, 2024:
 *          -Added UI functionality based on the activeScreen variable.
 *      -> November 13th, 2024:
 *          -Adapted to reset TacoScore
 *      -> November 24th, 2024:
 *          -Adjusted for minimap functionality.
 *      -> November 30th. 2024:
 *          -Added scene change fade.
 *          -Added small delay before death for paticles to take effect (if needed).
 *      -> December 2nd, 2024:
 *          -Disabled player input when health reaches 0.
 *      -> December 4th, 2024:
 *          -Adjusted for minimap UI on player 2
 *      -> December 6th, 2024:
 *          -Added pickup sound
 */

public class PlayerController : MonoBehaviour, IDamageTaker, IRewardTaker, IAbilityTaker
{

    //Scriptable player data
    public List<PlayerData> playerDataList;
    public PlayerData playerData;

    [SerializeField] private Camera _cam;
    //variables needed for moving
    private CharacterController _characterController;
    private Vector2 _input;
    private Vector3 _direction;
    private Vector3 _relativeDirection;
    [SerializeField] private float speed;
    [SerializeField] private Transform _playerTransform;

    //variables needed for jumping
    private float _gravity = 9.81f;
    private float _jumpHeight;
    [SerializeField] public Vector3 _velocity;
    [SerializeField] private bool _isJumpPressed;
    [SerializeField] Transform _groundCheck;
    [SerializeField] float _groundCheckRadius = 0.7f;
    [SerializeField] LayerMask _groundMask;
    [SerializeField] bool _isGrounded;

    //variables for tongue attack
    private bool _canAttack = true;
    private bool _isAttacking = false;
    [SerializeField] private LineRenderer _tongueAttackLineRenderer;
    [SerializeField] private GameObject _tongueAttackpoint;
    private CapsuleCollider _tongueAttackCollider;

    [SerializeField] protected internal int health = 3;
    private int _score = 0;

    public GameObject activeScreen;
    //player head to be chosen
    [SerializeField] private GameObject p1Head;
    [SerializeField] private GameObject p2Head;

    //player animator for animations
    [SerializeField] private Animator _animator;

    [Header("UI Related")]
    //public RenderTexture minimapTexture;
    [SerializeField] private Sprite player2MM;


    //player Input refrence
    [SerializeField] private PlayerInput _playerInput;

    public int Health { get { return health; } set { if (value > 0) health = value; } }

    //to ger reference to original jump height helps in reseting height after using ability
    private float origJumpHeight; 
    private AudioManager _playerAudio;
    InvincibilityUI _invUI;
    /// <summary>
    /// Parts of the IDamageTaker
    /// Used along with the IDamager
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        //if ability is active player will not get damage if deactivated player will receive damage
        if(_invUI.transform.GetChild(0).gameObject.activeSelf)        
            _invUI.ReduceSliderValue(damage);
        
        else if(!_invUI.transform.GetChild(0).gameObject.activeSelf) {
             health -= damage;
             _playerAudio.PlayGettingDamage();
             if (Health < 0) health = 0;

             activeScreen.transform.GetChild(1).GetChild(health).gameObject.SetActive(false);
            if (Health == 0)
                Invoke("LoadSceneAfterDeath", 1f);            
        }
    }

    /// <summary>
    /// To allow delays for particles to take effect
    /// </summary>
    public void LoadSceneAfterDeath()
    {
        //make sure score is updated before death (scene call)
        SaveManager.Instance.UpdateCurrentScore();
        SaveManager.Instance.ResetTacoScore();
        SceneChangeFade.Instance.AddSceneFade(); //scene transition fade
        SceneManager.LoadScene("LoseScreen");
    }

    public int Score { get { return _score; } set { if (value > 0) _score = value; } }

    /// <summary>
    /// Parts of the IRewardTaker
    /// Used along with the IRewarder
    /// </summary>
    /// <param name="points"></param>
    public void IncreaseScore(int points, ItemTypeEnum itemType) { 
        _score += points;
        playerData.moneyValue += points;
        switch (itemType) {
            case ItemTypeEnum.Diamond:
                string diamondText = activeScreen.transform.GetChild(2).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text;                
                activeScreen.transform.GetChild(2).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = (int.Parse(diamondText) + points).ToString();
                _playerAudio.PlayPickUpSound();
                break;
            case ItemTypeEnum.Coin:
                string coinText = activeScreen.transform.GetChild(3).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text;
                activeScreen.transform.GetChild(3).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = (int.Parse(coinText) + points).ToString();
                _playerAudio.PlayPickUpSound();
                break;
        }        
    }

    public void InitPlayer() {
        SetPlayerData();

        //set the p1 and p2 stat differences
        SetPlayerStats();
        _characterController = GetComponent<CharacterController>();

        //get the collider of the tongue attack
        _tongueAttackCollider = _tongueAttackpoint.GetComponent<CapsuleCollider>();

        //set the collider size and offset
        _tongueAttackCollider.height = -_tongueAttackpoint.transform.localPosition.x;
        _tongueAttackCollider.center = new Vector3(_tongueAttackCollider.height / 2, 0, 0);
        origJumpHeight = _jumpHeight;
        _invUI = GameObject.FindWithTag("PowerUp").GetComponent<InvincibilityUI>();
        _playerAudio = GetComponent<AudioManager>();
    }

    private void SetPlayerData() {
        foreach (PlayerData data in playerDataList) {
            if (data.name == gameObject.transform.parent.name) {
                playerData = data;
                return;
            }            
        }
    }

    private void SetPlayerMMIcon() {
        gameObject.transform.GetChild(4).GetComponent<SpriteRenderer>().sprite = player2MM;        
    }

    /// <summary>
    /// Sets up the proper camera texture for player 2's minimap.
    /// </summary>
    private void Start() {       
        if (gameObject.transform.parent.name == "Player2") {
            GameObject cam = gameObject.transform.parent.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
            cam.GetComponent<Camera>().targetTexture = playerData.minimapTexture;
            SetPlayerMMIcon();
        }
    }

    /// <summary>
    /// FixedUpdate does ground check and, runs, move, jump and updates player rotation.
    /// </summary>
    private void FixedUpdate()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _groundMask);

        _characterController.Move(_relativeDirection * speed * Time.deltaTime);

        Move();
        Jump();
        TongueAttack();
        UpdatePlayerRotation();

        if (_characterController.velocity.y > 0) _animator.SetBool("isJumping", true);
        else if(_characterController.velocity.y == 0) _animator.SetBool("isJumping", false);
    }

    /// <summary>
    /// makes sure that the player abilities are slightly different
    /// </summary>
    public void SetPlayerStats() {
        //set jump height.
        _jumpHeight = playerData.jumpHeight;
        _tongueAttackpoint.transform.SetLocalPositionAndRotation(new Vector3(-playerData.tongueAttackLength, _tongueAttackpoint.transform.localPosition.y, _tongueAttackpoint.transform.localPosition.z), _tongueAttackpoint.transform.localRotation); //set attack point
        _tongueAttackLineRenderer.positionCount = 2;
        _tongueAttackLineRenderer.SetPosition(0, new Vector3(0, 0.45f, 0)); //first tongue attack position
        _tongueAttackLineRenderer.SetPosition(1, new Vector3(-playerData.tongueAttackLength, 0.45f, 0)); //second tongue attack position
        
        //check if p1
        if (GameObject.FindGameObjectsWithTag("Player").Length == 1) p1Head.SetActive(true);
        
        else if (GameObject.FindGameObjectsWithTag("Player").Length == 2) p2Head.SetActive(true);        
    }

    /// <summary>
    /// Moves the player based on the updated inputs x and y.
    /// </summary>
    private void Move()
    {
        _direction = new Vector3(_input.x, 0.0f, _input.y).normalized;

        // get cam forward and right
        Vector3 cameraForward = _cam.transform.forward; 
        Vector3 cameraRight = _cam.transform.right;
        // set y values of the Vector3 so that theya re ignored
        cameraForward.y = 0;
        cameraRight.y = 0; 

        // set the relative redirection based on the camera's orientation (camera forward and right)
        _relativeDirection = _direction.x * cameraRight + _direction.z * cameraForward; 

        if (_characterController.velocity.x == 0 && _characterController.velocity.z == 0)
        {
            _animator.SetBool("isWalking", false);
        }
        else if (_characterController.velocity.x != 0 || _characterController.velocity.z != 0)
        {
            _animator.SetBool("isWalking", true);
        }
    }

    /// <summary>
    /// This method is called once the player presses WASD (defined in the new input system controls for 'Player1' or 'Player2'
    /// </summary>
    /// <param name="inputValue">Is the inout values passed by the controls/player input system</param>
    private void OnMove(InputValue inputValue)
    {
        //read the input values and update _inputs so they can be used.
        _input = inputValue.Get<Vector2>();
    }

    /// <summary>
    /// Handles jumping logic for the player.
    /// </summary>
    private void Jump()
    {
        //groundcheck
        if (_isGrounded)
        {
            //set velocity to 0 if player is grounded
            this._velocity.y = 0.0f;
        }

        //if grounded and the jumpp button is pressed...
        if (_isJumpPressed && _isGrounded)
        {
            //...make the player jump and set isJumpPressed to false (gravity is positive so the
            //player will 'jump' to a certain height.
            _velocity.y += Mathf.Sqrt(_jumpHeight * _gravity);
             _playerAudio.PlayJumping();
            if(_jumpHeight > origJumpHeight) _jumpHeight = origJumpHeight; //resets the jump height
            _isJumpPressed = false;
        }
        _velocity.y -= _gravity * Time.deltaTime; //otherwise make sure that gravity is negative
        _characterController.Move(_velocity * Time.deltaTime); //make sure the player can still move using their velocity
    }

    /// <summary>
    /// This method is called once the player presses the jump key(s) (defined in the new input system controls for 'Player1' or 'Player2'
    /// </summary>
    private void OnJump()
    {
        //if the character controller y velocity is nothing then set is jump pressed to true
        if (_characterController.velocity.y == 0)
        {
            _isJumpPressed = true;
        }
        //otherwise it should be false
        else
        {
            _isJumpPressed = false;
        }
    }

    /// <summary>
    /// Calls the proper attacking logic when the checks pass
    /// </summary>
    private void TongueAttack()
    {
        if(_isAttacking == true && _canAttack == true)
        {
            _canAttack = false;
            EnableTongueAttack(); //enable the tongue attack
            _playerAudio.PlayAttack();
            Invoke(nameof(DisableTongueAttack), 0.5f); //call disable on a delay
        }
    }

    /// <summary>
    /// When input is received, modify the checks
    /// </summary>
    private void OnTongueAttack()
    {
        if (_canAttack == true)
        {
            _isAttacking = true;
        }
        else
        {
            _isAttacking = false;
        }
    }

    /// <summary>
    /// Enables the tongue attack
    /// </summary>
    private void EnableTongueAttack()
    {
        _tongueAttackLineRenderer.enabled = true; //make the tongue line renderer disappear
        _tongueAttackCollider.enabled = true; //enable the collider
    }

    /// <summary>
    /// Disables the tongue attack
    /// </summary>
    private void DisableTongueAttack()
    {
        _tongueAttackLineRenderer.enabled = false; //make the tongue line renderer disappear
        _tongueAttackCollider.enabled = false; //disable the collider
        //reassign checks once the attack is over
        _canAttack = true; 
        _isAttacking = false;
    }

    /// <summary>
    /// Updates the player rotation based on which direction the WASD keys are taking the player
    /// </summary>
    private void UpdatePlayerRotation()
    {
        if (_input.sqrMagnitude == 0) return; //check to make sure that the player doesn't reset their rotation

        //updates the player rotation. the Vector3 passsed had to be adjusted since the x, y and z and different that the actual player WASD directional inputs translation.
        _playerTransform.rotation = Quaternion.LookRotation(new Vector3(_relativeDirection.z, _relativeDirection.y, _relativeDirection.x * -1));
    }

    /// <summary>
    /// IAbility taker interface attached, it helps in manupulating jump height when jump enhancer is acquired please let it stay public
    /// </summary>
    /// <returns></returns>
    public float GetJumpHeight() => _jumpHeight;
   
    public void SetHeight(float j) => _jumpHeight = j;
    public void PlayJumpEnhanceSound() => _playerAudio.PlayGetJumpEnhance();
    public void PlayDrownDeath()=> _playerAudio.PlayDrownDeath();
}