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

        public static WorkSummaryViewModel WorkSummary(this ScenarioContext context)
        {
            if (!context.ContainsKey("WorkSummary"))
            {
                context.Set(context.TestRoot().Get<WorkSummaryViewModel>(), "WorkSummary");
            }

            return context.Get<WorkSummaryViewModel>("WorkSummary");
        }

        public static TestCompositionRoot TestRoot(this ScenarioContext context)
        {
            if (!context.ContainsKey("TestCompositionRoot"))
            {
                context.Set(TestCompositionRoot.Create(), "TestCompositionRoot");
            }

            return context.Get<TestCompositionRoot>("TestCompositionRoot");
        }

        public static string Organization(this ScenarioContext context)
        {
            return context.Get<string>("Organization");
        }

        public static void Organization(this ScenarioContext context, string value)
        {
            context.Set(value, "Organization");
        }

        public static string Project(this ScenarioContext context)
        {
            return context.Get<string>("Project");
        }

        public static void Project(this ScenarioContext context, string value)
        {
            context.Set(value, "Project");
        }

        public static ViewType ViewType(this ScenarioContext context)
        {
            return context.Get<ViewType>("ViewType");
        }

        public static void ViewType(this ScenarioContext context, ViewType value)
        {
            context.Set(value, "ViewType");
        }


    }

    public enum ViewType
    {
        ProjectSummary,
        WorkSummary
    }
}