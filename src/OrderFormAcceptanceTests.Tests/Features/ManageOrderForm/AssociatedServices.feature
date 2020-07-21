Feature: Associated Services
	As a Buyer
	I want to manage the Associated Services of Order Form
	So that I can ensure the information is correct

Background: 
	Given an unsubmitted order exists
	And the Associated Services section is not complete

Scenario: Associated Services - Sections presented
	Given the Catalogue Solution section is complete
	When the Order Form for the existing order is presented
	Then there is the Associated Services section
	And the User is able to manage the Associated Services section

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
@ignore
Scenario: Associated Services - Select no Service Recipients and then go back and add service recipients
	Given there are no Service Recipients in the order
	And the Catalogue Solutions section is not complete
	And an Associated Service is added to the order
	And the Order Form for the existing order is presented
	When the User adds a Service Recipient to the Service Recipient section
	Then the Associated Service section is enabled 

Scenario: Associated Service dashboard
	Given there are no Service Recipients in the order
	When the User has chosen to manage the Associated Service section
	Then the Associated Services dashboard is presented
	And the Call Off Agreement ID is displayed in the page title
	And the Order description is displayed
	And there is a control to add an Associated Service
	And there is a control to continue

Scenario: Associated Service dashboard - No Associated Services added
	Given there are no Service Recipients in the order
	And the User has chosen to manage the Associated Service section
	When there is no Associated Service added to the order
	Then there is content indicating there is no Associated Service added

Scenario: Associated Service dashboard - User chooses not to add Associated Service
	Given there are no Service Recipients in the order
	And the User has chosen to manage the Associated Service section
	And the User chooses not to add an Associated Service
	When they choose to continue
	Then the Order is saved
	And the content validation status of the associated-services section is complete

Scenario: Associated Service dashboard - Go back
	Given there are no Service Recipients in the order
	And the User has chosen to manage the Associated Service section
	When the User chooses to go back
	Then the Order dashboard is presented
@ignore
Scenario: Select Associated Service - Select a single Associated Service to add
	Given there are no Service Recipients in the order
	And the User has chosen to manage the Associated Service section
	When the User has chosen to Add a single Associated Service
	Then they are presented with the Associated Services available from their chosen Supplier
	And the Call Off Agreement ID is displayed in the page title
	And they can select one Associated Service to add
@ignore
Scenario: Select Associated Service - No Associated Service selected
	Given there are no Service Recipients in the order
	And the User is presented with Associated Services available from their chosen Supplier
	And no Associated Service is selected
	When they choose to continue
	Then the User is informed they have to select an Associated Service
@ignore
Scenario: Select Associated Service - Go back
	Given there are no Service Recipients in the order
	And the User is presented with Associated Services available from their chosen Supplier
	When the User chooses to go back
	Then the Associated Services dashboard is presented
@ignore
Scenario: Select Associated Service - Display 'no associated services' message
	Given that the Supplier in the order has no associated services
	And there are no Service Recipients in the order
	And the User has chosen to manage the Associated Service section
	When the User has chosen to Add a single Associated Service
	Then the User is informed that there are no Associated Services to select
	And there is no Continue button
@ignore
Scenario: Associated Service - Select price - Select a price for the Associated Service
	Given there are no Service Recipients in the order
	And the User is presented with Associated Services available from their chosen Supplier
	And the User selects an Associated Service to add
	When they choose to continue
	Then all the available prices for that Associated Service are presented
	And they can select a price for the Associated Service
@ignore
Scenario: Associated Service - Select price - No price for the Associated Service selected
	Given there are no Service Recipients in the order
	And the User is presented with the prices for the selected Associated Service 
	And no Associated Service price is selected
	When they choose to continue
	Then the User is informed they have to select a Associated Service price
@ignore
Scenario: Associated Service - Select price - Go back 
	Given there are no Service Recipients in the order
	And the User is presented with the prices for the selected Associated Service 
	When the User chooses to go back
	Then they are presented with the Associated Services available from their chosen Supplier
	And the User's selected Associated Service is selected