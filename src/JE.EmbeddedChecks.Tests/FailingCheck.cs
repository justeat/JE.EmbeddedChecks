using System;

namespace JE.EmbeddedChecks.Tests
{
    internal class FailingCheck : EmbeddedCheck<bool>
    {
        public FailingCheck(string name)
            : base(name)
        {
        }

        protected override bool Run(CheckResult result)
        {
            throw new NotSupportedException("You shall not pass!");
        }
    }
}