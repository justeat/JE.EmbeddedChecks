using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace JE.EmbeddedChecks
{
    public class CheckRunner : IRunChecks
    {
        private readonly IPublishResults _publisher;

        public CheckRunner(IPublishResults publisher)
        {
            _publisher = publisher ?? new TracePublisher();
        }

        public IList<CheckResult> Run(IList<IAmACheck> checks)
        {
            var results = new List<CheckResult>();
            var sw = Stopwatch.StartNew();
            _publisher.PublishRunStarted(checks);
            foreach (var result in checks.Select(check => check.Execute()))
            {
                results.Add(result);
                _publisher.PublishResultButCatchExceptions(result);
            }
            _publisher.PublishRunFinished(results, sw.Elapsed);
            return results;
        }
    }
}
