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

Scenario: Call Off Ordering Party - Name and Address autopopulated
	Given an unsubmitted order exists
	And the Call Off Ordering Party section is not complete
	When the User chooses to edit the Call Off Ordering Party information
	Then the Call Off Ordering Party ODS code is autopopulated from the User's organisation
	And the Call Off Ordering Party Organisation Name is autopopulated from the User's organisation
	And the Call Off Ordering Party Organisation Address is autopopulated from the User's organisation
	And the Call Off Agreement ID is displayed in the page title

Scenario: Call Off Ordering Party - Unable to edit Call Off Ordering Party Organisation ODS code
	Given an unsubmitted order exists
	And the Call Off Ordering Party section is not complete
	When the User chooses to edit the Call Off Ordering Party information
	Then the User is unable to edit the ODS code

Scenario: Call Off Ordering Party - Unable to edit Call Off Ordering Party Organisation Name
	Given an unsubmitted order exists
	And the Call Off Ordering Party section is not complete
	When the User chooses to edit the Call Off Ordering Party information
	Then the User is unable to edit the Organisation Name

Scenario: Call Off Ordering Party - Unable to edit Call Off Ordering Party Organisation Address
Given an unsubmitted order exists
	And the Call Off Ordering Party section is not complete
	When the User chooses to edit the Call Off Ordering Party information
	Then the User is unable to edit Address

Scenario: Call Off Ordering Party - Go back
	Given an unsubmitted order exists
	And the Call Off Ordering Party section is not complete
	And the User chooses to edit the Call Off Ordering Party information
	When the User chooses to go back
	Then the Order dashboard is presented

