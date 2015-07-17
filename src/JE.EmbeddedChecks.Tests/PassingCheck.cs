namespace JE.EmbeddedChecks.Tests
{
    internal class PassingCheck : EmbeddedCheck<bool>
    {
        public PassingCheck(string name)
            : base(name)
        {
        }

        protected override bool Run(CheckResult result)
        {
            return true;
        }
    }
}