using UnityEngine;

public abstract class Interactor : MonoBehaviour
{
    protected PlayerInput _input;

    private void Start()
    {
        _input = PlayerInput.Instance;
    }

    void Update()
    {
        Interact();
    }

    public abstract void Interact();
}
