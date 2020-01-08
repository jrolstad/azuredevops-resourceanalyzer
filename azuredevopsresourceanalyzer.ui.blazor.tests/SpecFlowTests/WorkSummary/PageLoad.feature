Feature: Work Summary Page Load


Scenario: Work Summary page is loaded
Given the WorkSummary page is loaded
Then Organization is empty
And Project is empty
And the list of available projects is empty
And StartDate is '[2 Years Ago]'
And no errors are shown
And the page shows as not working
