using System.Collections.Generic;

namespace JE.EmbeddedChecks
{
    public interface IRunChecks
    {
        IList<CheckResult> Run(IList<IAmACheck> checks);
    }
}
