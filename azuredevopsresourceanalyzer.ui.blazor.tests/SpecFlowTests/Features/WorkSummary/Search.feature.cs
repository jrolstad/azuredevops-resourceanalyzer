// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:3.1.0.0
//      SpecFlow Generator Version:3.1.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Features.WorkSummary
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.1.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class WorkSummarySearchFeature : object, Xunit.IClassFixture<WorkSummarySearchFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "Search.feature"
#line hidden
        
        public WorkSummarySearchFeature(WorkSummarySearchFeature.FixtureData fixtureData, azuredevopsresourceanalyzer_ui_blazor_tests_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Work Summary Search", null, ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 3
#line hidden
#line 4
testRunner.Given("project \'my-project\' in organization \'the-org\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table16 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "area paths"});
            table16.AddRow(new string[] {
                        "team-1",
                        "path-1;path-3"});
            table16.AddRow(new string[] {
                        "team-2",
                        "path-2"});
#line 5
testRunner.And("and teams", ((string)(null)), table16, "And ");
#line hidden
            TechTalk.SpecFlow.Table table17 = new TechTalk.SpecFlow.Table(new string[] {
                        "title",
                        "area path",
                        "status",
                        "assigned to",
                        "updated at",
                        "created at",
                        "activated at",
                        "resolved at",
                        "completed at"});
            table17.AddRow(new string[] {
                        "work-0",
                        "path-0",
                        "Active",
                        "person-1",
                        "4/2/2018",
                        "3/1/2018",
                        "3/15/2018",
                        "",
                        ""});
            table17.AddRow(new string[] {
                        "work-1",
                        "path-1",
                        "Active",
                        "person-1",
                        "4/2/2018",
                        "3/1/2018",
                        "3/15/2018",
                        "",
                        ""});
            table17.AddRow(new string[] {
                        "work-2",
                        "path-2",
                        "Active",
                        "person-1",
                        "1/2/2017",
                        "1/1/2017",
                        "2/3/2017",
                        "",
                        ""});
            table17.AddRow(new string[] {
                        "work-3",
                        "path-2",
                        "New",
                        "person-2",
                        "1/2/2017",
                        "1/1/2017",
                        "",
                        "",
                        ""});
            table17.AddRow(new string[] {
                        "work-4",
                        "path-2",
                        "Closed",
                        "person-2",
                        "1/2/2017",
                        "1/1/2017",
                        "2/3/2017",
                        "2/4/2017",
                        "2/5/2017"});
#line 9
testRunner.And("work items with type \'user story\'", ((string)(null)), table17, "And ");
#line hidden
            TechTalk.SpecFlow.Table table18 = new TechTalk.SpecFlow.Table(new string[] {
                        "title",
                        "area path",
                        "status",
                        "assigned to",
                        "updated at",
                        "created at",
                        "activated at",
                        "resolved at",
                        "completed at"});
            table18.AddRow(new string[] {
                        "work-5",
                        "path-2",
                        "Active",
                        "person-1",
                        "4/2/2018",
                        "3/1/2018",
                        "3/15/2018",
                        "",
                        ""});
#line 16
testRunner.And("work items with type \'feature\'", ((string)(null)), table18, "And ");
#line hidden
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Execute Search shows all matching teams")]
        [Xunit.TraitAttribute("FeatureTitle", "Work Summary Search")]
        [Xunit.TraitAttribute("Description", "Execute Search shows all matching teams")]
        public virtual void ExecuteSearchShowsAllMatchingTeams()
        {
            string[] tagsOfScenario = ((string[])(null));
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Execute Search shows all matching teams", null, ((string[])(null)));
#line 21
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 3
this.FeatureBackground();
#line hidden
#line 22
testRunner.Given("the WorkSummary page is loaded", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 23
testRunner.And("I enter \'the-org\' into Organization", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 24
testRunner.And("I enter \'my-project\' into Project", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 25
testRunner.And("I enter \'\' into Team Filter", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 26
testRunner.And("I enter \'1/25/2016\' into Start Date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 27
testRunner.When("I press the Search button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 28
testRunner.Then("no errors are shown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
                TechTalk.SpecFlow.Table table19 = new TechTalk.SpecFlow.Table(new string[] {
                            "team"});
                table19.AddRow(new string[] {
                            "team-1"});
                table19.AddRow(new string[] {
                            "team-2"});
#line 29
testRunner.And("the work summary results contain teams", ((string)(null)), table19, "And ");
#line hidden
                TechTalk.SpecFlow.Table table20 = new TechTalk.SpecFlow.Table(new string[] {
                            "type",
                            "new count",
                            "active count",
                            "resolved count",
                            "completed count"});
                table20.AddRow(new string[] {
                            "user story",
                            "1",
                            "1",
                            "0",
                            "1"});
                table20.AddRow(new string[] {
                            "feature",
                            "0",
                            "1",
                            "0",
                            "0"});
#line 33
testRunner.And("the work summary results contains work item types for \'team-2\'", ((string)(null)), table20, "And ");
#line hidden
                TechTalk.SpecFlow.Table table21 = new TechTalk.SpecFlow.Table(new string[] {
                            "type",
                            "days in backlog",
                            "days in active",
                            "days in resolved",
                            "days active to done",
                            "days end to end"});
                table21.AddRow(new string[] {
                            "user story",
                            "33",
                            "1",
                            "1",
                            "2",
                            "35"});
                table21.AddRow(new string[] {
                            "feature",
                            "14",
                            "",
                            "",
                            "",
                            ""});
#line 37
testRunner.And("the work summary results contains lifespan metrics for \'team-2\'", ((string)(null)), table21, "And ");
#line hidden
                TechTalk.SpecFlow.Table table22 = new TechTalk.SpecFlow.Table(new string[] {
                            "contributor"});
                table22.AddRow(new string[] {
                            "person-1"});
                table22.AddRow(new string[] {
                            "person-2"});
#line 41
testRunner.And("the work summary results contains contributors for \'team-2\'", ((string)(null)), table22, "And ");
#line hidden
                TechTalk.SpecFlow.Table table23 = new TechTalk.SpecFlow.Table(new string[] {
                            "type",
                            "new count",
                            "active count",
                            "resolved count",
                            "completed count",
                            "days active to done"});
                table23.AddRow(new string[] {
                            "user story",
                            "0",
                            "1",
                            "0",
                            "0",
                            ""});
                table23.AddRow(new string[] {
                            "feature",
                            "0",
                            "1",
                            "0",
                            "0",
                            ""});
#line 45
testRunner.And("work summary results contains contributor \'person-1\' for \'team-2\' with work item " +
                        "counts", ((string)(null)), table23, "And ");
#line hidden
                TechTalk.SpecFlow.Table table24 = new TechTalk.SpecFlow.Table(new string[] {
                            "type",
                            "new count",
                            "active count",
                            "resolved count",
                            "completed count",
                            "days active to done"});
                table24.AddRow(new string[] {
                            "user story",
                            "1",
                            "0",
                            "0",
                            "1",
                            ""});
#line 49
testRunner.And("work summary results contains contributor \'person-2\' for \'team-2\' with work item " +
                        "counts", ((string)(null)), table24, "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.1.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                WorkSummarySearchFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                WorkSummarySearchFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion