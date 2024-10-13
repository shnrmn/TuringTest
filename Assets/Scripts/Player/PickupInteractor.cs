using UnityEngine;

public class PickupInteractor : Interactor
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask pickupLayer;
    [SerializeField] private float pickupDistance;
    [SerializeField] private Transform attachTransform;

    private bool _isPicked;
    private RaycastHit _raycastHit;
    private IPickable _pickable;

    public override void Interact()
    { 
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out _raycastHit, pickupDistance, pickupLayer))
        {
            _pickable = _raycastHit.transform.GetComponent<IPickable>();
            if (_pickable == null) return;

            if (_input.PickupPressed && !_isPicked)
            {
                _pickable.OnPickedUp(attachTransform);
                _isPicked = true;
                return;
            }
        }

        if (_input.PickupPressed && _pickable != null)
        {
            _pickable.OnDropped();
            _isPicked = false;
        }
    }
}
