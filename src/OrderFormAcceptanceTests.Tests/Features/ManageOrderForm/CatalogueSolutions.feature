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