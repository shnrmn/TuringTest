using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private float _distanceToPlayer;
    private Health _playerHealth;
    private float _damagePerSecond = 10f;

    public EnemyAttackState(EnemyController enemyController) : base(enemyController)
    {
        _playerHealth = enemyController.Player.GetComponent<Health>();
    }

    public override void OnStateEnter()
    {
        Debug.Log("Enter Attack State");
    }

    public override void OnStateExit()
    {
        Debug.Log("Exit Attack State");
    }

    public override void OnStateUpdate()
    {
        Attack();
        if (_enemyController.Player != null)
        {
            _distanceToPlayer = Vector3.Distance(_enemyController.Player.position, _enemyController.transform.position);

            if (_distanceToPlayer > _enemyController.aggroDistance)
            {
                _enemyController.ChangeState(new EnemyFollowState(_enemyController));
            }

            _enemyController.agent.destination = _enemyController.Player.position;
        }
        else
        {
            _enemyController.ChangeState(new EnemyIdleState(_enemyController));
        }
    }

    public void Attack()
    {
        if (_playerHealth != null)
        {
            _playerHealth.DeductHealth(_damagePerSecond * Time.deltaTime);
        }
    }
}
