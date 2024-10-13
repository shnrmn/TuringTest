using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    [Header("Player Turn")]
    [SerializeField] private float turnSpeed;

    private PlayerInput _input;

    private void Start()
    {
        _input = PlayerInput.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        // Turning player
        transform.Rotate(_input.MouseX * Time.deltaTime * turnSpeed * Vector3.up);
    }
}
