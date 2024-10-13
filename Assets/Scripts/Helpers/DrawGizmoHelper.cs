using UnityEngine;

public class DrawGizmoHelper : MonoBehaviour
{
    [SerializeField] private float size = 0.5f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, size);
    }
}
