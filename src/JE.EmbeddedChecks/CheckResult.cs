using System;
using System.Collections.Generic;

namespace JE.EmbeddedChecks
{
    public class CheckResult
    {
        public CheckResult(string name)
        {
            Name = name;
            Status = CheckStatus.Unknown;
            MetaData = new Dictionary<string, object>();
        }

        public string Name { get; set; }

        public CheckStatus Status { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public IDictionary<string, object> MetaData { get; set; }

        public Exception RawException { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
