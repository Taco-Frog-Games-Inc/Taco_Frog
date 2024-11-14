using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

/*
 * Source File Name: PlayerController.cs
 * Author Name: Alexander Maynard
 * Student Number: 301170707
 * Creation Date: October 2nd, 2024
 * 
 * Last Modified by: Audrey Bernier Larose
 * Last Modified Date: November 13th, 2024
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
 */

public class PlayerController : MonoBehaviour, IDamageTaker, IRewardTaker
{
    //variables needed for moving
    private CharacterController _characterController;
    private Vector2 _input;
    private Vector3 _direction;
    [SerializeField] private float speed;
    [SerializeField] private Transform _playerTransform;

    //variables needed for jumping
    private float _gravity = 9.81f;
    [SerializeField] float _player1JumpHeight = 10.0f; //for p1
    [SerializeField] float _player2JumpHeight = 6.0f; //for p2
    [SerializeField] private float _jumpHeight;
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
    [SerializeField] private float _player1TongueAttackpointDistance; //for p1
    [SerializeField] private float _player2TongueAttackpointDistance; //for p2
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

    public int Health { get { return health; } set { if (value > 0) health = value; } }

    /// <summary>
    /// Parts of the IDamageTaker
    /// Used along with the IDamager
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (Health < 0) health = 0;

        activeScreen.transform.GetChild(1).GetChild(health).gameObject.SetActive(false);
        if (Health == 0)
        {
            //make sure score is updated before death (scene call)
            SaveManager.Instance.UpdateCurrentScore();
            SaveManager.Instance.ResetTacoScore();
            SceneManager.LoadScene("LoseScreen");
        }
    }

    public int Score { get { return _score; } set { if (value > 0) _score = value; } }

    /// <summary>
    /// Parts of the IRewardTaker
    /// Used along with the IRewarder
    /// </summary>
    /// <param name="points"></param>
    public void IncreaseScore(int points, ItemTypeEnum itemType) { 
        _score += points;
        switch (itemType) {
            case ItemTypeEnum.Diamond:
                string diamondText = activeScreen.transform.GetChild(2).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text;                
                activeScreen.transform.GetChild(2).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = (int.Parse(diamondText) + points).ToString();
                break;
            case ItemTypeEnum.Coin:
                string coinText = activeScreen.transform.GetChild(3).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text;
                activeScreen.transform.GetChild(3).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = (int.Parse(coinText) + points).ToString();
                break;
        }        
    }

    /// <summary>
    /// OnAwake get the CharacterController Component
    /// </summary>
    private void Awake()
    {
        //set the p1 and p2 stat differences
        SetPlayerStats();
        _characterController = GetComponent<CharacterController>();

        //get the collider of the tongue attack
        _tongueAttackCollider = _tongueAttackpoint.GetComponent<CapsuleCollider>();

        //set the collider size and offset
        _tongueAttackCollider.height = -_tongueAttackpoint.transform.localPosition.x;
        _tongueAttackCollider.center = new Vector3(_tongueAttackCollider.height / 2, 0, 0);
    }       

    /// <summary>
    /// FixedUpdate does ground check and, runs, move, jump and updates player rotation.
    /// </summary>
    private void FixedUpdate()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _groundMask);

        _characterController.Move(_direction * speed * Time.deltaTime);

        Move();
        Jump();
        TongueAttack();
        UpdatePlayerRotation();

        if (_characterController.velocity.y > 0)
            _animator.SetBool("isJumping", true);
        else if(_characterController.velocity.y == 0)
            _animator.SetBool("isJumping", false);
    }

    /// <summary>
    /// makes sure that the player abilities are slightly different
    /// </summary>
    private void SetPlayerStats()
    { 
        //check if p1
        if (GameObject.FindGameObjectsWithTag("Player").Length == 1)
        {
            Debug.Log("Player 1 spawned");
            //set jump height.
            _jumpHeight = _player1JumpHeight;
            _tongueAttackpoint.transform.SetLocalPositionAndRotation(new Vector3(-_player1TongueAttackpointDistance, _tongueAttackpoint.transform.localPosition.y, _tongueAttackpoint.transform.localPosition.z), _tongueAttackpoint.transform.localRotation); //set attack point
            _tongueAttackLineRenderer.positionCount = 2;
            _tongueAttackLineRenderer.SetPosition(0, new Vector3(0, 0.45f, 0)); //first tongue attack position
            _tongueAttackLineRenderer.SetPosition(1, new Vector3(-_player1TongueAttackpointDistance, 0.45f, 0)); //second tongue attack position

            p1Head.SetActive(true);

        }
        else if (GameObject.FindGameObjectsWithTag("Player").Length == 2)
        {
            Debug.Log("Player 2 spawned");
            //set jump height.
            _jumpHeight = _player2JumpHeight;
            _tongueAttackpoint.transform.SetLocalPositionAndRotation(new Vector3(-_player2TongueAttackpointDistance, _tongueAttackpoint.transform.localPosition.y, _tongueAttackpoint.transform.localPosition.z), _tongueAttackpoint.transform.localRotation); //set attack point
            _tongueAttackLineRenderer.positionCount = 2;
            _tongueAttackLineRenderer.SetPosition(0, new Vector3(0, 0.45f, 0)); //first tongue attack position
            _tongueAttackLineRenderer.SetPosition(1, new Vector3(-_player2TongueAttackpointDistance, 0.45f, 0)); //second tongue attack position

            p2Head.SetActive(true);
        }
    }

    /// <summary>
    /// Moves the player based on the updated inputs x and y.
    /// </summary>
    private void Move()
    {
        _direction = new Vector3(_input.x, 0.0f, _input.y);

        if (_characterController.velocity.x == 0 && _characterController.velocity.z == 0)
        {
            _animator.SetBool("isWalking", false);
            //Debug.Log("Not moving");
        }
        else if (_characterController.velocity.x != 0 || _characterController.velocity.z != 0)
        {
            _animator.SetBool("isWalking", true);
            //Debug.Log("Moving");
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
        _playerTransform.rotation = Quaternion.LookRotation(new Vector3(_direction.z, _direction.y, _direction.x * -1));
    }
}