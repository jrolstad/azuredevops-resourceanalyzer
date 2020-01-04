Feature: Project Summary Search

Background: 
Given project 'my-project' in organization 'the-org'
And repository 'a-repo' with branches
| branch name | commits behind | commits ahead |
| master      | 0              | 0             |
| feature/foo | 2              | 5             |
And commits for 'a-repo'
| author name | author email      | committed at | lines deleted | lines edited | lines added |
| person-1    | person1@mail.com  | 12/2/2019    | 5             | 3            | 2           |
| person-1    | person.1@mail.com | 12/10/2019   | 0             | 1            | 6           |
| person-2    | person2@mail.com  | 1/25/2019    | 8             | 2            | 3           |
And pull requests for 'a-repo'
| created by | created at | status    |
| person-1   | 2/15/2019  | active    |
| person-1   | 1/25/2019  | abandoned |
| person-2   | 2/25/2018  | completed |
And build definitions for 'a-repo'
| name    |
| build-1 |
| build-2 |

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
And the project summary results contains contributors for 'a-repo'
| contributor | commits | last activity | lines deleted | lines edited | lines added |
| person-1    | 2       | 12/10/2019    | 5             | 4            | 8           |
| person-2    | 1       | 1/25/2019     | 8             | 2            | 3           |
And the project summary results contains pull requests for 'a-repo'
| created by | last activity | abandoned | active | completed |
| person-1   | 2/15/2019     | 1         | 1      | 0         |
| person-2   | 2/25/2018     | 0         | 0      | 1         |
And the project summary results contains build definitions for 'a-repo'
| name    |
| build-1 |
| build-2 |