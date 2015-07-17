using System.Collections.Generic;
using System.Linq;

namespace JE.EmbeddedChecks
{
    public class CheckRunner : IRunChecks
    {
        private readonly IPublishResults _publisher;

        public CheckRunner(IPublishResults publisher)
        {
            _publisher = publisher;
        }

        public IList<CheckResult> Run(IEnumerable<IAmACheck> checks)
        {
            var results = new List<CheckResult>();
            foreach (var result in checks.Select(check => check.Execute()))
            {
                results.Add(result);
                _publisher.PublishResultButCatchExceptions(result);
            }
            return results;
        }
    }
}
