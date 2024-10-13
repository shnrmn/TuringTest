using UnityEngine;

public class PickupObject : MonoBehaviour, IPickable
{
    private FixedJoint _joint;
    private Rigidbody _objectRb;

    // Start is called before the first frame update
    void Start()
    {
        _objectRb = GetComponent<Rigidbody>();
    }

    public void OnPickedUp(Transform attachedTransform)
    {
        transform.SetPositionAndRotation(attachedTransform.position, attachedTransform.rotation);
        transform.SetParent(attachedTransform);

        _objectRb.isKinematic = true;
        _objectRb.useGravity = false;
    }

    public void OnDropped()
    {
        Destroy(_joint);
        _objectRb.isKinematic = false;
        _objectRb.useGravity = true;
        transform.SetParent(null);
    }
}
