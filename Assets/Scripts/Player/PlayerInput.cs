using UnityEngine;

// Execute this before all other scripts
[DefaultExecutionOrder(-100)]
public class PlayerInput : MonoBehaviour
{
    private bool _clear;

    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
    public float MouseX { get; private set; }
    public float MouseY { get; private set; }
    public bool SprintHeld { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool InteractPressed { get; private set; }
    public bool PickupPressed { get; private set; }
    public bool PrimaryShootPressed { get; private set; }
    public bool SecondaryShootPressed { get; private set; }
    public bool WeaponOnePressed { get; private set; }
    public bool WeaponTwoPressed { get; private set; }
    public bool CommandPressed { get; private set; }

    /// <summary>
    /// Singleton pattern
    /// </summary>
    public static PlayerInput Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        ClearInput();
        ProcessInput();
    }

    private void FixedUpdate()
    {
        _clear = true;
    }

    private void ProcessInput()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        MouseY = Input.GetAxis("Mouse Y");
        MouseX = Input.GetAxis("Mouse X");

        SprintHeld = SprintHeld || Input.GetButton("Fire3");
        JumpPressed = JumpPressed || Input.GetButtonDown("Jump");
        InteractPressed = InteractPressed || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return);
        PickupPressed = PickupPressed || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.RightControl);

        PrimaryShootPressed = PrimaryShootPressed || Input.GetButtonDown("Fire1");
        SecondaryShootPressed = SecondaryShootPressed || Input.GetButtonDown("Fire2");

        WeaponOnePressed = WeaponOnePressed || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1);
        WeaponTwoPressed = WeaponTwoPressed || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2);

        CommandPressed = CommandPressed || Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.RightControl);
    }

    public void ClearInput()
    {
        if (!_clear) return;

        Horizontal = 0;
        Vertical = 0;
        MouseX = 0;
        MouseY = 0;

        SprintHeld = false;
        JumpPressed = false;
        InteractPressed = false;
        PickupPressed = false;

        PrimaryShootPressed = false;
        SecondaryShootPressed = false;

        WeaponOnePressed = false;
        WeaponTwoPressed = false;

        CommandPressed = false;
    }
}
