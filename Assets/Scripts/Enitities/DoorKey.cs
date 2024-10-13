using UnityEngine;
using UnityEngine.Events;

public class DoorKey : MonoBehaviour
{
    public UnityEvent OnKeyPicked;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnKeyPicked?.Invoke();
            Destroy(gameObject);
        }
    }
}
