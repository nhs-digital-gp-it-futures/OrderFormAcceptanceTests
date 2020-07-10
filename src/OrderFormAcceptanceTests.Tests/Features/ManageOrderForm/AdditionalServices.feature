Feature: Additional Services
	As a Buyer
	I want to manage the Additional Services of Order Form
	So that I can ensure the information is correct

Background:
	Given an unsubmited order with catalogue items exists

Scenario: Additional Services - Sections presented
	When the Order Form for the existing order is presented
	Then there is the Additional Service section
	And the User is able to manage the Additional Services section

Scenario: Additional Services - Catalogue Solution now complete, >= 1 Service Recipient, >=1 Catalogue Solution
	When the Order Form for the existing order is presented
	Then the Call Off Agreement ID is displayed
	And there is the Order description section
	And there is the Call-off Ordering Party section
	And there is the Supplier section
	And there is the Commencement date section
	And there is the Service Recipients section
	And there is the Additional Service section
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the Submit order button is disabled

Scenario: Additional Services - Catalogue Solution now complete, >= 1 Service Recipient, 0 Catalogue Solution
	Given the Catalogue Solutions section is not complete
	When the Order Form for the existing order is presented
	Then the Call Off Agreement ID is displayed
	And the Order description section is enabled
	And the Call-off Ordering Party section is enabled
	And the Supplier section is enabled
	And the Commencement date section is enabled 
	And the Service Recipient section is enabled
	And the Catalogue Solution section is enabled
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the Submit order button is disabled

Scenario: Additional Services - Additional Service dashboard
	Given the Order Form for the existing order is presented
	And the User is able to manage the Additional Services section
	When the Additional Service dashboard is presented
	Then the Order description is displayed
	And there is a control to add a Additional Service
	And there is a control to continue

Scenario: Additional Services - No Additional Service added
	Given the Order Form for the existing order is presented
	And the User is able to manage the Additional Services section
	When there is no Additional Service added to the order
	Then there is content indicating there is no Additional Service added

Scenario: Additional Services - User chooses not to add Additional Service
	Given the Order Form for the existing order is presented
	And the User is able to manage the Additional Services section
	And there is no Additional Service added to the order
	When they choose to continue	
	Then the content validation status of the additional-services section is complete

Scenario: Additional Services - Go back
	Given the Order Form for the existing order is presented
	And the User is able to manage the Additional Services section
	When the User chooses to go back
	Then the Order dashboard is presented

Scenario: Additional Services - Select a single Additional Service to add
	Given the Order Form for the existing order is presented
	And the User is able to manage the Additional Services section
	When the User chooses to add a single Additional Service
	Then they are presented with the Additional Service available from their chosen Supplier
	And they can select one Additional Service to add
	And the Call Off Agreement ID is displayed in the page title

Scenario: Additional Services - No Additional Service selected, produces relevant error message
	Given the Order Form for the existing order is presented
	Given the User is presented with Additional Services available from their chosen Supplier
	And no Additional Service is selected
	When they choose to continue
	When the user selects an error link in the Error Summary
	Then they will be navigated to the relevant part of the page

Scenario: Additional Services - Go back from select an additional service
	Given the Order Form for the existing order is presented
	Given the User is presented with Additional Services available from their chosen Supplier
	When the User chooses to go back
	Then the Additional Service dashboard is presented
