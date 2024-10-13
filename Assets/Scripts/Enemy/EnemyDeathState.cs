using UnityEngine;

public class EnemyDeathState : EnemyState
{
    public EnemyDeathState(EnemyController enemyController) : base(enemyController)
    {
    }

    public override void OnStateEnter()
    {
        _enemyController.agent.isStopped = true;
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
    }
}
