Feature: Service Recipients
	As a Buyer
	I want to manage the Service Recipients part of Order Form
	So that the information is correct

Background: 
	Given an unsubmitted order exists
	And the Service Recipients section is not complete

Scenario: Service Recipients - Sections presented
	Given an unsubmitted order exists
	When the Order Form for the existing order is presented
	Then there is the Service Recipients section
	And the user is able to manage the Service Recipients section

Scenario: Service Recipients - Commencement date section is now complete	
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

Scenario: Service Recipients - Select Service Recipient
	Given the User chooses to edit the Service Recipient section 
	Then the Call Off Ordering Party's Name (organisation name) and ODS code are presented as a Service Recipient
	And the User is able to select the Call Off Ordering Party
	
Scenario: Service Recipients - Select all/Deselect all Service Recipients
	Given the User chooses to edit the Service Recipient section
	When the User chooses to select all 
	Then the Call Off Ordering Party is selected
	And the Select all button changes to Deselect all
	When the User chooses to deselect all 
	Then the selected Call Off Ordering Party presented is deselected
	And the Deselect all button changes to Select all
	@ignore
Scenario: Service Recipients - Call Off Ordering Party selected
	Given the User chooses to edit the Service Recipient section
	And the Call Off Ordering Party is selected
	When the User chooses to continue
	Then the Order is saved
	And the content validation status of the service-recipients section is complete
	And the Service Recipient section is saved in the DB
	And the Service Recipient is saved in the DB
	@ignore
Scenario: Service Recipients - Call Off Ordering Party is not selected
	Given the User chooses to edit the Service Recipient section
	And the Call Off Ordering Party is not selected
	When the User chooses to continue
	Then the Service Recipient section is not saved
	And the content validation status of the service-recipients section is complete
	And the Service Recipient section is saved in the DB