﻿Feature: Complete Order

Scenario: Enable Complete Button - Funding Source now complete, >=1 Catalogue Solution, >=0 Associated Services
	Given an unsubmited order with catalogue items exists
	And there are one or more Service Recipients in the order
	And there is one or more Additional Services added to the order
	And there is no Associated Service in the order but the section is complete
	And the Funding Source section is complete
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
	And the Funding Source section is enabled
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the Complete order button is enabled

Scenario: Enable Complete Button - Funding Source now complete, >=1 Catalogue Solution, >=1 Associated Service
	Given an unsubmited order with catalogue items exists
	And there are one or more Service Recipients in the order
	And there is one or more Additional Services added to the order
	And an Associated Service with a flat price declarative order type is saved to the order
	And the Funding Source section is complete
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
	And the Funding Source section is enabled
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the Complete order button is enabled

Scenario: Enable Complete Button - Funding Source now complete, 0 Service Recipient, 0 Catalogue Solution, >=1 Associated Service
	Given an unsubmitted order exists
	And there are no Service Recipients in the order
	And an Associated Service with a flat price declarative order type is saved to the order
	And the Funding Source section is complete
	When the Order Form for the existing order is presented
	Then the Call Off Agreement ID is displayed
	And the Order description is displayed
	And the Call-off Ordering Party section is enabled
	And the Supplier section is enabled
	And the Commencement date section is enabled 
	And the Service Recipient section is enabled
	And the Catalogue Solution section is not enabled
	And the Additional Service section is not enabled
	And the Associated Service section is enabled
	And the Funding Source section is enabled
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the Complete order button is enabled

Scenario: Enable Complete Button - Funding Source now complete, >=1 Service Recipient, 0 Catalogue Solution, >=1 Associated Service
	Given an unsubmitted order exists
	And there are one or more Service Recipients in the order
	And there is no Catalogue Solution in the order but the section is complete
	And an Associated Service with a flat price declarative order type is saved to the order
	And the Funding Source section is complete
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
	And the Funding Source section is enabled
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the Complete order button is enabled
@ignore
Scenario: Complete Order - Complete order screen if Funding Source was 'yes'
	Given the order is complete enough so that the Complete order button is enabled with Funding Source option 'yes' selected
	And the Order Form for the existing order is presented
	When the User chooses to complete the Order
	Then the confirm complete order screen is displayed
	And there is specific content related to the User answering 'yes' on the Funding Source question
	And the Call Off Agreement ID is displayed in the page title
	And the Order description is displayed
	And there is a control to complete order

Scenario: Complete Order - Go back before complete order
	Given that the User is on the confirm complete order screen with Funding Source option 'yes' selected
	And the User has not completed the Order
	When the User chooses to go back
	Then the Order dashboard is presented
@ignore
Scenario: Complete Order - Order completed if Funding Source was 'yes' confirmation screen
	Given that the User is on the confirm complete order screen with Funding Source option 'yes' selected
	When the User confirms to complete the Order
	Then the Order completed screen is displayed
	And the Call Off Agreement ID is displayed in the page title
	And the Order description is displayed
	And there is specific content related to the User answering 'yes' on the Funding Source question
@ignore
Scenario: Complete Order - Go Back after Order completed
	Given that the User has completed their Order
	When the User chooses to go back
	Then the Organisation Orders Dashboard is displayed

Scenario: Complete Order - Complete order screen if Funding Source was 'no'
	Given the order is complete enough so that the Complete order button is enabled with Funding Source option 'no' selected
	And the Order Form for the existing order is presented
	When the User chooses to complete the Order
	Then the confirm complete order screen is displayed
	And there is specific content related to the User answering 'no' on the Funding Source question
	And the Call Off Agreement ID is displayed in the page title
	And the Order description is displayed
	And there is a control to complete order
@ignore
Scenario: Complete Order - Order completed if Funding Source was 'no' confirmation screen
	Given that the User is on the confirm complete order screen with Funding Source option 'no' selected
	When the User confirms to complete the Order
	Then the Order completed screen is displayed
	And the Call Off Agreement ID is displayed in the page title
	And the Order description is displayed
	And there is specific content related to the User answering 'no' on the Funding Source question
	And there is a control that allows the User to download a .PDF version of the Order Summary