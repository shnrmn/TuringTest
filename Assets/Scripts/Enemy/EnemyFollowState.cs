using UnityEngine;

public class EnemyFollowState : EnemyState
{
    private float _distanceToPlayer;

    public EnemyFollowState(EnemyController enemyController) : base(enemyController)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("Enter Follow State");
    }

    public override void OnStateExit()
    {
        Debug.Log("Exit Follow State");
    }

    public override void OnStateUpdate()
    {
        if (_enemyController.Player != null)
        {
            _distanceToPlayer = Vector3.Distance(_enemyController.Player.position, _enemyController.transform.position);

            if (_distanceToPlayer > _enemyController.playerCheckDistance)
            {
                _enemyController.ChangeState(new EnemyIdleState(_enemyController));
            }

            if (_distanceToPlayer < _enemyController.aggroDistance)
            {
                _enemyController.ChangeState(new EnemyAttackState(_enemyController));
            }

            _enemyController.agent.destination = _enemyController.Player.position;
        }
        else
        {
            _enemyController.ChangeState(new EnemyIdleState(_enemyController));
        }
    }
}
