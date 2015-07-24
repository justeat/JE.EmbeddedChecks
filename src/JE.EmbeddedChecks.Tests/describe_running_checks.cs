using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using FakeItEasy.ExtensionSyntax.Full;
using Ploeh.AutoFixture;
using Shouldly;

namespace JE.EmbeddedChecks.Tests
{
    public class describe_running_checks : nspec_base
    {
        private IList<IAmACheck> _checks;

        private IPublishResults _publisher;

        private IEnumerable<CheckResult> _results;

        private IRunChecks _sut;

        private void before_each()
        {
            _publisher = AF.Create<IPublishResults>();
            _sut = new CheckRunner(_publisher);
        }

        private void act_each()
        {
            _results = _sut.Run(_checks);
        }

        public void should_have_timed_run()
        {
            before = () => _checks = new List<IAmACheck> { new PassingCheck("a") };

            it["should have published run start"] =
                () => _publisher.CallsTo(x => x.PublishRunStarted(A<List<IAmACheck>>._)).MustHaveHappened();
            it["should have published run end"] =
                () =>
                _publisher.CallsTo(x => x.PublishRunFinished(A<List<CheckResult>>._, A<TimeSpan>._)).MustHaveHappened();
        }

        public void when_running()
        {
            it["should have timing information for each check"] =
                () => _results.All(x => x.Duration.Ticks > 0).ShouldBe(true);

            context["and there are 4 checks"] = () =>
                {
                    context["and all pass"] = () =>
                        {
                            before =
                                () =>
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
                            it["should return 0 inconclusive"] =
                                () => ResultsShouldBe(CheckStatus.FailedInconclusive, 0);
                            it["should return 0 failed"] = () => ResultsShouldBe(CheckStatus.FailedWithReason, 0);
                            it["should return 0 unknown"] = () => ResultsShouldBe(CheckStatus.Unknown, 0);
                        };
                    context["and 1 fails for a specific reason"] = () =>
                        {
                            before =
                                () =>
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
                            it["should return 0 inconclusive"] =
                                () => ResultsShouldBe(CheckStatus.FailedInconclusive, 0);
                            it["should return 0 unknown"] = () => ResultsShouldBe(CheckStatus.Unknown, 0);
                        };
                    context["and 1 fails because of an exception"] = () =>
                        {
                            before =
                                () =>
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
                            it["should return 1 inconclusive"] =
                                () => ResultsShouldBe(CheckStatus.FailedInconclusive, 1);
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