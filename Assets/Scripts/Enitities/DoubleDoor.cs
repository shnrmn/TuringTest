using UnityEngine;

public class DoubleDoor : MonoBehaviour
{
    [SerializeField] private float waitTime = 1.0f;
    [SerializeField] private bool isLocked = true;
    [SerializeField] private Animator animator;
    [SerializeField] private int triggerCountToOpen;

    private float _timer = 0.0f;
    private int _triggerCount = 0;

    private void OnTriggerStay(Collider other)
    {
        if (!isLocked) return;
        if (!other.CompareTag("Player")) return;

        _timer += Time.deltaTime;

        if (_timer >= waitTime)
        {
            _timer = waitTime;
            OpenDoor(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        OpenDoor(false);
    }

    public void LockDoor()
    {
        isLocked = true;
    }

    public void UnlockDoor()
    {
        isLocked = false;
    }

    public void SetTrigger(bool trigger)
    {
        Debug.Log("SetTrigger: " + trigger);
        if (trigger)
            _triggerCount++;
        else
            _triggerCount--;

        if (_triggerCount >= triggerCountToOpen)
            OpenDoor(true);
        else
            OpenDoor(false);
    }

    public void OpenDoor(bool open)
    {
        if (!isLocked)
            animator.SetBool("Open", open);
    }
}
