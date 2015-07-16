namespace JE.EmbeddedChecks.Tests
{
    public class describe_an_embedded_check : nspec_base
    {
        public void when_executing()
        {
            context["and check passes"] = () => { xit["should return a pass result"] = () => { }; };
            context["and check fails"] = () =>
                {
                    context["with an error during execution"] =
                        () => { xit["should return a fail result"] = () => { }; };
                    context["with a check failure"] = () => { xit["should return a fail result"] = () => { }; };
                };
            context["and the check enriches the result"] =
                () => { xit["should return the result with the enrichment"] = () => { }; };
        }
    }
}
