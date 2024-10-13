using UnityEngine;

public class SimpleInteractor : Interactor
{
    [Header("Interact")]
    [SerializeField] private Camera cam;
    [SerializeField] private float interactDistance;
    [SerializeField] private LayerMask interactLayer;

    private RaycastHit _raycastHit;
    protected ISelectable _selectable;

    public override void Interact()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out _raycastHit, interactDistance, interactLayer))
        {
            if (_raycastHit.transform.TryGetComponent(out _selectable))
            {
                _selectable.OnHoverEnter();

                if (_input.InteractPressed)
                {
                    _selectable.OnSelect();
                }
            }
        }

        if (_raycastHit.transform == null && _selectable != null)
        {
            _selectable.OnHoverExit();
            _selectable = null;
        }
    }
}
