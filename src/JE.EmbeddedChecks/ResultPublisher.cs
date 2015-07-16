using System;

namespace JE.EmbeddedChecks
{
    public abstract class ResultPublisher : IPublishResults
    {
        public void PublishResultButCatchExceptions(CheckResult result)
        {
            try
            {
                Publish(result);
            }
            catch (Exception ex)
            {
                OnPublishError(result, ex);
            }
        }

        protected abstract void Publish(CheckResult result);

        protected virtual void OnPublishError(CheckResult result, Exception exception)
        {
            // hint: log it.
        }
    }
}
