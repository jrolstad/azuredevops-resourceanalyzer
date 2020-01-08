Feature: Project Summary Page Load


Scenario: Project Summary page is loaded
Given the ProjectSummary page is loaded
Then Organization is empty
And Project is empty
And the list of available projects is empty
And StartDate is '[2 Years Ago]'
And no errors are shown
And the page shows as not working
