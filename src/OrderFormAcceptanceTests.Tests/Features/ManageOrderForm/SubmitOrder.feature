Feature: Submit Order

#@ignore
Scenario: Enable Submit Button - Funding Source now complete, >=1 Catalogue Solution, >=0 Associated Services
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
	And the Submit order button is enabled
#@ignore
Scenario: Enable Submit Button - Funding Source now complete, >=1 Catalogue Solution, >=1 Associated Service
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
	And the Submit order button is enabled
#@ignore
Scenario: Enable Submit Button - Funding Source now complete, 0 Service Recipient, 0 Catalogue Solution, >=1 Associated Service
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
	And the Submit order button is enabled
#@ignore
Scenario: Enable Submit Button - Funding Source now complete, >=1 Service Recipient, 0 Catalogue Solution, >=1 Associated Service
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
	And the Submit order button is enabled