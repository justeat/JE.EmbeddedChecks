using System.Collections.Generic;
using System.Linq;
using Shouldly;

namespace JE.EmbeddedChecks.Tests
{
    public class describe_running_checks : nspec_base
    {
        private IEnumerable<IAmACheck> _checks;

        private IEnumerable<CheckResult> _results;

        private IRunChecks _runner;

        public void when_running()
        {
            before = () => _runner = new CheckRunner(new ConsolePublisher());
            act = () => _results = _runner.Run(_checks);
            context["and there are 3 checks"] = () =>
                {
                    context["and all pass"] = () =>
                        {
                            before = () =>
                                    {
                                        _checks = new List<IAmACheck>
                                                      {
                                                          new PassingCheck("a"),
                                                          new PassingCheck("b"),
                                                          new PassingCheck("c"),
                                                          new PassingCheck("d")
                                                      };
                                    };
                            it["should return 3 passes"] = () => ResultsShouldBe(CheckStatus.Passed, 4);
                            it["should return 0 inconclusive"] = () => ResultsShouldBe(CheckStatus.FailedInconclusive, 0);
                            it["should return 0 failed"] = () => ResultsShouldBe(CheckStatus.FailedWithReason, 0);
                            it["should return 0 unknown"] = () => ResultsShouldBe(CheckStatus.Unknown, 0);
                        };
                    context["and 1 fails for a specific reason"] = () =>
                        {
                            before = () =>
                                    {
                                        _checks = new List<IAmACheck>
                                                      {
                                                          new PassingCheck("a"),
                                                          new PassingCheck("b"),
                                                          new FailsWithReasonCheck("c"),
                                                          new PassingCheck("d")
                                                      };
                                    };
                            it["should return 2 passes"] = () => ResultsShouldBe(CheckStatus.Passed, 3);
                            it["should return 1 failed"] = () => ResultsShouldBe(CheckStatus.FailedWithReason, 1);
                            it["should return 0 inconclusive"] = () => ResultsShouldBe(CheckStatus.FailedInconclusive, 0);
                            it["should return 0 unknown"] = () => ResultsShouldBe(CheckStatus.Unknown, 0);
                        };
                    context["and 1 fails because of an exception"] = () =>
                        {
                            before = () =>
                            {
                                _checks = new List<IAmACheck>
                                                      {
                                                          new PassingCheck("a"),
                                                          new FailingCheck("b"),
                                                          new PassingCheck("c"),
                                                          new PassingCheck("d")
                                                      };
                            };
                            it["should return 2 passes"] = () => ResultsShouldBe(CheckStatus.Passed, 3);
                            it["should return 0 failures"] = () => ResultsShouldBe(CheckStatus.FailedWithReason, 0);
                            it["should return 1 inconclusive"] = () => ResultsShouldBe(CheckStatus.FailedInconclusive, 1);
                            it["should return 0 unknown"] = () => ResultsShouldBe(CheckStatus.Unknown, 0);
                        };
                };
        }

        private void ResultsShouldBe(CheckStatus expectedStatus, int expectedCount)
        {
            _results.Count(x => x.Status == expectedStatus).ShouldBe(expectedCount);
        }
    }
}
