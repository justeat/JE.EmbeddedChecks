namespace JE.EmbeddedChecks
{
    public interface IAmACheck
    {
        CheckResult Execute();

        string Name { get; }
    }
}