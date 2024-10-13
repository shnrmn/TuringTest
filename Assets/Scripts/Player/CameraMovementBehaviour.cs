using UnityEngine;

public class CameraMovementBehaviour : MonoBehaviour
{
    [Header("Player Turn")]
    [SerializeField] private float turnSpeed;
    [SerializeField] private bool invertMouse;

    private float _cameraRotationX;
    private PlayerInput _input;

    // Start is called before the first frame update
    void Start()
    {
        _input = PlayerInput.Instance;

        // Hides mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        // Camera up and down
        _cameraRotationX += _input.MouseY * Time.deltaTime * turnSpeed * (invertMouse ? 1 : -1);
        _cameraRotationX = Mathf.Clamp(_cameraRotationX, -85f, 85f);

        transform.localRotation = Quaternion.Euler(_cameraRotationX, 0, 0);
    }
}
