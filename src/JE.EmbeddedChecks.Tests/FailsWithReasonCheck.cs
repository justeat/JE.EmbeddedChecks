namespace JE.EmbeddedChecks.Tests
{
    internal class FailsWithReasonCheck : EmbeddedCheck<bool>
    {
        public FailsWithReasonCheck(string name)
            : base(name)
        {
        }

        protected override bool Run(CheckResult result)
        {
            return true;
        }

        protected override void EnrichResultWith(CheckResult result, bool checkResult)
        {
            base.EnrichResultWith(result, checkResult);
            result.Status = CheckStatus.FailedWithReason;
        }
    }
}