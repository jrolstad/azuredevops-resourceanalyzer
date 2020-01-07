Feature: Work Summary Search

Background: 
Given project 'my-project' in organization 'the-org'
And and teams
| name   | area paths    |
| team-1 | path-1;path-3 |
| team-2 | path-2        |
And work items with type 'user story'
| id | title | area path | status | assigned to | updated at | created at | activated at | resolved at | completed at |
| 1  | work  | path-2    | Active | person-1    | 1/2/2017   | 1/1/2017   | 2/3/2017     |             |              |
