Feature: Project Summary Search

Background: 
Given project 'my-project' in organization 'the-org'

Scenario: Execute Search shows all matching components
Given I enter 'the-org' into Organization
And I enter 'my-project' into Project
And I enter '' into Repository Filter
And I enter '12/25/2018' into Start Date
When I press the Search button
Then 0 components are shown
And no errors are shown