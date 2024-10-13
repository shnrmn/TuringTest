using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintMultiplier;
    [SerializeField] private float turnSpeed;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private bool invertMouse;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float gravity = -9.81f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundCheckDistance;

    [Header("Shooting")]
    [SerializeField] private Rigidbody bulletPrefab;
    [SerializeField] private float shootForce;
    [SerializeField] private Transform shootPoint;

    [Header("Interactable")]
    [SerializeField] private LayerMask buttonLayer;
    [SerializeField] private float interactDistance;

    [Header("Pick and Drop")]
    [SerializeField] private LayerMask pickableLayer;
    [SerializeField] private float pickableDistance = 5f;
    [SerializeField] private Transform pickableAttachPoint;

    private CharacterController _characterController;
    private float _horizontal, _vertical;
    private float _mouseX, _mouseY;
    private float _moveMultiplier;
    private float _cameraRotationX;
    private Vector3 _playerVelocity;
    private bool _isGrounded;
    private RaycastHit _raycastHit;
    private ISelectable _selectable;
    private Camera _camera;
    private bool _hasPickable;
    private IPickable _pickable;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _characterController = GetComponent<CharacterController>();

        // Hides mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        MovePlayer();
        RotatePlayer();
        GroundCheck();
        JumpCheck();
        ShootBullet();
        Interact();
        PickAndDrop();
    }

    private void GetInput()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");

        _moveMultiplier = Input.GetButton("Fire3") ? sprintMultiplier : 1;
    }

    private void MovePlayer()
    {
        _characterController.Move(_moveMultiplier * moveSpeed * Time.deltaTime * (transform.forward * _vertical + transform.right * _horizontal));

        if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2f;
        }

        _playerVelocity.y += gravity * Time.deltaTime;

        _characterController.Move(_playerVelocity * Time.deltaTime);
    }

    private void RotatePlayer()
    {
        // Turning player
        transform.Rotate(_mouseX * Time.deltaTime * turnSpeed * Vector3.up);

        // Camera up and down
        _cameraRotationX += _mouseY * Time.deltaTime * turnSpeed * (invertMouse ? 1 : -1);
        _cameraRotationX = Mathf.Clamp(_cameraRotationX, -85f, 85f);

        cameraTransform.localRotation = Quaternion.Euler(_cameraRotationX, 0, 0);
    }

    private void GroundCheck()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);
    }

    private void JumpCheck()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _playerVelocity.y = jumpVelocity;
        }
    }

    private void ShootBullet()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
            bullet.velocity = cameraTransform.forward * shootForce;
            Destroy(bullet.gameObject, 5f);
        }
    }

    private void Interact()
    {
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out _raycastHit, interactDistance, buttonLayer))
        {
            if (_raycastHit.transform.TryGetComponent(out _selectable))
            {
                _selectable.OnHoverEnter();

                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E))
                {
                    _selectable.OnSelect();
                }
            }
        }

        if (_raycastHit.transform == null && _selectable != null)
        {
            _selectable.OnHoverExit();
            _selectable = null;
        }
    }

    private void PickAndDrop()
    {
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out _raycastHit, pickableDistance, pickableLayer))
        {
            if (_raycastHit.transform.TryGetComponent(out _pickable))
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E))
                {
                    if (_hasPickable)
                    {
                        _pickable.OnDropped();
                        _hasPickable = false;
                    }
                    else
                    {
                        _pickable.OnPickedUp(pickableAttachPoint);
                        _hasPickable = true;
                    }
                }
            }
        }
    }
}
