Feature: Funding Source
	As a Buyer
	I want to manage the Funding Source of Order Form
	So that I can ensure the information is correct
	
@ignore
Scenario: Funding Source - Sections presented
	Given an unsubmitted order exists
	And there are no Service Recipients in the order
	And an Associated Service with a flat price declarative order type is saved to the order
	When the Order Form for the existing order is presented
	Then there is the Funding Source section
	And the User is able to manage the Funding Source section
@ignore
Scenario: Funding Source - Associated Service now complete, >=1 Service Recipient, >=1 Catalogue Solution, >=1 Additional Service, >=1 Associated Service
	Given an unsubmited order with catalogue items exists
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
@ignore
Scenario: Funding Source - Associated Service now complete, >= 1 Service Recipient, >= 1 Catalogue Solution, >=1 Additional Service, 0 Associated Service
Given the completed sections are Order Description
And Call-off Ordering Party
And Supplier
And Commencement Date
And Service Recipient
And Catalogue Solution
And Additional Service
And Associated Service
And there are one or more Service Recipients in the order
And there are one or more Catalogue Solutions in the order
And there are one or more Additional Services in the order
And there is no Associated Service in the order
When the Order dashboard is presented
Then the Call-off Agreement ID is displayed
And the Order description is displayed
And the enabled sections are Order description
And Call-off Ordering Party
And Supplier
And Commencement date
And Service Recipient
And Catalogue Solution
And Additional Service
And Associated Service
And Funding Source
And the Preview order summary button is enabled
And the Delete order button is enabled
And the Submit order button is disabled
@ignore
Scenario: Funding Source - Associated Service now complete, >=1 Service Recipient, >=1 Catalogue Solution, 0 Additional Service, >=1 Associated Service
Given the completed sections are Order Description
And Call-off Ordering Party
And Supplier
And Commencement Date
And Service Recipient
And Catalogue Solution
And Additional Service
And Associated Service
And there are one or more Service Recipients in the order
And there are one or more Catalogue Solutions in the order
And there is no Additional Service in the order
And there are one or more Associated Services in the order
When the Order dashboard is presented
Then the Call-off Agreement ID is displayed
And the Order description is displayed
And the enabled sections are Order description
And Call-off Ordering Party
And Supplier
And Commencement date
And Service Recipient
And Catalogue Solution
And Additional Service
And Associated Service
And Funding Source
And the Preview order summary button is enabled
And the Delete order button is enabled
And the Submit order button is disabled
@ignore
Scenario: Funding Source - Associated Service now complete, >= 1 Service Recipient, >= 1 Catalogue Solution, 0 Additional Service, 0 Associated Service
Given the completed sections are Order Description
And Call-off Ordering Party
And Supplier
And Commencement Date
And Service Recipient
And Catalogue Solution
And Additional Service
And Associated Service
And there are one or more Service Recipients in the order
And there are one or more Catalogue Solutions in the order
And there is no Additional Service in the order
And there is no Associated Service in the order
When the Order dashboard is presented
Then the Call-off Agreement ID is displayed
And the Order description is displayed
And the enabled sections are Order description
And Call-off Ordering Party
And Supplier
And Commencement date
And Service Recipient
And Catalogue Solution
And Additional Service
And Associated Service
And Funding Source
And the Preview order summary button is enabled
And the Delete order button is enabled
And the Submit order button is disabled
@ignore
Scenario: Funding Source - Associated Service now complete, 0 Service Recipient, >=1 Associated Service
Given the completed sections are Order Description
And Call-off Ordering Party
And Supplier
And Commencement Date
And Service Recipient
And Associated Service
And there is no Service Recipient in the order
And there are one or more Associated Services in the order
When the Order dashboard is presented
Then the Call-off Agreement ID is displayed
And the Order description is displayed
And the enabled sections are Order description
And Call-off Ordering Party
And Supplier
And Commencement date
And Service Recipient
And Associated Service
And Funding Source
And the Preview order summary button is enabled
And the Delete order button is enabled
And the Submit order button is disabled
@ignore
Scenario: Funding Source - Associated Service now complete, >=1 Service Recipient, 0 Catalogue Solution, >=1 Associated Service
Given the completed sections are Order Description
And Call-off Ordering Party
And Supplier
And Commencement Date
And Service Recipient
And Catalogue Solution
And Associated Service
And there are one or more Service Recipients in the order
And there is no Catalogue Solution in the order
And there are one or more Associated Services in the order
When the Order dashboard is presented
Then the Call-off Agreement ID is displayed
And the Order description is displayed
And the enabled sections are Order description
And Call-off Ordering Party
And Supplier
And Commencement date
And Service Recipient
And Catalogue Solution
And Associated Service
And Funding Source
And the Preview order summary button is enabled
And the Delete order button is enabled
And the Submit order button is disabled
@ignore
Scenario: Funding Source - Associated Service Complete:  0 Service Recipient, 0 Catalogue Solutions, 0 Associated Service
Given the completed sections are Order Description 
And Call-off Ordering Party
And Supplier
And Commencement Date
And Service Recipient
And Associated Service
And there is no Service Recipient in the order
And there is no Associated Service in the order
When the Order dashboard is presented
Then the Call-off Agreement ID is displayed
And the Order description is displayed
And the enabled sections are Order description
And Call-off Ordering Party
And Supplier
And Commencement date
And Service Recipient
And Associated Service 
And Catalogue Solution
And the Disabled sections are Additional Services
And Funding Source
And the Preview order summary button is enabled
And the Delete order button is enabled
And the Submit order button is disabled
@ignore
Scenario: Funding Source - Associated Service now complete, >= 1 Service Recipient, 0 Catalogue Solution, 0 Associated Service
Given the completed sections are Order Description
And Call-off Ordering Party
And Supplier
And Commencement Date
And Service Recipient
And Catalogue Solution
And Associated Service
And there is one or more Service Recipients in the order
And there is no Catalogue Solution in the order
And there is no Associated Service in the order
When the Order dashboard is presented
Then the Call-off Agreement ID is displayed
And the Order description is displayed
And the enabled sections are Order description
And Call-off Ordering Party
And Supplier
And Commencement date
And Service Recipient
And Catalogue Solution
And Associated Service
And the Disabled sections are Additional Services
And Funding Source
And the Preview order summary button is enabled
And the Delete order button is enabled
And the Submit order button is disabled