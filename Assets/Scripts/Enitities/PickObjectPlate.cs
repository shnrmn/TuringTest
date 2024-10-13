using UnityEngine;

public class PickObjectPlate : MonoBehaviour
{
    [SerializeField] private DoubleDoor door;
    [SerializeField] private Color activeColor;

    private bool _isTriggered;
    private Color _baseColor;
    private MeshRenderer _plateRenderer;

    private void Start()
    {
        _plateRenderer = GetComponent<MeshRenderer>();
        _baseColor = _plateRenderer.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isTriggered && other.CompareTag("Pickable"))
        {
            _isTriggered = true;
            door.SetTrigger(true);
            _plateRenderer.material.color = activeColor;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isTriggered && other.CompareTag("Pickable"))
        {
            _isTriggered = false;
            door.SetTrigger(false);
            _plateRenderer.material.color = _baseColor;
        }
    }
}
