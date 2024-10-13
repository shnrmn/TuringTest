using UnityEngine;

public class DoubleDoorClose : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private GameObject objectToDelete;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetBool("Open", false);
            if (objectToDelete != null)
                Destroy(objectToDelete);
        }
    }
}
