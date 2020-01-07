Feature: SearchProjects

Background: 
Given project 'my-project-2' in organization 'the-org'
Given project 'my-project-3' in organization 'the-org'
Given project 'my-project-1' in organization 'the-org'
Given project 'my-project-4' in organization 'the-other-org'

Scenario: Search projects in organization shows all projects
Given I enter 'the-org' into Organization on the WorkSummary page
When I press the Project Search button on the WorkSummary page
Then the list of projects shown on the WorkSummary page are 
| Project      |
| my-project-1 |
| my-project-2 |
| my-project-3 |
And the selected project on the WorkSummary page is 'my-project-1' 
And no errors are shown on the WorkSummary page