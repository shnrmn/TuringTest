using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int health;

    public UnityEvent OnDeath;
    public Transform enemy;
    public float playerCheckDistance;
    public float aggroDistance;
    public float checkRadius;
    public Transform[] targetPoints;
    public NavMeshAgent agent;

    private EnemyState _currentState;
    private ParticleSystem _explode;
    private bool _isDead;

    public Transform Player { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _currentState = new EnemyIdleState(this);
        _currentState.OnStateEnter();
        _explode = GetComponent<ParticleSystem>();
        _explode.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.OnStateUpdate();
    }

    public void ChangeState(EnemyState state)
    {
        _currentState.OnStateExit();
        _currentState = state;
        _currentState.OnStateEnter();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemy.position, checkRadius);
        Gizmos.DrawWireSphere(enemy.position + enemy.forward * playerCheckDistance, checkRadius);

        Gizmos.DrawLine(enemy.position, enemy.position + enemy.forward * playerCheckDistance);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Explode into pieces
    /// </summary>
    private void Die()
    {
        if (_isDead) return;
        _isDead = true;
        ChangeState(new EnemyDeathState(this));
        _explode.Play();
        Destroy(gameObject, _explode.main.duration);
        OnDeath?.Invoke();
    }
}
