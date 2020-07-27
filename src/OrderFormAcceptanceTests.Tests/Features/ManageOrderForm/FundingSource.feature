Feature: Funding Source
	As a Buyer
	I want to manage the Funding Source of Order Form
	So that I can ensure the information is correct
	
Scenario: Funding Source - Sections presented
	Given the minimum data needed to enable the Funding Source section exists
	When the Order Form for the existing order is presented
	Then there is the Funding Source section
	And the User is able to manage the Funding Source section

Scenario: Funding Source - Associated Service now complete, >=1 Service Recipient, >=1 Catalogue Solution, >=1 Additional Service, >=1 Associated Service
	Given an unsubmited order with catalogue items exists
	And there are one or more Service Recipients in the order
	And there is one or more Additional Services added to the order
	And an Associated Service with a flat price declarative order type is saved to the order
	And the Funding Source section is not complete
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
	And the Submit order button is disabled

Scenario: Funding Source - Associated Service now complete, >= 1 Service Recipient, >= 1 Catalogue Solution, >=1 Additional Service, 0 Associated Service
	Given an unsubmited order with catalogue items exists
	And there are one or more Service Recipients in the order
	And there is one or more Additional Services added to the order
	And there is no Associated Service in the order but the section is complete
	And the Funding Source section is not complete
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
	And the Submit order button is disabled

Scenario: Funding Source - Associated Service now complete, >=1 Service Recipient, >=1 Catalogue Solution, 0 Additional Service, >=1 Associated Service
	Given an unsubmited order with catalogue items exists
	And there are one or more Service Recipients in the order
	And there is no Additional Service in the order but the section is complete
	And an Associated Service with a flat price declarative order type is saved to the order
	And the Funding Source section is not complete
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
	And the Submit order button is disabled

Scenario: Funding Source - Associated Service now complete, >= 1 Service Recipient, >= 1 Catalogue Solution, 0 Additional Service, 0 Associated Service
	Given an unsubmited order with catalogue items exists
	And there are one or more Service Recipients in the order
	And there is no Additional Service in the order but the section is complete
	And there is no Associated Service in the order but the section is complete
	And the Funding Source section is not complete
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
	And the Submit order button is disabled

Scenario: Funding Source - Associated Service now complete, 0 Service Recipient, >=1 Associated Service
	Given an unsubmitted order exists
	And there are no Service Recipients in the order
	And an Associated Service with a flat price declarative order type is saved to the order
	And the Funding Source section is not complete
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
	And the Submit order button is disabled

Scenario: Funding Source - Associated Service now complete, >=1 Service Recipient, 0 Catalogue Solution, >=1 Associated Service
	Given an unsubmitted order exists
	And there are one or more Service Recipients in the order
	And there is no Catalogue Solution in the order but the section is complete
	And an Associated Service with a flat price declarative order type is saved to the order
	And the Funding Source section is not complete
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
	And the Submit order button is disabled

Scenario: Funding Source - Associated Service Complete:  0 Service Recipient, 0 Catalogue Solutions, 0 Associated Service
	Given an unsubmitted order exists
	And there are no Service Recipients in the order
	And there is no Associated Service in the order but the section is complete
	And the Funding Source section is not complete
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
	And the Funding Source section is not enabled
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the Submit order button is disabled
@ignore
Scenario: Funding Source - Associated Service now complete, >= 1 Service Recipient, 0 Catalogue Solution, 0 Associated Service
	Given an unsubmitted order exists
	And there are one or more Service Recipients in the order
	And there is no Catalogue Solution in the order but the section is complete
	And there is no Associated Service in the order but the section is complete
	And the Funding Source section is not complete
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
	And the Funding Source section is not enabled
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the Submit order button is disabled

Scenario: Edit Funding Source - No Funding Source option selected
	Given the minimum data needed to enable the Funding Source section exists
	And the User is presented with the edit Funding Source page
	And no Funding Source option is selected
	When the User chooses to save
	Then they are informed they have to select a Funding Source option

Scenario: Edit Funding Source - Go back
	Given the minimum data needed to enable the Funding Source section exists
	And the User is presented with the edit Funding Source page
	When the User chooses to go back
	Then the Order dashboard is presented
@ignore
Scenario: Edit Funding Source - Funding source option selected
	Given the minimum data needed to enable the Funding Source section exists
	And the User is presented with the edit Funding Source page
	When the User chooses a Funding Source option
	And the User chooses to save
	Then the Order dashboard is presented
	And the Funding Source section is complete
	And the content validation status of the funding-sources section is complete
