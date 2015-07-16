using System;
using Shouldly;

namespace JE.EmbeddedChecks.Tests
{
    public class describe_an_embedded_check : nspec_base
    {
        private EmbeddedCheck<bool> _check;

        private CheckResult _result;

        public void when_executing()
        {
            act = () => _result = _check.Execute();
            context["and check passes"] = () =>
                {
                    before = () => _check = new PassingCheck("frodo");
                    it["should return a pass result"] = () => { _result.Status.ShouldBe(CheckStatus.Passed); };
                };

            context["and check fails"] = () =>
                {
                    before = () => _check = new FailingCheck("gandalf");
                    context["with an error during execution"] = () =>
                        {
                            it["should return a fail result"] = () => { _result.Status.ShouldBe(CheckStatus.Failed); };
                            it["should have the exception message"] =
                                () => { _result.Message.ShouldBe("You shall not pass!"); };
                            it["should have the exception stacktrace"] = () => { _result.StackTrace.ShouldNotBe(null); };
                        };
                };

            context["and the check enriches the result"] = () =>
                {
                    before = () => _check = new EnrichingCheck("legolas");
                    it["should return the result with the enrichment"] =
                        () => { _result.MetaData["data"].ShouldBe(false.ToString()); };
                };
        }
    }

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
