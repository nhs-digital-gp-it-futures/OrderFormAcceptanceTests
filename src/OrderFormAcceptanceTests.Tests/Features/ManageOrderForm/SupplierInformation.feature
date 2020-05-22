Feature: Supplier Information
	As a Buyer
	I want to manage Supplier part of Order Form 
	So that the information is correct

Scenario: Supplier Information - Sections presented
	Given an unsubmitted order exists
	And the Supplier section is not complete
	When the Order Form for the existing order is presented
	Then there is the Supplier section
	And the user is able to manage the Supplier section

Scenario: Supplier Information - Order description section is now complete
	Given an unsubmitted order exists
	And the Supplier section is not complete
	When the Order Form for the existing order is presented
	Then the Call Off Agreement ID is displayed
	And there is the Order description section
	And there is the Call-off Ordering Party section
	And there is the Supplier section
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the Submit order button is disabled

Scenario: Call Off Ordering Party section is now complete but, Supplier not
Given the completed sections are Order Description
And Call Off Ordering Party
And the section not complete is Supplier
When the Order Form is presented
Then the Call Off Agreement ID is displayed
And the Order description is displayed
And the enabled sections are Order description
And Call Off Ordering Party
And Supplier
And the Preview order summary button is enabled
And the Delete order button is enabled
And the Submit order button is disabled

Scenario: Supplier section is now complete but, Call Off Ordering Party not
Given the completed sections are Order Description
And Supplier
And the sections that are not complete are Call Off Ordering Party
When the Order Form is presented
Then the Call Off Agreement ID is displayed
And the Order description is displayed
And the enabled sections are Order description
And Call Off Ordering Party
And Supplier
And the Preview order summary button is enabled
And the Delete order button is enabled
And the Submit order button is disabled

