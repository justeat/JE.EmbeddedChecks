namespace JE.EmbeddedChecks
{
    /// <remarks>These names & values will be used within metrics, so changing a value is contractual.</remarks>
    public enum CheckStatus
    {
        Unknown = 0,
        Passed = 20000,
        FailedInconclusive = 50000,
        FailedWithReason = 55000
    }
}
