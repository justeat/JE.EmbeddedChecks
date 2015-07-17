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
                            it["should return a fail result"] = () => { _result.Status.ShouldBe(CheckStatus.FailedInconclusive); };
                            it["should have the exception message"] =
                                () => { _result.Message.ShouldBe("You shall not pass!"); };
                            it["should have the exception stacktrace"] = () => { _result.StackTrace.ShouldNotBe(null); };
                            it["should have the raw exception"] =
                                () => _result.RawException.ShouldBeOfType<NotSupportedException>();
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
}
