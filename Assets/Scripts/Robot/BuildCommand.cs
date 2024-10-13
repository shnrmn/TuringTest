using UnityEngine.AI;

public class BuildCommand : Command
{
    private readonly NavMeshAgent _agent;
    private readonly Builder _builder;

    public BuildCommand(NavMeshAgent agent, Builder builder)
    {
        _agent = agent;
        _builder = builder;
    }

    public override bool IsComplete => BuildComplete();

    public override void Execute()
    {
        if (_builder != null)
            _agent.SetDestination(_builder.transform.position);
    }

    private bool BuildComplete()
    {
        if (_agent.remainingDistance > 0.1f)
            return false;

        if (_builder != null)
            _builder.Build();

        return true;
    }
}
