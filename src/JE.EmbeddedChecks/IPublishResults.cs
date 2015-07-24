using System;
using System.Collections.Generic;

namespace JE.EmbeddedChecks
{
    public interface IPublishResults
    {
        void PublishResultButCatchExceptions(CheckResult result);

        void PublishRunStarted(IList<IAmACheck> checks);

        void PublishRunFinished(IList<CheckResult> results, TimeSpan elapsed);
    }
}
