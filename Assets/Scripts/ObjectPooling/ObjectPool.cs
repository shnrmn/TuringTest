using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private List<PooledObject> objectPool = new();
    [SerializeField] private List<PooledObject> usedPool = new();

    private PooledObject _tempObject;

    public GameObject objectToPool;
    public int startSize;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public PooledObject GetPooledObject()
    {
        PooledObject tempObject;

        if (objectPool.Count > 0)
        {
            tempObject = objectPool[0];
            objectPool.RemoveAt(0);
        }
        else
        {
            AddNewObject();
            tempObject = GetPooledObject();
        }

        tempObject.gameObject.SetActive(true);
        tempObject.ResetObject();

        return tempObject;
    }

    public void RestoreObject(PooledObject obj)
    {
        Debug.Log("Restoring object");
        obj.gameObject.SetActive(false);
        usedPool.Remove(obj);
        objectPool.Add(obj);
    }

    public void DestroyPooledObject(PooledObject obj, float time = 0)
    {
        if (time == 0)
        {
            obj.Destroy();
            return;
        }
        
        obj.Destroy(time);
    }

    private void Initialize()
    {
        for (int i = 0; i < startSize; i++)
        {
            AddNewObject();
        }
    }

    private void AddNewObject()
    {
        _tempObject = Instantiate(objectToPool, transform).GetComponent<PooledObject>();
        _tempObject.gameObject.transform.parent = null;
        _tempObject.gameObject.SetActive(false);
        _tempObject.SetObjectPool(this);
        objectPool.Add(_tempObject);
    }
}
