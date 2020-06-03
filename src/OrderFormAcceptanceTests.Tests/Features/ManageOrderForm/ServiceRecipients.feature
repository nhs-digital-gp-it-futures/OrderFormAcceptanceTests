Feature: Service Recipients
	As a Buyer
	I want to manage the Service Recipients part of Order Form
	So that the information is correct

Scenario: Service Recipients - Sections presented
	Given an unsubmitted order exists
	When the Order Form for the existing order is presented
	Then there is the Service Recipients section
	And the user is able to manage the Service Recipients section

Scenario: Service Recipients - Commencement date section is now complete
	Given an unsubmitted order exists
	And the Service Recipients section is not complete
	When the Order Form for the existing order is presented
	Then the Call Off Agreement ID is displayed
	And there is the Order description section
	And there is the Call-off Ordering Party section
	And there is the Supplier section
	And there is the Commencement date section
	And the content validation status of the commencement-date section is complete 
	And there is the Service Recipients section
	And the content validation status of the service-recipients section is incomplete 
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the Submit order button is disabled