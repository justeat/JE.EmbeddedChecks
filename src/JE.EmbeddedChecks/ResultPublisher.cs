using System;
using System.Collections.Generic;

namespace JE.EmbeddedChecks
{
    public class ResultPublisher : IPublishResults
    {
        public void PublishResultButCatchExceptions(CheckResult result)
        {
            try
            {
                PublishCheckResult(result);
            }
            catch (Exception ex)
            {
                OnCheckError(result, ex);
            }
        }

        public virtual void PublishRunStarted(IList<IAmACheck> checks)
        {
            
        }

        public virtual void PublishRunFinished(IList<CheckResult> results, TimeSpan elapsed)
        {
            
        }

        public virtual void PublishCheckResult(CheckResult result)
        {
        }

        public virtual void OnCheckError(CheckResult result, Exception exception)
        {
        }
    }
}
