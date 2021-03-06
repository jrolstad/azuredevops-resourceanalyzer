﻿Feature: SearchProjects

Background: 
Given project 'my-project-2' in organization 'the-org'
Given project 'my-project-3' in organization 'the-org'
Given project 'my-project-1' in organization 'the-org'
Given project 'my-project-4' in organization 'the-other-org'

Scenario: Search projects in organization shows all projects
Given the WorkSummary page is loaded
And I enter 'the-org' into Organization
When I press the Project Search button
Then the list of projects shown are 
| Project      |
| my-project-1 |
| my-project-2 |
| my-project-3 |
And the selected project is 'my-project-1' 
And no errors are shown

Scenario: Search projects with invalid organization shows error
Given the WorkSummary page is loaded
And I enter 'something-else' into Organization
When I press the Project Search button
Then the error is shown

Scenario: Search projects with empty organization shows error
Given the WorkSummary page is loaded
When I press the Project Search button
Then the error 'Unable to search projects; please enter an organization first' is shown