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
	@ignore
Scenario: Supplier Information - Call Off Ordering Party section is now complete but, Supplier not 
	Given an unsubmitted order exists
	And the Supplier section is not complete
	When the Order Form for the existing order is presented
	Then the Call Off Agreement ID is displayed
	And there is the Order description section
	And there is the Call-off Ordering Party section
	And the content validation status of the call-off-ordering-party section is complete 
	And there is the Supplier section
	And the content validation status of the supplier section is incomplete 
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the Submit order button is disabled
	@ignore
Scenario: Supplier Information - Supplier section is now complete but, Call Off Ordering Party not
	Given an unsubmitted order exists
	And the Call Off Ordering Party section is not complete
	When the Order Form for the existing order is presented
	Then the Call Off Agreement ID is displayed
	And there is the Order description section
	And there is the Call-off Ordering Party section
	And the content validation status of the call-off-ordering-party section is incomplete 
	And there is the Supplier section
	And the content validation status of the supplier section is complete 
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the User is unable to submit the order

Scenario: Supplier Information - Search supplier 
	Given an unsubmitted order exists
	And the Supplier section is not complete
	When the User chooses to edit the Supplier section for the first time
	Then the Search supplier screen is presented
	And the Call Off Agreement ID is displayed

Scenario: Supplier Information - Supplier(s) returned
	Given an unsubmitted order exists
	And the Supplier section is not complete
	And the User chooses to edit the Supplier section for the first time
	When the User has entered a valid Supplier search criterion
	And they choose to search
	Then the matching Suppliers are presented

Scenario: Supplier Information - No Supplier(s) returned
	Given an unsubmitted order exists
	And the Supplier section is not complete
	And the User chooses to edit the Supplier section for the first time
	When the User has entered a non matching Supplier search criterion
	And they choose to search
	Then no matching Suppliers are presented
	And the User is informed that no matching Suppliers exist

Scenario: Supplier Information - Supplier name missing
	Given an unsubmitted order exists
	And the Supplier section is not complete
	And the User chooses to edit the Supplier section for the first time
	When the User has not entered a Supplier search criterion
	And the User chooses to search
	Then they are informed that a Supplier name needs to be entered

Scenario: Supplier Information - Go Back (Search Supplier)
	Given an unsubmitted order exists
	And the Supplier section is not complete
	And the User chooses to edit the Supplier section for the first time
	And the Search supplier screen is presented
	When the User chooses to go back
	Then the Order dashboard is presented

Scenario: Supplier Information - Go Back (No Supplier(s) returned)
	Given an unsubmitted order exists
	And the Supplier section is not complete
	And the User chooses to edit the Supplier section for the first time
	When the User has entered a non matching Supplier search criterion
	And they choose to search
	Then no matching Suppliers are presented
	When the User chooses to go back
	Then the Search supplier screen is presented