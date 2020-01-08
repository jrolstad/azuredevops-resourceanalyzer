Feature: Work Summary Search

Background: 
Given project 'my-project' in organization 'the-org'
And and teams
| name   | area paths    |
| team-1 | path-1;path-3 |
| team-2 | path-2        |
And work items with type 'user story'
| title  | area path | status | assigned to | updated at | created at | activated at | resolved at | completed at |
| work-0 | path-0    | Active | person-1    | 4/2/2018   | 3/1/2018   | 3/15/2018    |             |              |
| work-1 | path-1    | Active | person-1    | 4/2/2018   | 3/1/2018   | 3/15/2018    |             |              |
| work-2 | path-2    | Active | person-1    | 1/2/2017   | 1/1/2017   | 2/3/2017     |             |              |
| work-3 | path-2    | New    | person-2    | 1/2/2017   | 1/1/2017   |              |             |              |
| work-4 | path-2    | Closed | person-2    | 1/2/2017   | 1/1/2017   | 2/3/2017     | 2/4/2017    | 2/5/2017     |
And work items with type 'feature'
| title  | area path | status | assigned to | updated at | created at | activated at | resolved at | completed at |
| work-5 | path-2    | Active | person-1    | 4/2/2018   | 3/1/2018   | 3/15/2018    |             |              |


Scenario: Execute Search shows all matching teams
Given the WorkSummary page is loaded
And I enter 'the-org' into Organization
And I enter 'my-project' into Project
And I enter '' into Team Filter
And I enter '1/25/2016' into Start Date
When I press the Search button
Then no errors are shown
And the work summary results contain teams
| team   |
| team-1 |
| team-2 |
And the work summary results contains work item types for 'team-2' 
| type       | new count | active count | resolved count | completed count |
| user story | 1         | 1            | 0              | 1               |
| feature    | 0         | 1            | 0              | 0               |
And the work summary results contains lifespan metrics for 'team-2'
| type       | days in backlog | days in active | days in resolved | days active to done | days end to end |
| user story | 33              | 1              | 1                | 2                   | 35              |
| feature    | 14              |                |                  |                     |                 |
And the work summary results contains contributors for 'team-2' 
| contributor |
| person-1    |
| person-2    |
And work summary results contains contributor 'person-1' for 'team-2' with work item counts
| type       | new count | active count | resolved count | completed count | days active to done |
| user story | 0         | 1            | 0              | 0               |                     |
| feature    | 0         | 1            | 0              | 0               |                     |
And work summary results contains contributor 'person-2' for 'team-2' with work item counts
| type       | new count | active count | resolved count | completed count | days active to done |
| user story | 1         | 0            | 0              | 1               |                     |
