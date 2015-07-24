using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace JE.EmbeddedChecks
{
    public class TracePublisher : ResultPublisher
    {
        private readonly IDictionary<CheckStatus, string> _map =
            new Dictionary<CheckStatus, string> {
            {
                CheckStatus.Unknown,
                "?"
            },
            {
                CheckStatus.Passed,
                "."
            },
            {
                CheckStatus.FailedInconclusive,
                "/"
            },
            {
                CheckStatus.FailedWithReason,
                "F"
            }
        };

        public override void PublishCheckResult(CheckResult result)
        {
            Trace.TraceInformation(_map[result.Status]);
        }

        public override void OnCheckError(CheckResult result, Exception exception)
        {
            Trace.TraceError(
                "{0} failed: {1}|{2}|{3}|{4}|{5}",
                result.Name,
                result.Duration,
                result.Message,
                result.MetaData,
                result.StackTrace,
                exception);
        }
    }
}
