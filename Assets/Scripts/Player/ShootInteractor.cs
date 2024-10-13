using UnityEngine;

public class ShootInteractor : Interactor
{
    [SerializeField] private InputType _inputType;

    [Header("Gun")]
    public MeshRenderer gunRenderer;
    public Color bulletGunColor;
    public Color rocketGunColor;

    [Header("Shooting")]
    public ObjectPool bulletPool;
    public ObjectPool rocketPool;
    [SerializeField] private float shootVelocity;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private PlayerMovement playerMovement;

    private float _finalShootVelocity;
    private IShootStrategy _currentShootStrategy;

    public enum InputType
    {
        Primary,
        Secondary
    }

    public override void Interact()
    {
        _currentShootStrategy ??= new BulletShootStrategy(this);

        if (_input.WeaponOnePressed)
            _currentShootStrategy = new BulletShootStrategy(this);
        else if (_input.WeaponTwoPressed)
            _currentShootStrategy = new RocketShootStrategy(this);

        if (_input.PrimaryShootPressed)
            _currentShootStrategy.Shoot();
    }

    public float GetShootVelocity()
    {
        _finalShootVelocity = playerMovement.GetForwardSpeed() + shootVelocity;
        return _finalShootVelocity;
    }

    public Transform GetShootPoint()
    {
        return shootPoint;
    }
}
