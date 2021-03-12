Feature: Supplier Information
	As a Buyer
	I want to manage Supplier part of Order Form 
	So that the information is correct

Scenario: Supplier Information - Sections presented
	Given an incomplete order exists
	And the Supplier section is not complete
	When the Order Form for the existing order is presented
	Then there is the Supplier section
	And the user is able to manage the Supplier section

Scenario: Supplier Information - Call Off Ordering Party section is now complete but, Supplier not 
	Given an incomplete order exists
	And the Call Off Ordering Party section is complete
	And the Supplier section is not complete
	When the Order Form for the existing order is presented
	Then the Call Off Agreement ID is displayed
	And there is the Order description section
	And there is the Call-off Ordering Party section
	And the content validation status of the ordering-party section is complete 
	And there is the Supplier section
	And the content validation status of the supplier section is incomplete 
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the Complete order button is disabled

Scenario: Supplier Information - Supplier section is now complete but, Call Off Ordering Party not
	Given an incomplete order exists
	And a supplier has been selected
	And the Call Off Ordering Party section is not complete
	When the Order Form for the existing order is presented
	Then the Call Off Agreement ID is displayed
	And there is the Order description section
	And there is the Call-off Ordering Party section
	And the content validation status of the ordering-party section is incomplete 
	And there is the Supplier section
	And the content validation status of the supplier section is complete 
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the User is unable to submit the order

Scenario: Supplier Information - Search supplier 
	Given an incomplete order exists
	And the Supplier section is not complete
	When the User chooses to edit the Supplier section for the first time
	Then the Search supplier screen is presented
	And the Call Off Agreement ID is displayed in the page title

Scenario: Supplier Information - Supplier(s) returned
	Given an incomplete order exists
	And the Supplier section is not complete
	And the User chooses to edit the Supplier section for the first time
	When the User has entered a valid Supplier search criterion
	And the User chooses to search
	Then the matching Suppliers are presented

Scenario: Supplier Information - No Supplier(s) returned
	Given an incomplete order exists
	And the Supplier section is not complete
	And the User chooses to edit the Supplier section for the first time
	When the User has entered a non matching Supplier search criterion
	And the User chooses to search
	Then no matching Suppliers are presented
	And the User is informed that no matching Suppliers exist

Scenario: Supplier Information - Supplier name missing
	Given an incomplete order exists
	And the Supplier section is not complete
	And the User chooses to edit the Supplier section for the first time
	When the User has not entered a Supplier search criterion
	And the User chooses to search
	Then they are informed that a Supplier name needs to be entered

Scenario: Supplier Information - Go Back (Search Supplier)
	Given an incomplete order exists
	And the Supplier section is not complete
	And the User chooses to edit the Supplier section for the first time
	And the Search supplier screen is presented
	When the User chooses to go back
	Then the Order dashboard is presented

Scenario: Supplier Information - Go Back (No Supplier(s) returned)
	Given an incomplete order exists
	And the Supplier section is not complete
	And the User chooses to edit the Supplier section for the first time
	And the User has entered a non matching Supplier search criterion
	And the User chooses to search
	And no matching Suppliers are presented
	When the User chooses to go back
	Then the Search supplier screen is presented
	
Scenario: Supplier Information - Supplier selected
	Given the User has been presented with matching Suppliers
	When they select a Supplier that has saved contact details
	And they choose to continue
	Then the Edit Supplier Form Page is presented
	And the Supplier name is autopopulated
	And the Supplier Registered Address is autopopulated
	And the Supplier Contact details are autopopulated

Scenario: Supplier Information - No Supplier selected
	Given the User has been presented with matching Suppliers
	And no Supplier is selected
	When they choose to continue
	Then they are informed that a Supplier needs to be selected

Scenario: Supplier Information - Go Back (matching suppliers list)
	Given the User has been presented with matching Suppliers
	When the User chooses to go back
	Then the Search supplier screen is presented

Scenario: Supplier Information - Supplier selected (first time)
	Given the User has been presented with matching Suppliers
	When they select a Supplier
	And they choose to continue
	Then the Edit Supplier Form Page is presented
	And there is a control available to search again for a Supplier

Scenario: Supplier Information - Mandatory data missing
	Given the User has selected a supplier for the first time
	And mandatory data are missing 
	When the User chooses to save
	Then Supplier section is not saved
	And the reason is displayed

Scenario: Supplier Information - Data exceeds the maximum length
	Given the User has selected a supplier for the first time
	And the User has entered data into a field that exceeds the maximum length of 100 characters
	When the User chooses to save
	Then the Supplier section is not saved 
	And the reason is displayed

Scenario: Supplier Information - Validation Error Message Anchors
	Given the User has selected a supplier for the first time
	And mandatory data are missing
	And the validation has been triggered
	When the user selects an error link in the Error Summary
	Then they will be navigated to the relevant part of the page

Scenario: Supplier Information - All data are valid
	Given the User has selected a supplier for the first time
	And the user has entered a valid supplier contact for the order
	When the User chooses to save
	Then the Order is saved
	And the content validation status of the supplier section is complete
	And the Supplier section is saved in the DB

Scenario: Supplier Information - Search again for a Supplier
	Given the User has selected a supplier for the first time
	When the User chooses to search again for a Supplier
	Then the Search supplier screen is presented

Scenario: Supplier Information - Go Back (first time)
	Given the User has selected a supplier for the first time
	And mandatory data are missing 
	When the User chooses to go back
	Then the matching Suppliers are presented

Scenario: Supplier Information - Edit (subsequent times)
	Given an incomplete order exists
	And a supplier has been selected
	And the Order Form for the existing order is presented
	When the User re-edits the Supplier section
	Then the Edit Supplier Form Page is presented
	And there is not a control available to search again for a Supplier
	And the Supplier name is autopopulated
	And the Supplier Registered Address is autopopulated
	And the Supplier Contact details are autopopulated

Scenario: Supplier Information - Edit and save (subsequent times)
	Given an incomplete order exists
	And a supplier has been selected
	And the Order Form for the existing order is presented
	And the User re-edits the Supplier section
	And the user has entered a valid supplier contact for the order
	When the User chooses to save
	Then the Order is saved
	And the content validation status of the supplier section is complete
	And the Supplier section is saved in the DB

Scenario: Supplier Information - Go Back (subsequent times)
	Given an incomplete order exists
	And the Order Form for the existing order is presented
	And the User re-edits the Supplier section
	And the Edit Supplier Form Page is presented
	When the User chooses to go back
	Then the Order dashboard is presented

Scenario: Supplier Information - Visit search supplier page after section complete
	Given an incomplete order exists
	And a supplier has been selected
	And the Order Form for the existing order is presented
	When the User chooses to the visit the search supplier page
	Then they are redirected to the Edit Supplier page

Scenario: Supplier Information - Visit select supplier page after section complete
	Given an incomplete order exists
	And a supplier has been selected
	And the Order Form for the existing order is presented
	When the User chooses to visit the select supplier page
	Then they are redirected to the Edit Supplier page
