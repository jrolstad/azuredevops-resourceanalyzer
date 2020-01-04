Feature: Project Summary Search

Background: 
Given project 'my-project' in organization 'the-org'

Scenario: Execute Search
Given Project Summary with organization 'the-org', project 'my-project', repository filter '' and start date '12/1/2017'
When I press the Search button
Then 1 components are shown