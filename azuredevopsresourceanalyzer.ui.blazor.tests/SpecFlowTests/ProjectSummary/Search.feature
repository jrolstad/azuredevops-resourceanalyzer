Feature: Project Summary Search

Background: 
Given project 'my-project' in organization 'the-org'
And repository 'a-repo' with branches
| branch name | commits behind | commits ahead |
| master      | 0              | 0             |
| feature/foo | 2              | 5             |

Scenario: Execute Search shows all matching components
Given I enter 'the-org' into Organization
And I enter 'my-project' into Project
And I enter '' into Repository Filter
And I enter '12/25/2018' into Start Date
When I press the Search button
Then no errors are shown
And the project summary results contain repositories
| repository   |
| a-repo       |
And the project summary results contains branches for 'a-repo'
| branch name | commits behind | commits ahead |
| master      | 0              | 0             |
| feature/foo | 2              | 5             |