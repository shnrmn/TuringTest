using UnityEngine;

public class ScatterOnCollision : MonoBehaviour
{
    [Header("Scattering Prefab")]

    [SerializeField] private Rigidbody _prefabToScatter;

    [SerializeField] private int _numberOfInstances = 10;

    [SerializeField] private float _scatterRadius = 2f;

    [SerializeField] private float _scatterForce = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 collisionPoint = collision.contacts[0].point;
        Vector3 playerPosition = FindObjectOfType<PlayerController>().transform.position;
        Vector3 directionToPlayer = (playerPosition - collisionPoint).normalized;

        for (int i = 0; i < _numberOfInstances; i++)
        {
            // Calculate a random position around the collision point but towards the player
            Vector3 randomOffset = new(
                Random.Range(-_scatterRadius, _scatterRadius),
                Random.Range(-_scatterRadius, _scatterRadius),
                Random.Range(0, _scatterRadius)); // Only scatter towards the player, not behind

            Vector3 scatterPosition = collisionPoint + randomOffset + directionToPlayer * _scatterRadius * 0.5f;

            // Instantiate the prefab
            Rigidbody scatteredInstance = Instantiate(_prefabToScatter, scatterPosition, Quaternion.identity);

            // Apply a small force towards the player
            Vector3 scatterDirection = (playerPosition - scatterPosition).normalized + Random.insideUnitSphere * 0.1f;
            scatteredInstance.AddForce(scatterDirection * _scatterForce, ForceMode.Impulse);

            // Optionally destroy after a certain time
            Destroy(scatteredInstance.gameObject, 5.0f);
        }
    }
}
