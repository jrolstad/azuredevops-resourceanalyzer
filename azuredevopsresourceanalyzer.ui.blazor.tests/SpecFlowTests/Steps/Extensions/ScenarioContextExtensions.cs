using azuredevopsresourceanalyzer.ui.blazor.Application.ViewModels;
using azuredevopsresourceanalyzer.ui.blazor.tests.TestUtility;
using TechTalk.SpecFlow;

namespace azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.Extensions
{
    public static class ScenarioContextExtensions
    {
        public static ProjectSummaryViewModel ProjectSummary(this ScenarioContext context)
        {
            if (!context.ContainsKey("ProjectSummary"))
            {
                context.Set(context.TestRoot().Get<ProjectSummaryViewModel>(), "ProjectSummary");
            }

            return context.Get<ProjectSummaryViewModel>("ProjectSummary");
        }

        public static TestCompositionRoot TestRoot(this ScenarioContext context)
        {
            if (!context.ContainsKey("TestCompositionRoot"))
            {
                context.Set(TestCompositionRoot.Create(), "TestCompositionRoot");
            }

            return context.Get<TestCompositionRoot>("TestCompositionRoot");
        }

        
    }
}