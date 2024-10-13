using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] private GameObject _objectToBuild;
    [SerializeField] private Transform _placementPoint;

    public void Build()
    {
        Instantiate(_objectToBuild, _placementPoint.position, _placementPoint.rotation);
        Destroy(gameObject);
    }
}
