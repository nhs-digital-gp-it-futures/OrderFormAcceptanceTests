Feature: Call Off Ordering Party
	As a Buyer
	I want to manage the Call Off Ordering Party part of Order Form
	So that the information is correct

Scenario: Call Off Ordering Party - Sections presented
	Given an unsubmitted order exists
	And the Call Off Ordering Party section is not complete
	When the Order Form for the existing order is presented
	Then there is the Call-off Ordering Party section
	And the user is able to manage the Call-off Ordering Party section

Scenario: Call Off Ordering Party - Order description section is now complete
	Given an unsubmitted order exists
	And the Call Off Ordering Party section is not complete
	When the Order Form for the existing order is presented
	Then the Call Off Agreement ID is displayed
	And there is the Order description section
	And there is the Call-off Ordering Party section
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the User is unable to submit the order
