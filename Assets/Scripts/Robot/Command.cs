public abstract class Command
{
    public abstract bool IsComplete { get; }
    public abstract void Execute();
}
