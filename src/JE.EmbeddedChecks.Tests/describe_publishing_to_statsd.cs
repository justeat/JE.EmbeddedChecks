using System;
using System.Collections.Generic;
using FakeItEasy;
using FakeItEasy.ExtensionSyntax.Full;
using JE.EmbeddedChecks.StatsD;
using JustEat.StatsD;
using Ploeh.AutoFixture;

namespace JE.EmbeddedChecks.Tests
{
    public class describe_publishing_to_statsd : nspec_base
    {
        private IList<CheckResult> _allPassing;

        private TimeSpan _elapsed;

        private CheckResult _failResult;

        private IStatsDPublisher _statsd;

        private PublishToStatsD _sut;

        private void before_each()
        {
            _statsd = AF.Create<IStatsDPublisher>();
            _allPassing = new List<CheckResult>
                              {
                                  new CheckResult("a")
                                      {
                                          Status = CheckStatus.Passed,
                                          Duration = TimeSpan.FromMilliseconds(34)
                                      }
                              };
            _failResult = new CheckResult("b")
                              {
                                  Status = CheckStatus.FailedInconclusive,
                                  Duration = TimeSpan.FromMilliseconds(25)
                              };
            _elapsed = TimeSpan.FromMilliseconds(2345);
            _sut = new PublishToStatsD(_statsd);
        }

        private void when_publishing_run_finished()
        {
            act = () => _sut.PublishRunFinished(_allPassing, _elapsed);
            it["should be a pass"] =
                () => _statsd.CallsTo(x => x.Timing(_elapsed, "checks._run.pass")).MustHaveHappened();
        }

        private void when_publishing_check_result()
        {
            act = () => _sut.PublishCheckResult(_allPassing[0]);
            it["should publish check duration as pass"] =
                () => _statsd.CallsTo(x => x.Timing(_allPassing[0].Duration, "checks.a.Passed")).MustHaveHappened();
        }

        private void when_publishing_check_error()
        {
            act = () => _sut.OnCheckError(_failResult, new NotSupportedException());
            it["should publish check duration as fail"] =
                () =>
                _statsd.CallsTo(
                    x => x.Timing(_failResult.Duration, "checks.b.FailedInconclusive.NotSupportedException"))
                    .MustHaveHappened();
        }
    }
}