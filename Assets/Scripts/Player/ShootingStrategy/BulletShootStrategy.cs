using UnityEngine;

public class BulletShootStrategy : IShootStrategy
{
    public ShootInteractor _interactor;
    public Transform _shootPoint;

    public BulletShootStrategy(ShootInteractor interactor)
    {
        Debug.Log("Switched to Bullet Mode");
        _interactor = interactor;
        _shootPoint = interactor.GetShootPoint();

        _interactor.gunRenderer.material.color = _interactor.bulletGunColor;
    }

    public void Shoot()
    {
        float shootVelocity = _interactor.GetShootVelocity();
        Transform shootPoint = _interactor.GetShootPoint();

        PooledObject pooledBullet = _interactor.bulletPool.GetPooledObject();
        pooledBullet.gameObject.SetActive(true);

        Rigidbody bullet = pooledBullet.GetComponent<Rigidbody>();
        bullet.transform.SetPositionAndRotation(shootPoint.position, shootPoint.rotation);
        bullet.velocity = shootPoint.forward * shootVelocity;

        _interactor.bulletPool.DestroyPooledObject(pooledBullet, 5f);
    }
}
