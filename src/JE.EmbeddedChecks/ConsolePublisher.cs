using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace JE.EmbeddedChecks
{
    public class ConsolePublisher : ResultPublisher
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

        protected override void Publish(CheckResult result)
        {
            Console.Write(_map[result.Status]);
        }

        protected override void OnPublishError(CheckResult result, Exception exception)
        {
            Trace.TraceError(
                "{0} failed: {1}|{2}|{3}|{4}",
                result.Name,
                result.Message,
                result.MetaData,
                result.StackTrace,
                exception);
        }
    }
}
