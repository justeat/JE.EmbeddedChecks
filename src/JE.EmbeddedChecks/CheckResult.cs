using System.Collections.Generic;

namespace JE.EmbeddedChecks
{
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
}