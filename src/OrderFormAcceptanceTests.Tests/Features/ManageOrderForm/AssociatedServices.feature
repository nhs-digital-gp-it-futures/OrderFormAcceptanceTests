Feature: Associated Services
	As a Buyer
	I want to manage the Associated Services of Order Form
	So that I can ensure the information is correct

Background: 
	Given an unsubmitted order exists
	And the Associated Services section is not complete
	@ignore
Scenario: Associated Services - Sections presented
	When the Order Form for the existing order is presented
	Then there is the Associated Services section
	And the User is able to manage the Associated Services section
	@ignore
Scenario: Associated Services - Service Recipient now complete, 0 Service Recipient
	Given there are no Service Recipients in the order
	And the Catalogue Solutions section is not complete
	When the Order Form for the existing order is presented
	Then the Call Off Agreement ID is displayed
	And the Order description is displayed
	And the Call-off Ordering Party section is enabled
	And the Supplier section is enabled
	And the Commencement date section is enabled 
	And the Service Recipient section is enabled
	And the Associated Service section is enabled
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the Submit order button is disabled