using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerJump : Interactor
{
    [Header("Jump")]
    [SerializeField] private float jumpVelocity;

    private PlayerMovement _playerMovement;

    public override void Interact()
    {
        if (_playerMovement == null)
            _playerMovement = GetComponent<PlayerMovement>();

        if (_input.JumpPressed && _playerMovement.IsGrounded)
        {
            _playerMovement.SetYVelocity(jumpVelocity);
        }
    }
}
