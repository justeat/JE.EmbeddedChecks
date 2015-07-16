namespace JE.EmbeddedChecks.Tests
{
    public class describe_running_checks : nspec_base
    {
        public void when_running()
        {
            context["and there are 3 checks"] = () =>
                {
                    context["and all pass"] = () =>
                        {
                            xit["should return 3 passes"] = () => { };
                            xit["should return 0 inconclusive"] = () => { };
                            xit["should return 0 failures"] = () => { };
                        };
                    context["and 1 fails"] = () =>
                        {
                            xit["should return 2 passes"] = () => { };
                            xit["should return 0 inconclusive"] = () => { };
                            xit["should return 1 failure"] = () => { };
                        };
                    context["and 1 cannot be run for an error"] = () =>
                        {
                            xit["should return 2 passes"] = () => { };
                            xit["should return 1 inconclusive"] = () => { };
                            xit["should return 0 failure"] = () => { };
                        };
                };
        }
    }
}