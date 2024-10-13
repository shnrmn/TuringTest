using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintMultiplier;
    [SerializeField] private float gravity = -9.81f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundCheckDistance;

    private CharacterController _characterController;
    private Vector3 _playerVelocity;
    private float _moveMultiplier = 1f;
    private PlayerInput _input;

    public bool IsGrounded { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        _input = PlayerInput.Instance;
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        MovePlayer();
    }

    public void SetYVelocity(float velocity)
    {
        _playerVelocity.y = velocity;
    }

    public float GetForwardSpeed()
    {
        return _input.Vertical * moveSpeed * _moveMultiplier;
    }

    private void MovePlayer()
    {
        _moveMultiplier = _input.SprintHeld ? sprintMultiplier : 1;

        _characterController.Move(_moveMultiplier * moveSpeed * Time.deltaTime * (transform.forward * _input.Vertical + transform.right * _input.Horizontal));

        if (IsGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2f;
        }

        _playerVelocity.y += gravity * Time.deltaTime;

        _characterController.Move(_playerVelocity * Time.deltaTime);
    }

    private void GroundCheck()
    {
        IsGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);
    }
}
