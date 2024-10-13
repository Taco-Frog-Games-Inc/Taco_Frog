using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Source File Name: PlayerController.cs
 * Author Name: Alexander Maynard
 * Student Number: 301170707
 * Creation Date: October 2nd, 2024
 * 
 * Last Modified by: Alexander Maynard
 * Last Modified Date: October 2nd, 2024
 * 
 * 
 * Program Description: 
 *      
 *      This script handles the player inputs and translates that to in game functions for the playeble character.
 * 
 * Revision History:
 *      -> October 2nd, 2024:
 *          -Created this script and fully implemented it.
 */
public class PlayerController : MonoBehaviour, IDamageTaker
{
    //variables needed for moving
    private CharacterController _characterController;
    private Vector2 _input;
    private Vector3 _direction;
    [SerializeField] private float speed;
    [SerializeField] private Transform _playerTransform;

    //variables needed for jumping
    private float _gravity = 9.81f;
    [SerializeField] float _jumpHeight = 4.0f;
    [SerializeField] Vector3 _velocity;
    [SerializeField] private bool _isJumpPressed;
    [SerializeField] Transform _groundCheck;
    [SerializeField] float _groundCheckRadius = 0.7f;
    [SerializeField] LayerMask _groundMask;
    [SerializeField] bool _isGrounded;

    public int Health { get; set; }
    public void TakeDamage(int damage)
    {
        Debug.Log("Player is taking damage - Ouch! " + damage);
    }
    /// <summary>
    /// OnAwake get the CharacterController Component
    /// </summary>

    //PlayerAttackSystem _plAttkSys;
    //[SerializeField] private GameObject _playerLongRgAtt; // Prefab for the tongue
    //private TongueAttack tongueAttack;
    //private GameObject _enemy; //Removed [SerializedField]


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        //_enemy = GameObject.FindWithTag("Enemy");

        //_plAttkSys = new PlayerAttackSystem(_enemy, this.gameObject);
        //tongueAttack = new TongueAttack();
        //tongueAttack.Initialize(_playerLongRgAtt, transform);
    }

     void Update()
    {
       
        //tongueAttack.Update();

        //new input system needed here....
        //if (Input.GetKeyDown(KeyCode.Space)) 
        //{
            //tongueAttack.ExecuteAttack(); 
        //}
    }
    /// <summary>
    /// FixedUpdate does ground check and, runs, move, jump and updates player rotation.
    /// </summary>
    private void FixedUpdate()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _groundMask);

       //Debug.Log(_groundMask.value);

        _characterController.Move(_direction * speed * Time.deltaTime);
        Move();
        Jump();
        UpdatePlayerRotation();
        //_plAttkSys.JumpAttack();
    }

    /// <summary>
    /// Moves the player based on the updated inputs x and y.
    /// </summary>
    private void Move()
    {
        _direction = new Vector3(_input.x, 0.0f, _input.y);
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
    /// Updates the player rotation based on which direction the WASD keys are taking the player
    /// </summary>
    private void UpdatePlayerRotation()
    {
        if (_input.sqrMagnitude == 0) return; //check to make sure that the player doesn't reset their rotation

        //updates the player rotation. the Vector3 passsed had to be adjusted since the x, y and z and different that the actual player WASD directional inputs translation.
        _playerTransform.rotation = Quaternion.LookRotation(new Vector3(_direction.z, _direction.y, _direction.x * -1));
    }

}