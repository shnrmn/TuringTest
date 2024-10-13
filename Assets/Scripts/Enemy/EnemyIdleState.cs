using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private int _currentTarget = 0;

    public EnemyIdleState(EnemyController enemyController) : base(enemyController)
    {
    }

    public override void OnStateEnter()
    {
        _enemyController.agent.destination = _enemyController.targetPoints[_currentTarget].position;
    }

    public override void OnStateExit()
    {
        Debug.Log("Exit Idle State");
    }

    public override void OnStateUpdate()
    {
        if (_enemyController.agent.remainingDistance < 0.1f)
        {
            _currentTarget++;

            if (_currentTarget >= _enemyController.targetPoints.Length)
                _currentTarget = 0;

            _enemyController.agent.destination = _enemyController.targetPoints[_currentTarget].position;
        }

        // If the player is in the enemy's line of sight, follow the player
        if (Physics.SphereCast(_enemyController.enemy.position, _enemyController.checkRadius, _enemyController.enemy.forward, out RaycastHit hit, _enemyController.playerCheckDistance)
            && hit.transform.CompareTag("Player"))
        {
            _enemyController.Player = hit.collider.transform;
            _enemyController.ChangeState(new EnemyFollowState(_enemyController));
        }
    }
}
