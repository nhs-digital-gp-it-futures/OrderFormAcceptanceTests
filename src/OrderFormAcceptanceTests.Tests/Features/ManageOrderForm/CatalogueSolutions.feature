Feature: Catalogue Solutions
	As a Buyer
	I want to manage the Catalogue Solutions of Order Form 
	So that the information is correct

Background:
	Given an incomplete order exists
	And the Supplier and Call Off Ordering Party sections are complete
	And the Commencement Date is complete

Scenario: Catalogue Solutions- Sections presented
	When the Order Form for the existing order is presented
	Then there is the Catalogue Solutions section
	And there is no Service Recipient section
	And the User is able to manage the Catalogue Solutions section

Scenario: Catalogue Solutions - Commencement date section is complete
	When the Order Form for the existing order is presented
	Then the Call Off Agreement ID is displayed
	And there is the Order description section
	And there is the Call-off Ordering Party section
	And there is the Supplier section
	And there is the Commencement date section
	And the content validation status of the commencement-date section is complete 
	And there is the Catalogue Solutions section
	And the content validation status of the catalogue-solutions section is incomplete 
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the Complete order button is disabled

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
	Then they are presented with select Service Recipient form
	And the Call Off Agreement ID is displayed in the page title
	And they can select one or more Service Recipients for the Catalogue Solution
	And the Call Off Ordering Party's Name (organisation name) and ODS code are presented as a Service Recipient

Scenario: Catalogue Solutions - No Service Recipient for the Catalogue Solution selected
	Given the User is presented with the Service Recipients for the Order after selecting the variable flat price
	And no Service Recipient is selected
	When they choose to continue
	Then the User is informed they have to select a Service Recipient

Scenario: Catalogue Solutions - Go back from select a service recipient
	Given the User is presented with the Service Recipients for the Order after selecting the variable flat price
	When the User chooses to go back
	Then all the available prices for that Catalogue Solution are presented
	And the User's selected price is selected

Scenario: Catalogue Solutions - edit price screen - Flat price with per patient order type selected
	Given the User is presented with the Service Recipients for the Order after selecting the per patient flat price
	And a Service Recipient is selected
	When they choose to continue
	And they choose to continue
	Then they are presented with the Catalogue Solution edit form
	And the name of the selected Catalogue Solution is displayed on the Catalogue Solution edit form
	And the Catalogue Solution edit form contains an input for the price
	And the item on the Catalogue Solution edit form contains an input for the practiceListSize
	And the item on the Catalogue Solution edit form contains an input for date
	And the price input is autopopulated with the list price for the flat list price selected
	And the save button is enabled

Scenario Outline: Catalogue Solutions - edit price screen - Flat price Mandatory data missing
	Given the User is presented with the Catalogue Solution edit form for a <ProvisioningType> flat price
	And mandatory data are missing
	When the User chooses to save
	Then the Catalogue Solution is not saved
	And the reason is displayed

	Examples:
		| ProvisioningType |
		| declarative      |
		| variable         |
		| per patient      |

Scenario Outline: Catalogue Solutions - edit price screen - Flat price Data type is not valid - invalid date
	Given the User is presented with the Catalogue Solution edit form for a <ProvisioningType> flat price
	And fills in the Catalogue Solution edit form with valid data for <quantityLabel>
	And the proposed date is an invalid date
	When the User chooses to save
	Then the Catalogue Solution is not saved
	And the reason is displayed

	Examples:
		| ProvisioningType | quantityLabel |
		| declarative      | quantity      |
		| variable         | quantity      |
		| per patient      | quantity      |

Scenario Outline: Catalogue Solutions - edit price screen - Flat price Data type is not valid - price with 5 decimal place
	Given the User is presented with the Catalogue Solution edit form for a <ProvisioningType> flat price
	And fills in the Catalogue Solution edit form with valid data for <quantityLabel>
	And the price has 5 decimal places
	When the User chooses to save
	Then the Catalogue Solution is not saved
	And the reason is displayed

	Examples:
		| ProvisioningType | quantityLabel |
		| declarative      | quantity      |
		| variable         | quantity      |
		| per patient      | quantity      |

@ignore negative prices do not throw error
Scenario Outline: Catalogue Solutions - edit price screen - Flat price Data type is not valid - price is negative
	Given the User is presented with the Catalogue Solution edit form for a <ProvisioningType> flat price
	And fills in the Catalogue Solution edit form with valid data for <quantityLabel>
	And the price is negative
	When the User chooses to save
	Then the Catalogue Solution is not saved
	And the reason is displayed

	Examples:
		| ProvisioningType | quantityLabel |
		| declarative      | quantity      |
		| variable         | quantity      |
		| per patient      | quantity      |

Scenario Outline: Catalogue Solutions - edit price screen - Flat price Data type is not valid - price does not allow characters
	Given the User is presented with the Catalogue Solution edit form for a <ProvisioningType> flat price
	And fills in the Catalogue Solution edit form with valid data for <quantityLabel>
	And the price contains characters
	When the User chooses to save
	Then the Catalogue Solution is not saved
	And the reason is displayed

	Examples:
		| ProvisioningType | quantityLabel |
		| declarative      | quantity      |
		| variable         | quantity      |
		| per patient      | quantity      |

Scenario Outline: Catalogue Solutions - edit price screen - Flat price Data type is not valid - quantity does not allow characters
	Given the User is presented with the Catalogue Solution edit form for a <ProvisioningType> flat price
	And fills in the Catalogue Solution edit form with valid data for <quantityLabel>
	And the <quantityLabel> quantity contains characters
	When the User chooses to save
	Then the Catalogue Solution is not saved
	And the reason is displayed

	Examples:
		| ProvisioningType | quantityLabel |
		| declarative      | quantity      |
		| variable         | quantity      |
		| per patient      | quantity      |


Scenario Outline: Catalogue Solutions - edit price screen - Flat price Data type is not valid - quantity does not allow decimals
	Given the User is presented with the Catalogue Solution edit form for a <ProvisioningType> flat price
	And fills in the Catalogue Solution edit form with valid data for <quantityLabel>
	And the <quantityLabel> quantity is a decimal
	When the User chooses to save
	Then the Catalogue Solution is not saved
	And the reason is displayed

	Examples:
		| ProvisioningType | quantityLabel |
		| declarative      | quantity      |
		| variable         | quantity      |
		| per patient      | quantity      |

Scenario Outline: Catalogue Solutions - edit price screen - Flat price Data type is not valid - quantity can not be negative
	Given the User is presented with the Catalogue Solution edit form for a <ProvisioningType> flat price
	And fills in the Catalogue Solution edit form with valid data for <quantityLabel>
	And the <quantityLabel> quantity is negative
	When the User chooses to save
	Then the Catalogue Solution is not saved
	And the reason is displayed

	Examples:
		| ProvisioningType | quantityLabel |
		| declarative      | quantity      |
		| variable         | quantity      |
		| per patient      | quantity      |

Scenario Outline: Catalogue Solutions - edit price screen - Flat price Data type is not valid - quantity exceeds the maximum length
	Given the User is presented with the Catalogue Solution edit form for a <ProvisioningType> flat price
	And fills in the Catalogue Solution edit form with valid data for <quantityLabel>
	And the <quantityLabel> quantity is over the max length
	When the User chooses to save
	Then the Catalogue Solution is not saved
	And the reason is displayed

	Examples:
		| ProvisioningType | quantityLabel |
		| declarative      | quantity      |
		| variable         | quantity      |
		| per patient      | quantity      |

Scenario Outline: Catalogue Solutions - edit price screen - Flat price Data type is not valid - price exceeds the maximum value
	Given the User is presented with the Catalogue Solution edit form for a <ProvisioningType> flat price
	And fills in the Catalogue Solution edit form with valid data for <quantityLabel>
	And the price is over the max value
	When the User chooses to save
	Then the Catalogue Solution is not saved
	And the reason is displayed

	Examples:
		| ProvisioningType | quantityLabel |
		| declarative      | quantity      |
		| variable         | quantity      |
		| per patient      | quantity      |

Scenario Outline: Catalogue Solutions - edit price screen - Flat price Validation Error Message Anchors
	Given the User is presented with the Catalogue Solution edit form for a <ProvisioningType> flat price
	And mandatory data are missing
	And the validation has been triggered
	When the user selects an error link in the Error Summary
	Then they will be navigated to the relevant part of the page

	Examples:
		| ProvisioningType |
		| declarative      |
		| variable         |
		| per patient      |

Scenario Outline: Catalogue Solutions - edit price screen - Flat price Delivery date validation - date is equal to 183 weeks after commencement date
	Given the User is presented with the Catalogue Solution edit form for a <ProvisioningType> flat price
	And fills in the Catalogue Solution edit form with valid data for <quantityLabel>
	And the User enters a Delivery Date that is equal to 183 weeks after the Commencement Date
	When the User chooses to save
	Then the Catalogue Solution is saved

	Examples:
		| ProvisioningType | quantityLabel |
		| declarative      | quantity      |
		| variable         | quantity      |
		| per patient      | quantity      |

Scenario Outline: Catalogue Solutions - edit price screen - Flat price Delivery date validation - date is less than 183 weeks after commencement date
	Given the User is presented with the Catalogue Solution edit form for a <ProvisioningType> flat price
	And fills in the Catalogue Solution edit form with valid data for <quantityLabel>
	And the User enters a Delivery Date that is less than 183 weeks after the Commencement Date
	When the User chooses to save
	Then the Catalogue Solution is saved

	Examples:
		| ProvisioningType | quantityLabel |
		| declarative      | quantity      |
		| variable         | quantity      |
		| per patient      | quantity      |

Scenario Outline: Catalogue Solutions - edit price screen - Flat price Delivery date validation - date is more than 183 weeks after commencement date
	Given the User is presented with the Catalogue Solution edit form for a <ProvisioningType> flat price
	And fills in the Catalogue Solution edit form with valid data for <quantityLabel>
	And the User enters a Delivery Date that is more than 183 weeks after the Commencement Date
	When the User chooses to save
	Then the Catalogue Solution is not saved
	And the reason is displayed

	Examples:
		| ProvisioningType | quantityLabel |
		| declarative      | quantity      |
		| variable         | quantity      |
		| per patient      | quantity      |

Scenario Outline: Catalogue Solutions - edit price screen - Flat price Delivery date validation - date cannot be before commencement date
	Given the User is presented with the Catalogue Solution edit form for a <ProvisioningType> flat price
	And fills in the Catalogue Solution edit form with valid data for <quantityLabel>
	And the User enters a Delivery Date that is before the Commencement Date
	When the User chooses to save
	Then the Catalogue Solution is not saved
	And the reason is displayed

	Examples:
		| ProvisioningType | quantityLabel |
		| declarative      | quantity      |
		| variable         | quantity      |
		| per patient      | quantity      |


Scenario Outline: Catalogue Solutions - edit price screen - Flat price All data are valid
	Given the User is presented with the Catalogue Solution edit form for a <ProvisioningType> flat price
	And fills in the Catalogue Solution edit form with valid data for <quantityLabel>
	When the User chooses to save
	Then the Catalogue Solution is saved

	Examples:
		| ProvisioningType | quantityLabel |
		| declarative      | quantity      |
		| variable         | quantity      |
		| per patient      | quantity      |

Scenario Outline: Catalogue Solutions - edit price screen - Flat price Price is displayed to a minimum of 2 decimal places
	Given the User is presented with the Catalogue Solution edit form for a <ProvisioningType> flat price
	Then the price is displayed to two decimal places

	Examples:
		| ProvisioningType |
		| declarative      |
		| variable         |
		| per patient      |

Scenario Outline: Catalogue Solutions - edit price screen - Flat price Go back before save
	Given the User is presented with the Catalogue Solution edit form for a <ProvisioningType> flat price
	When the User chooses to go back
	Then they are presented with the <expectedPage> for the order
	Examples:
	| ProvisioningType | expectedPage |
	| declarative      | Quantity     |
	| variable         | Quantity     |
	| per patient      | Delivery Date |

Scenario: Catalogue Solutions - View Added Catalogue Solutions
	Given a Catalogue Solution is added to the order
	And the User has chosen to manage the Catalogue Solution section
	When the Catalogue Solution dashboard is presented
	Then the Catalogue Solutions are presented
	And the name of the Catalogue Solution is displayed
	And there is a control to add a Catalogue Solution
	And there is a control to continue
	And there is a control to edit each Catalogue Solution

Scenario: Catalogue Solutions - Catalogue Solution added section complete
	Given the User has chosen to manage the Catalogue Solution section
	When the User chooses to continue
	Then the Order dashboard is presented
	And the content validation status of the catalogue-solutions section is complete

Scenario: Catalogue Solutions - Catalogue Solution displayed in alphabetical order
	When the User has chosen to manage the Catalogue Solution section
	And the Catalogue Solution dashboard is presented
	When the User chooses to add a single Catalogue Solution
	Then they are presented with the Catalogue Solutions available from their chosen Supplier
	And they are displayed in alphabetical order

@ignore PUT request broken
Scenario: Catalogue Solutions - Edit Added Solutions - Data saved after editing
	Given a User has added a per patient solution to the order
	And the User has chosen to manage the Catalogue Solution section
	And the User chooses to edit a saved Catalogue Solution
	And fills in the Catalogue Solution edit form with valid data for quantity
	When the User chooses to save
	Then the Catalogue Solution is saved

@ignore PUT request broken
Scenario: Catalogue Solutions - Edit Added Solutions - User informed invalid data
	Given a User has added a per patient solution to the order
	And the User has chosen to manage the Catalogue Solution section
	And the User chooses to edit a saved Catalogue Solution
	And the quantity is over the max length  
	When the User chooses to save
	Then the Catalogue Solution is not saved
	And the reason is displayed 

Scenario: Catalogue Solutions - Published solutions display
	Given the User has chosen to manage the Catalogue Solution section
	When the User chooses to add a single Catalogue Solution
	Then only the published catalogue solution are available for selection

Scenario: Catalogue Solutions - Multiple added solutions displayed alphabetically
	Given a User has added multiple solutions to the order
	And the User has chosen to manage the Catalogue Solution section
	When the Catalogue Solution dashboard is presented
	Then the Catalogue Solutions are presented
	And the Catalogue Solutions are in alphabetical order

@ignore Solution dashboard error
Scenario: Catalogue Solutions - Edit Service Recipients
	Given a Catalogue Solution is added to the order
	And the User has chosen to manage the Catalogue Solution section
	And the User chooses to edit a saved Catalogue Solution
	When the User chooses to edit the service recipients
	And the User adds another service recipient to the order
	And the User chooses to continue
	Then the Edit Price form displays the expected number of recipients

@ignore Solution dashboard error
Scenario: Catalogue Solutions - Edit Service Recipients - No Additional Recipients
	Given a Catalogue Solution is added to the order
	And the User has chosen to manage the Catalogue Solution section
	And the User chooses to edit a saved Catalogue Solution
	When the User chooses to edit the service recipients
	And the User chooses to continue
	Then the Edit Price form displays the same number of recipients as earlier

@ignore Solution dashboard error
Scenario: Catalogue Solutions - Edit Service Recipients - Go Back
	Given a Catalogue Solution is added to the order
	And the User has chosen to manage the Catalogue Solution section
	And the User chooses to edit a saved Catalogue Solution
	When the User chooses to edit the service recipients
	And the User chooses to go back
	Then the Edit Price form displays the same number of recipients as earlier
