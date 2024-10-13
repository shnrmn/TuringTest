using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour, ISelectable
{
    [SerializeField] private Material hoverColor;
    [SerializeField] private Animator deviceAnimator;

    private MeshRenderer _renderer;
    private Material _defaultColor;

    public UnityEvent OnPush;

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _defaultColor = _renderer.material;
    }

    public void ActivateDevice()
    {
        deviceAnimator.SetTrigger("Activate");
    }

    public void OnHoverEnter()
    {
        _renderer.material = hoverColor;
    }

    public void OnHoverExit()
    {
        _renderer.material = _defaultColor;
    }

    public void OnSelect()
    {
        OnPush?.Invoke();
    }
}
