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

Scenario: Catalogue Solutions - Select a single Catalogue Solution to add
	Given the User has chosen to manage the Catalogue Solution section
	When the User chooses to add a single Catalogue Solution
	Then they are presented with the Catalogue Solutions available from their chosen Supplier
	And they can select one Catalogue Solution to add
	And the Call Off Agreement ID is displayed in the page title

Scenario: Catalogue Solutions - No Catalogue Solution selected
	Given the User is presented with Catalogue Solutions available from their chosen Supplier
	And no Catalogue Solution is selected
	When they choose to continue
	Then the User is informed they have to select a Catalogue Solution

Scenario: Catalogue Solutions - Go back from select a solution
	Given the User is presented with Catalogue Solutions available from their chosen Supplier
	When the User chooses to go back
	Then the Catalogue Solution dashboard is presented

Scenario: Catalogue Solutions - Select a price for the Catalogue Solution
	Given the User is presented with Catalogue Solutions available from their chosen Supplier
	And the User selects a catalogue solution to add
	When they choose to continue
	Then all the available prices for that Catalogue Solution are presented
	And they can select a price for the Catalogue Solution

Scenario: Catalogue Solutions - No price for the Catalogue Solution selected
	Given the User is presented with the prices for the selected Catalogue Solution 
	And no Catalogue Solution price is selected
	When they choose to continue
	Then the User is informed they have to select a Catalogue Solution price

Scenario: Catalogue Solutions - Go back from select price
	Given the User is presented with the prices for the selected Catalogue Solution
	When the User chooses to go back
	Then they are presented with the Catalogue Solutions available from their chosen Supplier

Scenario: Catalogue Solutions - Select a Service Recipient
	Given the User is presented with the prices for the selected Catalogue Solution
	And the User selects a price
	When they choose to continue 
	Then they are presented with the Service Recipients saved in the Order

Scenario: Catalogue Solutions - No Service Recipient for the Catalogue Solution selected
	Given the User is presented with the Service Recipients saved in the Order
	And no Service Recipient is selected
	When they choose to continue
	Then the User is informed they have to select a Service Recipient

Scenario: Catalogue Solutions - Go back from select a service recipient
	Given the User is presented with the Service Recipients saved in the Order
	When the User chooses to go back
	Then all the available prices for that Catalogue Solution are presented
@ignore
Scenario: Catalogue Solutions - edit price screen - Flat price with variable order type selected
	Given the User is presented with the Service Recipients saved in the Order
	And a Service Recipient is selected
	When they choose to continue
	Then they are presented with the Catalogue Solution edit form
	And the name of the selected Catalogue Solution is displayed on the Catalogue Solution edit form
	And the selected Service Recipient with their ODS code is displayed on the Catalogue Solution edit form
	And the Catalogue Solution edit form contains an input for the price
	And the item on the Catalogue Solution edit form contains a unit of order
	And the item on the Catalogue Solution edit form contains an input for the quantity
	And the item on the Catalogue Solution edit form contains an input for date
	And the item on the Catalogue Solution edit form contains a selection for the quantity estimation period 
	And the price input is autopopulated with the list price for the flat list price selected
	And the delete button is disabled
	And the save button is enabled
	@ignore
Scenario: Catalogue Solutions - edit price screen - Flat price Mandatory data missing
	Given the User is presented with the Catalogue Solution edit form
	And mandatory data are missing 
	When the User chooses to save
	Then the Catalogue Solution is not saved
	And the reason is displayed
	@ignore
Scenario: Catalogue Solutions - edit price screen - Flat price Data type is not valid - invalid date
	Given the User is presented with the Catalogue Solution edit form
	And the proposed date is an invalid date
	When the User chooses to save
	Then the Catalogue Solution is not saved
	And the reason is displayed
@ignore
Scenario: Catalogue Solutions - edit price screen - Flat price Data type is not valid - price with 4 decimal place
	Given the User is presented with the Catalogue Solution edit form
	And the price has 4 decimal places
	When the User chooses to save
	Then the Catalogue Solution is not saved
	And the reason is displayed
@ignore
Scenario: Catalogue Solutions - edit price screen - Flat price Data type is not valid - price is negative
	Given the User is presented with the Catalogue Solution edit form
	And the price is negative
	When the User chooses to save
	Then the Catalogue Solution is not saved
	And the reason is displayed
@ignore
Scenario: Catalogue Solutions - edit price screen - Flat price Data type is not valid - price does not allow characters
	Given the User is presented with the Catalogue Solution edit form
	And the price contains characters
	When the User chooses to save
	Then the Catalogue Solution is not saved
	And the reason is displayed
@ignore
Scenario: Catalogue Solutions - edit price screen - Flat price Data type is not valid - quantity does not allow characters
	Given the User is presented with the Catalogue Solution edit form
	And the quantity contains characters
	When the User chooses to save
	Then the Catalogue Solution is not saved
	And the reason is displayed
@ignore
Scenario: Catalogue Solutions - edit price screen - Flat price Data type is not valid - quantity does not allow decimals
	Given the User is presented with the Catalogue Solution edit form
	And the quanitity is a decimal
	When the User chooses to save
	Then the Catalogue Solution is not saved
	And the reason is displayed
@ignore
Scenario: Catalogue Solutions - edit price screen - Flat price Data type is not valid - quantity can not be negative
	Given the User is presented with the Catalogue Solution edit form
	And the quantity is negative
	When the User chooses to save
	Then the Catalogue Solution is not saved
	And the reason is displayed
@ignore
Scenario: Catalogue Solutions - edit price screen - Flat price Data exceeds the maximum length
Given the User has entered data into a field
And it exceeds the maximum length
When the User chooses to save
Then the Catalogue Solution is not saved 
And the reason is displayed
@ignore
Scenario: Catalogue Solutions - edit price screen - Flat price Validation Error Message Anchors
Given validation has been triggered
When the user selects an error link in the Error Summary
Then they will be navigated to the relevant part of the page
@ignore
Scenario: Catalogue Solutions - edit price screen - Flat price Delivery date is equal to 183 weeks after commencement date
Given there is a Commencement Date
And the Delivery Date cannot be later than 183 weeks after the Commencement Date
When the User enters a Delivery Date that is equal to 183 weeks after the Commencement Date
Then the Delivery Date is valid
@ignore
Scenario: Catalogue Solutions - edit price screen - Flat price Delivery date is less than 183 weeks after commencement date
Given there is a Commencement Date
And the Delivery Date cannot be later than 183 weeks after the Commencement Date
When the User enters a Delivery Date that is less than 183 weeks after the Commencement Date
Then the Delivery Date is valid
@ignore
Scenario: Catalogue Solutions - edit price screen - Flat price Delivery date is more than 183 weeks after commencement date
Given there is a Commencement Date
And the Delivery Date cannot be later than 183 weeks after the Commencement Date
When the User enters a Delivery Date that is more than 183 weeks after the Commencement Date
Then the Delivery Date is invalid
@ignore
Scenario: Catalogue Solutions - edit price screen - Flat price Delivery Date cannot be before commencement date
Given there is a Commencement Date
And the Delivery Date cannot be before the Commencement Date
When the User enters a Delivery Date that is before the Commencement Date
Then the Delivery Date is invalid
@ignore
Scenario: Catalogue Solutions - edit price screen - Flat price All data are valid
Given all the data are valid
When the User saves the Catalogue Solution
Then the Catalogue Solution is saved
And the content validation status of the Catalogue Solution is Complete
And the section content validation status of Complete is displayed on the Orders dashboard
And the delete button is enabled
@ignore
Scenario: Catalogue Solutions - edit price screen - Flat price values populated after editing and saving
Given that the User has amended a price
And entered a quantity
And selected a period
And saved the Solution
And entered a date
And navigated away from the Catalogue Solution
When the User re-visits the Catalogue Solution
Then the price value will be populated with the value that was saved by the User
And the quantity value will be populated with the value that was saved by the User
And the period selection will be the value that was selected by the User
And the date value will be the value that was saved by the User
@ignore
Scenario: Catalogue Solutions - edit price screen - Flat price Price is displayed to a minimum of 2 decimal places 
Given that a Price is to less than two decimal places
When it is displayed
Then it is displayed to two decimal places
@ignore
Scenario: Catalogue Solutions - edit price screen - Flat price Go back before save
Given the edit Catalogue Solution form for flat list price with variable (patient numbers) order type is presented
And the User hasn't saved the Catalogue Solution
When the User chooses to go back
Then the select service recipient page is presented
And the User's selected service recipient is selected
@ignore
Scenario: Catalogue Solutions - edit price screen - Flat price Go back post save
Given the edit Catalogue Solution form for flat list price with variable (patient numbers) order type is presented
And the User has saved the Catalogue Solution
When the User chooses to go back
Then the Catalogue Solution Dashboard is displayed
@ignore
Scenario: Catalogue Solutions - edit price screen - Flat price Select quantity period
Given the User can select a quantity period
When estimating quantity
Then the User can choose the quantity period as Per Year or Per Month
