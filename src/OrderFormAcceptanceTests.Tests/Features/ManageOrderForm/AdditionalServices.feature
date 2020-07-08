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

Scenario: Additional Service dashboard
	When the Order Form for the existing order is presented
	And the User has chosen to manage the Additional Service section
	Then the Additional Service dashboard is presented
	And the Order description is displayed
	And there is a control to add a Additional Service
	And there is a control to continue

Scenario: No Additional Service added
	When the Order Form for the existing order is presented
	And the User has chosen to manage the Additional Service section
	And there is no Additional Service added to the order
	Then there is content indicating there is no Additional Service added

Scenario: User chooses not to add Additional Service
	When the Order Form for the existing order is presented
	And the User has chosen to manage the Additional Service section
	And there is no Additional Service added to the order
	When they choose to continue	
	Then the content validation status of the additional-services section is complete

Scenario: Go back
	When the Order Form for the existing order is presented
	And the User has chosen to manage the Additional Service section
	And the User chooses to go back
	Then the Order dashboard is presented