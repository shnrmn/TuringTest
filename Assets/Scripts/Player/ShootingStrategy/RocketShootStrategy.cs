using UnityEngine;

public class RocketShootStrategy : IShootStrategy
{
    public ShootInteractor _interactor;
    public Transform _shootPoint;

    public RocketShootStrategy(ShootInteractor interactor)
    {
        Debug.Log("Switched to Rocket Mode");
        _interactor = interactor;
        _shootPoint = interactor.GetShootPoint();

        _interactor.gunRenderer.material.color = _interactor.rocketGunColor;
    }

    public void Shoot()
    {
        float shootVelocity = _interactor.GetShootVelocity();
        Transform shootPoint = _interactor.GetShootPoint();

        PooledObject pooledRocket = _interactor.rocketPool.GetPooledObject();
        pooledRocket.gameObject.SetActive(true);

        Rigidbody rocket = pooledRocket.GetComponent<Rigidbody>();
        rocket.transform.SetPositionAndRotation(shootPoint.position, shootPoint.rotation);
        rocket.velocity = shootPoint.forward * shootVelocity;

        _interactor.rocketPool.DestroyPooledObject(pooledRocket, 5f);
    }
}
