using System.Collections.Generic;
using System.Linq;

namespace JE.EmbeddedChecks
{
    public class CheckRunner : IRunChecks
    {
        private readonly IEnumerable<IAmACheck> _checks;

        private readonly IPublishResults _publisher;

        public CheckRunner(IEnumerable<IAmACheck> checks, IPublishResults publisher)
        {
            _checks = checks;
            _publisher = publisher;
        }

        public IList<CheckResult> Run()
        {
            var results = new List<CheckResult>();
            foreach (var result in _checks.Select(check => check.Execute()))
            {
                results.Add(result);
                _publisher.PublishResultButCatchExceptions(result);
            }
            return results;
        }
    }
}
