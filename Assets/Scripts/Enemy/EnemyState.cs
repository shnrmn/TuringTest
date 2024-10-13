public abstract class EnemyState
{
    protected EnemyController _enemyController;

    public EnemyState(EnemyController enemyController)
    {
        _enemyController = enemyController;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();
}
