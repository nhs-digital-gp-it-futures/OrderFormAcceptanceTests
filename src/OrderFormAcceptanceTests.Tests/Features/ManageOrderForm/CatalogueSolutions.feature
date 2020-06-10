Feature: Catalogue Solutions
	As a Buyer
	I want to manage the Catalogue Solutions of Order Form 
	So that the information is correct

Background: 
	Given an unsubmitted order exists
	And the Catalogue Solutions section is not complete

Scenario: Catalogue Solutions- Sections presented
	When the Order Form for the existing order is presented
	Then there is the Catalogue Solutions section
	And the User is able to manage the Catalogue Solutions section

Scenario: Catalogue Solutions - Service Recipient now complete, >=1 Service Recipient
	Given there are one or more Service Recipients in the order
	When the Order Form for the existing order is presented
	Then the Call Off Agreement ID is displayed
	And there is the Order description section
	And there is the Call-off Ordering Party section
	And there is the Supplier section
	And there is the Commencement date section
	And there is the Service Recipients section
	And the content validation status of the service-recipients section is complete
	And there is the Catalogue Solutions section
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the Submit order button is disabled

Scenario: Catalogue Solutions - Service Recipient now complete, 0 Service Recipient
	Given there are no Service Recipients in the order
	When the Order Form for the existing order is presented
	Then the content validation status of the service-recipients section is complete
	And there is the Catalogue Solutions section
	And the User is not able to manage the Catalogue Solutions section 

Scenario: Catalogue Solutions - Catalogue Solution dashboard
	When the User has chosen to manage the Catalogue Solution section
	Then the Catalogue Solution dashboard is presented
	And the Call Off Agreement ID is displayed in the page title
	And the Order description is displayed
	And there is a control to add a Catalogue Solution
	And there is a control to continue

Scenario: Catalogue Solutions - No Catalogue Solution added
	When the User has chosen to manage the Catalogue Solution section
	Then there is content indicating there is no Catalogue Solution added
	#@ignore
Scenario: Catalogue Solutions - User chooses not to add Catalogue Solution
	Given the User has chosen to manage the Catalogue Solution section
	And the User chooses not to add a Catalogue Solution
	When they choose to continue
	Then the Order is saved
	And the content validation status of the catalogue-solutions section is complete

Scenario: Catalogue Solutions - Go back
	Given the User has chosen to manage the Catalogue Solution section
	When the User chooses to go back
	Then the Order dashboard is presented
