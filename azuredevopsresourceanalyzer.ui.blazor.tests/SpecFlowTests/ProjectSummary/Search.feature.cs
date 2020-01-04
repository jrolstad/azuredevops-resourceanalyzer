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
namespace azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.ProjectSummary
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.1.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class ProjectSummarySearchFeature : object, Xunit.IClassFixture<ProjectSummarySearchFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "Search.feature"
#line hidden
        
        public ProjectSummarySearchFeature(ProjectSummarySearchFeature.FixtureData fixtureData, azuredevopsresourceanalyzer_ui_blazor_tests_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Project Summary Search", null, ProgrammingLanguage.CSharp, ((string[])(null)));
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
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "branch name",
                        "commits behind",
                        "commits ahead"});
            table1.AddRow(new string[] {
                        "master",
                        "0",
                        "0"});
            table1.AddRow(new string[] {
                        "feature/foo",
                        "2",
                        "5"});
#line 5
testRunner.And("repository \'a-repo\' with branches", ((string)(null)), table1, "And ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "author name",
                        "author email",
                        "committed at",
                        "lines deleted",
                        "lines edited",
                        "lines added"});
            table2.AddRow(new string[] {
                        "person-1",
                        "person1@mail.com",
                        "12/2/2019",
                        "5",
                        "3",
                        "2"});
            table2.AddRow(new string[] {
                        "person-1",
                        "person.1@mail.com",
                        "12/10/2019",
                        "0",
                        "1",
                        "6"});
            table2.AddRow(new string[] {
                        "person-2",
                        "person2@mail.com",
                        "1/25/2019",
                        "8",
                        "2",
                        "3"});
#line 9
testRunner.And("commits for \'a-repo\'", ((string)(null)), table2, "And ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "created by",
                        "created at",
                        "status"});
            table3.AddRow(new string[] {
                        "person-1",
                        "2/15/2019",
                        "active"});
            table3.AddRow(new string[] {
                        "person-1",
                        "1/25/2019",
                        "abandoned"});
            table3.AddRow(new string[] {
                        "person-2",
                        "2/25/2018",
                        "completed"});
#line 14
testRunner.And("pull requests for \'a-repo\'", ((string)(null)), table3, "And ");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "name"});
            table4.AddRow(new string[] {
                        "build-1"});
            table4.AddRow(new string[] {
                        "build-2"});
#line 19
testRunner.And("build definitions for \'a-repo\'", ((string)(null)), table4, "And ");
#line hidden
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Execute Search shows all matching components")]
        [Xunit.TraitAttribute("FeatureTitle", "Project Summary Search")]
        [Xunit.TraitAttribute("Description", "Execute Search shows all matching components")]
        public virtual void ExecuteSearchShowsAllMatchingComponents()
        {
            string[] tagsOfScenario = ((string[])(null));
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Execute Search shows all matching components", null, ((string[])(null)));
#line 24
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
#line 25
testRunner.Given("I enter \'the-org\' into Organization", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 26
testRunner.And("I enter \'my-project\' into Project", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 27
testRunner.And("I enter \'\' into Repository Filter", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 28
testRunner.And("I enter \'12/25/2018\' into Start Date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 29
testRunner.When("I press the Search button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 30
testRunner.Then("no errors are shown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
                TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                            "repository"});
                table5.AddRow(new string[] {
                            "a-repo"});
#line 31
testRunner.And("the project summary results contain repositories", ((string)(null)), table5, "And ");
#line hidden
                TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                            "branch name",
                            "commits behind",
                            "commits ahead"});
                table6.AddRow(new string[] {
                            "master",
                            "0",
                            "0"});
                table6.AddRow(new string[] {
                            "feature/foo",
                            "2",
                            "5"});
#line 34
testRunner.And("the project summary results contains branches for \'a-repo\'", ((string)(null)), table6, "And ");
#line hidden
                TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                            "contributor",
                            "commits",
                            "last activity",
                            "lines deleted",
                            "lines edited",
                            "lines added"});
                table7.AddRow(new string[] {
                            "person-1",
                            "2",
                            "12/10/2019",
                            "5",
                            "4",
                            "8"});
                table7.AddRow(new string[] {
                            "person-2",
                            "1",
                            "1/25/2019",
                            "8",
                            "2",
                            "3"});
#line 38
testRunner.And("the project summary results contains contributors for \'a-repo\'", ((string)(null)), table7, "And ");
#line hidden
                TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                            "created by",
                            "last activity",
                            "abandoned",
                            "active",
                            "completed"});
                table8.AddRow(new string[] {
                            "person-1",
                            "2/15/2019",
                            "1",
                            "1",
                            "0"});
                table8.AddRow(new string[] {
                            "person-2",
                            "2/25/2018",
                            "0",
                            "0",
                            "1"});
#line 42
testRunner.And("the project summary results contains pull requests for \'a-repo\'", ((string)(null)), table8, "And ");
#line hidden
                TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                            "name"});
                table9.AddRow(new string[] {
                            "build-1"});
                table9.AddRow(new string[] {
                            "build-2"});
#line 46
testRunner.And("the project summary results contains build definitions for \'a-repo\'", ((string)(null)), table9, "And ");
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
                ProjectSummarySearchFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                ProjectSummarySearchFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
