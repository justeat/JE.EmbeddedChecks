using System;

namespace JE.EmbeddedChecks
{
    public abstract class EmbeddedCheck<TCheckResult> : IAmACheck
    {
        private readonly string _name;

        protected EmbeddedCheck(string name)
        {
            _name = name;
        }

        public CheckResult Execute()
        {
            var result = new CheckResult(_name);
            try
            {
                var customThing = Run(result);
                EnrichResultWith(result, customThing);
                return result;
            }
            catch (Exception exception)
            {
                return ResultFromException(exception);
            }
        }

        protected virtual void EnrichResultWith(CheckResult result, TCheckResult checkResult)
        {
            result.Status = CheckStatus.Passed;
        }

        protected abstract TCheckResult Run(CheckResult result);

        protected virtual CheckResult ResultFromException(Exception ex)
        {
            return new CheckResult(_name)
            {
                Status = CheckStatus.FailedInconclusive,
                Message = ex.GetBaseException().Message,
                StackTrace = ex.GetBaseException().StackTrace
            };
        }
    }
}
