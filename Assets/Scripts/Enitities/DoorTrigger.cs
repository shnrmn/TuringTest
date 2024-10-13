using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("DoorStay", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("DoorStay", false);
        }
    }
}
