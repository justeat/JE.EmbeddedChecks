namespace JE.EmbeddedChecks.Tests
{
    internal class EnrichingCheck : EmbeddedCheck<bool>
    {
        public EnrichingCheck(string name)
            : base(name)
        {
        }

        protected override bool Run(CheckResult result)
        {
            return false;
        }

        protected override void EnrichResultWith(CheckResult result, bool checkResult)
        {
            result.MetaData["data"] = checkResult.ToString();
            base.EnrichResultWith(result, checkResult);
        }
    }
}
