﻿Feature: Work Summary Search

Background: 
Given project 'my-project' in organization 'the-org'
And and teams
| name   | area paths    |
| team-1 | path-1;path-3 |
| team-2 | path-2        |
And work items with type 'user story'
| title  | area path | status | assigned to | updated at | created at | activated at | resolved at | completed at |
| work-1 | path-0    | Active | person-1    | 4/2/2018   | 3/1/2018   | 3/15/2018    |             |              |
| work-2 | path-2    | Active | person-1    | 1/2/2017   | 1/1/2017   | 2/3/2017     |             |              |


Scenario: Execute Search shows all matching teams
Given the WorkSummary page is loaded
And I enter 'the-org' into Organization
And I enter 'my-project' into Project
And I enter '' into Teams Filter
And I enter '1/25/2016' into Start Date
When I press the Search button
Then no errors are shown
And the work summary results contain teams
| team   |
| team-1 |
| team-2 |
And the work summary results contains work item types for 'team-1' 
| type       | new count | active count | resolved count | completed count |
| user story | 0         | 1            | 0              | 0               |
And the work summary results contains lifespan metrics for 'team-1'
| type       | days in backlog | days in active | days in resolved | days active to done | days end to end |
| user story | 14              |                |                  |                     |                 |
And the work summary results contains contributors for 'team-1' 
| contributor |
| person-1    |
And work summary results contains contributor 'person-1' for 'team-1' with work item counts
| type       | new count | active count | resolved count | completed count | days active to done |
| user story | 0         | 1            | 0              | 0               |                     |
