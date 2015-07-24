using System;
using System.Collections.Generic;
using System.Linq;
using JustEat.StatsD;

namespace JE.EmbeddedChecks.StatsD
{
    public class PublishToStatsD : ResultPublisher
    {
        private readonly IStatsDPublisher _statsd;

        public PublishToStatsD(IStatsDPublisher statsd)
        {
            _statsd = statsd;
        }

        public override void PublishCheckResult(CheckResult result)
        {
            _statsd.Timing(result.Duration, string.Format("checks.{0}.{1}", result.Name, result.Status));
        }

        public override void OnCheckError(CheckResult result, Exception exception)
        {
            _statsd.Timing(result.Duration, string.Format("checks.{0}.{1}.{2}", result.Name, result.Status, exception.GetType().Name));
        }

        public override void PublishRunFinished(IList<CheckResult> results, TimeSpan elapsed)
        {
            var overall = results.All(x => x.Status == CheckStatus.Passed) ? "pass" : "errors";
            _statsd.Timing(elapsed, string.Format("checks._run.{0}", overall));
        }
    }
}