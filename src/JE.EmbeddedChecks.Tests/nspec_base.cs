using System.Linq;
using NSpec;
using NSpec.Domain;
using NSpec.Domain.Formatters;
using Shouldly;
using Xunit;

namespace JE.EmbeddedChecks.Tests
{
    public abstract class nspec_base : nspec
    {
        [Fact]
        public void run()
        {
            var currentSpec = GetType();
            var finder = new SpecFinder(new[] { currentSpec });
            var builder = new ContextBuilder(finder, new Tags().Parse(currentSpec.Name), new DefaultConventions());
            var runner = new ContextRunner(builder, new ConsoleFormatter(), false);
            var collection = builder.Contexts().Build();
            var results = runner.Run(collection);

            //assert that there aren't any failures
            var failures = results.Failures();
            var count = failures.Count();
            count.ShouldBe(0);

            // TODO: figure out how to programmatically skip, when there are pending
        }
    }
}
