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
@ignore
Scenario: Associated Services - Catalogue Solution now complete, >= 1 Service Recipient, 0 Catalogue Solution
	Given there are one or more Service Recipients in the order
	And the Catalogue Solution section is complete
	And there is no Catalogue Solution in the order
	When the Order Form for the existing order is presented
	Then the Call Off Agreement ID is displayed
	And the Order description is displayed
	And the Call-off Ordering Party section is enabled
	And the Supplier section is enabled
	And the Commencement date section is enabled 
	And the Service Recipient section is enabled
	And the Catalogue Solution section is enabled
	And the Additional Service section is not enabled
	And the Associated Service section is enabled
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the Submit order button is disabled

Scenario: Associated Services - Service Recipient >=1, Catalogue Solution is NULL 
	Given there are one or more Service Recipients in the order
	And the Catalogue Solutions section is not complete
	When the Order Form for the existing order is presented
	Then the Call Off Agreement ID is displayed
	And the Order description is displayed
	And the Call-off Ordering Party section is enabled
	And the Supplier section is enabled
	And the Commencement date section is enabled 
	And the Service Recipient section is enabled
	And the Catalogue Solution section is enabled
	And the Additional Service section is not enabled
	And the Associated Service section is not enabled
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the Submit order button is disabled

Scenario: Associated Services - >= 1 Service Recipient, >=1 Catalogue Solution, NULL Additional Service
	Given a Catalogue Solution is added to the order
	And the Additional Services section is not complete
	When the Order Form for the existing order is presented
	Then the Call Off Agreement ID is displayed
	And the Order description is displayed
	And the Call-off Ordering Party section is enabled
	And the Supplier section is enabled
	And the Commencement date section is enabled 
	And the Service Recipient section is enabled
	And the Catalogue Solution section is enabled
	And the Additional Service section is enabled
	And the Associated Service section is not enabled
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the Submit order button is disabled
@ignore
Scenario: Associated Services - Additional Service now complete, >= 1 Service Recipient, >=1 Catalogue Solution, 0 Additional Services
	Given a Catalogue Solution is added to the order
	And the Additional Services section is complete
	And there are no Additional Services in the order
	When the Order Form for the existing order is presented
	Then the Call Off Agreement ID is displayed
	And the Order description is displayed
	And the Call-off Ordering Party section is enabled
	And the Supplier section is enabled
	And the Commencement date section is enabled 
	And the Service Recipient section is enabled
	And the Catalogue Solution section is enabled
	And the Additional Service section is enabled
	And the Associated Service section is enabled
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the Submit order button is disabled
@ignore
Scenario: Associated Services - Additional Service now complete, >= 1 Service Recipient, >=1 Catalogue Solution, =>1 Additional Service
	Given a Catalogue Solution is added to the order
	And an Additional Service is added to the order
	When the Order Form for the existing order is presented
	Then the Call Off Agreement ID is displayed
	And the Order description is displayed
	And the Call-off Ordering Party section is enabled
	And the Supplier section is enabled
	And the Commencement date section is enabled 
	And the Service Recipient section is enabled
	And the Catalogue Solution section is enabled
	And the Additional Service section is enabled
	And the Associated Service section is enabled
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the Submit order button is disabled