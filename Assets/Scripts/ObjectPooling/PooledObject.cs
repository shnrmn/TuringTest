using UnityEngine;
using UnityEngine.Events;

public class PooledObject : MonoBehaviour
{
    [SerializeField] private UnityEvent OnReset;

    private ObjectPool _associatedPool;
    private float _timer;
    private bool _setToDestroy;
    private float _destroyTime;

    // Update is called once per frame
    void Update()
    {
        if (_setToDestroy)
        {
            _timer += Time.deltaTime;

            if (_timer >= _destroyTime)
            {
                _setToDestroy = false;
                _timer = 0;
                Destroy();
            }
        }
    }

    public void SetObjectPool(ObjectPool pool)
    {
        _associatedPool = pool;
        _timer = 0;
        _destroyTime = 0;
        _setToDestroy = false;
    }

    public void ResetObject()
    {
        OnReset?.Invoke();
    }

    public void Destroy()
    {
        if (_associatedPool != null)
        {
            _associatedPool.RestoreObject(this);
        }
    }

    public void Destroy(float time)
    {
        _setToDestroy = true;
        _destroyTime = time;
    }
}
