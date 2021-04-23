Feature: Additional Services
	As a Buyer
	I want to manage the Additional Services of Order Form
	So that I can ensure the information is correct

Background:
	Given an incomplete order with catalogue items exists

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
	And there is the Additional Service section
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the Complete order button is disabled

Scenario: Additional Services - Catalogue Solution now complete, >= 1 Service Recipient, 0 Catalogue Solution
	Given the Catalogue Solutions section is not complete
	When the Order Form for the existing order is presented
	Then the Call Off Agreement ID is displayed
	And the Order description section is enabled
	And the Call-off Ordering Party section is enabled
	And the Supplier section is enabled
	And the Commencement date section is enabled 
	And the Catalogue Solution section is enabled
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the Complete order button is disabled

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
	And the User is presented with Additional Services available from their chosen Supplier
	And no Additional Service is selected
	When they choose to continue
	When the user selects an error link in the Error Summary
	Then they will be navigated to the relevant part of the page

Scenario: Additional Services - Go back from select an additional service
	Given the Order Form for the existing order is presented
	Given the User is presented with Additional Services available from their chosen Supplier
	When the User chooses to go back
	Then the Additional Service dashboard is presented

@ignore Need to rewrite this test
Scenario: Additional Services - Select a price for the Additional Service
	Given the Order Form for the existing order is presented
	And the User has selected an Additional Service to add
	When they choose to continue
	Then all the available prices for that Additional Service are presented

Scenario: Additional Services - No price for the Additional Service selected
	Given the Order Form for the existing order is presented
	And the available prices for the selected Additional Service are presented  
	When they choose to continue
	Then the Additional Service price is not saved
	And the reason is displayed

Scenario: Additional Services - Go back from select price
	Given the Order Form for the existing order is presented
	And the available prices for the selected Additional Service are presented 
	When the User chooses to go back
	Then the Additional Services of the Catalogue Solutions in the order is displayed
	And the User's selected Additional Service is selected

Scenario: Additional Services - Select service recipient - Select a Service Recipient
	Given the Order Form for the existing order is presented
	And the available prices for the selected Additional Service are presented
	And the User chooses to continue
	When they choose to continue 
	Then they are presented with the Service Recipients saved in the Order
	And the Additional Service name is displayed

Scenario: Additional Services - Select service recipient - No Service Recipient for the Additional Service selected
	Given the Order Form for the existing order is presented
	And the available prices for the selected Additional Service are presented
	And the User has selected a Additional Service price
	When no Service Recipient is selected
	And they choose to continue
	Then the User is informed they have to select a Service Recipient

Scenario: Additional Services - Select service recipient - Go back 
	Given the Order Form for the existing order is presented
	And the available prices for the selected Additional Service are presented
	And the User has selected a Additional Service price
	When the User chooses to go back
	Then the User is presented with the correct page

Scenario: Additional Services - Flat price with variable order type selected
	Given the User is on the Edit Price form
	Then they are presented with the Additional Service edit form for flat list price
	And the form contains one item
	And the Additional Service edit form contains an input for the price
	And the Additional Service edit form contains a unit of order
	And the Additional Service edit form contains an input for the quantity
	And the price input is autopopulated with the list price for the flat list price selected
	And the save button is enabled

Scenario: Additional Services - Mandatory data missing
	Given the User is on the Edit Price form
	When the User chooses to save
	Then the Additional Service is not saved
	And the reason is displayed

Scenario: Additional Services - Data exceeds the maximum length
	Given the User is on the Edit Price form
	When the quantity is above the max value
	And the User chooses to save
	Then the Additional Service is not saved 
	And the reason is displayed

Scenario: Additional Services - Validation Error Message Anchors
	Given the User is on the Edit Price form
	When the User chooses to save
	And the user selects an error link in the Error Summary
	Then they will be navigated to the relevant part of the page

Scenario: Additional Services - All data are valid
	Given the User is on the Edit Price form
	When all data is complete and valid
	And the User chooses to save	
	Then the Additional Service is saved
	And the content validation status of the additional-services section is complete

Scenario: Additional Services - Go back before save
	Given the User is on the Edit Price form
	When the User chooses to go back
	Then they are presented with the Planned delivery date

Scenario: Additional Services - Go back post save
	Given an additional service with a flat price variable Patient order type is saved to the order
	And the Order Form for the existing order is presented
	And the edit Additional Service form for flat list price with variable (patient numbers) order type is presented
	When the User chooses to go back
	Then the Additional Service dashboard is presented

Scenario: Additional Services - Values populated after editing and saving - Flat List Price Variable (Patient Numbers)
	Given an additional service with a flat price variable Patient order type is saved to the order
	And the edit Additional Service form for flat list price with variable (patient numbers) order type is presented	
	Then the pricing values will be populated with the values that was saved by the User

Scenario: Additional Services - Values populated after editing and saving - Flat List Price Declarative
	Given an additional service with a flat price variable Declarative order type is saved to the order
	And the Order Form for the existing order is presented
	And the edit Additional Service form for flat list price with declarative order type is presented
	Then the pricing values will be populated with the values that was saved by the User

Scenario: Additional Services - Values populated after editing and saving - Flat List Price Variable (On Demand)
	Given an additional service with a flat price variable On Demand order type with the quantity period per year is saved to the order
	And the edit Additional Service form for flat list price with variable (on demand) order type is presented
	Then the pricing values will be populated with the values that was saved by the User

Scenario: Additional Services added - View Additional Services
	Given there is one or more Additional Services added to the order
	And the User has chosen to manage the Additional Services section
	When the Additional Services dashboard is presented
	Then the Additional Services are presented
	And the name of each Additional Service is displayed
	And they are able to manage each Additional Service 

Scenario: Additional Services added - section marked as complete
	Given an additional service with a flat price variable Patient order type is saved to the order
	And the User has chosen to manage the Additional Services section
	And the Additional Services dashboard is presented
	When the User chooses to continue
	Then the Order dashboard is presented
	And the content validation status of the additional-services section is complete

Scenario: Additional Services - Published additional services display
	Given the Order Form for the existing order is presented
	And the User is able to manage the Additional Services section
	When the User chooses to add a single Additional Service
	Then only the published additional services are available for selection
