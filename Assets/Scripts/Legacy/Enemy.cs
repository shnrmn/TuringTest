using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] targetPoints;
    [SerializeField] private Transform player;
    [SerializeField] private Transform enemy;
    [SerializeField] private float playerCheckDistance = 10f;
    [SerializeField] private float aggroDistance = 5f;
    [SerializeField] private float checkRadius;

    private int _currentTarget;

    public bool isIdle = true;
    public bool isPlayerFound;
    public bool isCloseToPlayer;

    private NavMeshAgent _agent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = targetPoints[_currentTarget].position;
    }

    void Update()
    {
        if (isIdle)
            Idle();
        else if (isPlayerFound)
        {
            if (isCloseToPlayer)
                AttackPlayer();
            else
                FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        if (player != null)
        {
            if (Vector3.Distance(player.position, transform.position) > playerCheckDistance)
            {
                isPlayerFound = false;
                isIdle = true;
            }

            if (Vector3.Distance(player.position, transform.position) < playerCheckDistance / 5)
            {
                isCloseToPlayer = true;
            }
            else
            {
                isCloseToPlayer = false;
            }

            _agent.destination = player.position;
        }
        else
        {
            isPlayerFound = false;
            isIdle = true;
            isCloseToPlayer = false;
        }
    }

    private void Idle()
    {
        if (_agent.remainingDistance < 0.1f)
        {
            _currentTarget++;

            if (_currentTarget >= targetPoints.Length)
                _currentTarget = 0;

            _agent.destination = targetPoints[_currentTarget].position;
        }

        if (Physics.SphereCast(enemy.position, checkRadius, transform.forward, out RaycastHit hit, aggroDistance)
            && hit.transform.CompareTag("Player"))
        {
            player = hit.transform;
            isPlayerFound = true;
            isIdle = false;
            _agent.destination = player.position;
        }
    }

    private void AttackPlayer()
    {
        if (Vector3.Distance(player.position, transform.position) > playerCheckDistance / 5)
        {
            isCloseToPlayer = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemy.position, checkRadius);
        Gizmos.DrawWireSphere(enemy.position + enemy.forward * playerCheckDistance, checkRadius);

        Gizmos.DrawLine(enemy.position, enemy.position + enemy.forward * playerCheckDistance);
    }
}
