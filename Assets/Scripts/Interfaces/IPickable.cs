using UnityEngine;

public interface IPickable
{
    public void OnPickedUp(Transform attachedTransform);
    public void OnDropped();
}
