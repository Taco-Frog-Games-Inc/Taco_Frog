using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //variables needed for moving
    private CharacterController _characterController;
    private Vector2 _input;
    private Vector3 _direction;
    [SerializeField] private float speed;
    [SerializeField] private Transform _playerTransform;

    //needed for jumping
    private float _gravity = 9.81f;
    [SerializeField] float _jumpHeight = 4.0f;
    [SerializeField] Vector3 _velocity;
    [SerializeField] private bool _isJumpPressed;
    
    [SerializeField] Transform _groundCheck;
    [SerializeField] float _groundCheckRadius = 0.5f;
    [SerializeField] LayerMask _groundMask;
    [SerializeField] bool _isGrounded;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _groundMask);
        
        _characterController.Move(_direction * speed * Time.deltaTime);
        Move();
        Jump();
        UpdatePlayerRotation();
    }

    public void Move()
    {
        _direction = new Vector3(_input.x, 0.0f, _input.y);
    }

    public void OnMove(InputValue inputValue)
    {
        _input = inputValue.Get<Vector2>();
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _velocity.y = 0.0f;
        }

        if (_isJumpPressed && _isGrounded)
        {
            _velocity.y += Mathf.Sqrt(_jumpHeight * _gravity);
            _isJumpPressed = false;
        }

        _velocity.y -= _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }

    public void OnJump()
    {
        if (_characterController.velocity.y == 0)
        {
            _isJumpPressed = true;
        }
        else
        {
            _isJumpPressed = false;
        }
    }

    private void UpdatePlayerRotation()
    {
        if (_input.sqrMagnitude == 0) return;

        _playerTransform.rotation = Quaternion.LookRotation(new Vector3(_direction.z, _direction.y, _direction.x * -1));
    }
}