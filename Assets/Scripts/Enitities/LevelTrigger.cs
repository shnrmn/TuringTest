using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            levelManager.EndLevel();
            Destroy(gameObject);
        }
    }
}
