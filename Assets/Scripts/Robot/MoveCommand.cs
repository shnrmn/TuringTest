using UnityEngine;
using UnityEngine.AI;

public class MoveCommand : Command
{
    private NavMeshAgent _agent;
    private Vector3 _destination;

    public MoveCommand(NavMeshAgent agent, Vector3 destination)
    {
        _agent = agent;
        _destination = destination;
    }

    public override bool IsComplete => ReachedDestination();

    public override void Execute()
    {
        _agent?.SetDestination(_destination);
    }

    private bool ReachedDestination()
    {
        if (_agent == null) return false;
        return _agent.remainingDistance <= 0.1f;
    }
}
