using System;
using System.Diagnostics;

namespace JE.EmbeddedChecks
{
    public abstract class EmbeddedCheck<TCheckResult> : IAmACheck
    {
        public string Name { get; }

        protected EmbeddedCheck(string name)
        {
            Name = name;
        }

        public CheckResult Execute()
        {
            var result = new CheckResult(Name);
            var sw = Stopwatch.StartNew();
            try
            {
                var customThing = Run(result);
                EnrichResultWith(result, customThing);
            }
            catch (Exception exception)
            {
                result = ResultFromException(exception);
            }
            finally
            {
                result.Duration = sw.Elapsed;
            }
            return result;
        }

        protected virtual void EnrichResultWith(CheckResult result, TCheckResult checkResult)
        {
            result.Status = CheckStatus.Passed;
        }

        protected abstract TCheckResult Run(CheckResult result);

        protected virtual CheckResult ResultFromException(Exception ex)
        {
            return new CheckResult(Name)
            {
                Status = CheckStatus.FailedInconclusive,
                Message = ex.GetBaseException().Message,
                StackTrace = ex.GetBaseException().StackTrace,
                RawException = ex
            };
        }
    }
}
