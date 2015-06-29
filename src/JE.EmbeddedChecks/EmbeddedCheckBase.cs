using System;
using System.Linq;
using System.Collections.Generic;

namespace JE.EmbeddedChecks
{
    public enum CheckStatus
    {
        Unknown = 0,
        Passed = 20000,
        Failed = 50000
    }

    public interface IAmACheck
    {
        CheckResult Execute();
    }

    public abstract class EmbeddedCheckBase<TCheckResult> : IAmACheck
    {
        private string _name;

        protected EmbeddedCheckBase(string name)
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
            }
            catch (Exception exception)
            {
                result = ResultFromException(exception);
                OnError(result);
            }
            return result;
        }

        protected virtual void OnError(CheckResult result)
        {
            // _logger.Error(() => new { Log = "HealthCheck Error", _name, Error = exception.GetBaseException() }.ToJson());
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
                Status = CheckStatus.Failed,
                Message = ex.GetBaseException().Message,
                StackTrace = ex.GetBaseException().StackTrace
            };
        }
    }

    public class CheckResult
    {
        public CheckResult(string name)
        {
            Name = name;
            Status = CheckStatus.Unknown;
            MetaData = new Dictionary<string, string>();
        }

        public string Name { get; set; }
        public CheckStatus Status { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public IDictionary<string, string> MetaData { get; set; }
    }

    public interface IRunChecks
    {
        IList<CheckResult> Run();
    }

    public class CheckRunner : IRunChecks
    {
        private readonly IEnumerable<IAmACheck> _checks;

        public CheckRunner(IEnumerable<IAmACheck> checks)
        {
            _checks = checks;
        }

        public IList<CheckResult> Run()
        {
            return _checks.Select(check => check.Execute()).ToList();
        }
    }
}
