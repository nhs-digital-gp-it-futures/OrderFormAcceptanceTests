﻿Feature: Call Off Ordering Party
	As a Buyer
	I want to manage the Call Off Ordering Party part of Order Form
	So that the information is correct

Scenario: Call Off Ordering Party - Sections presented
	Given an incomplete order exists
	And the Call Off Ordering Party section is not complete
	When the Order Form for the existing order is presented
	Then there is the Call-off Ordering Party section
	And the user is able to manage the Call-off Ordering Party section

Scenario: Call Off Ordering Party - Order description section is now complete
	Given an incomplete order exists
	And the Call Off Ordering Party section is not complete
	When the Order Form for the existing order is presented
	Then the Call Off Agreement ID is displayed
	And there is the Order description section
	And the content validation status of the description section is complete 
	And there is the Call-off Ordering Party section
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the User is unable to complete the order

Scenario: Call Off Ordering Party - Name and Address autopopulated
	Given an incomplete order exists
	And the Call Off Ordering Party section is not complete
	When the User chooses to edit the Call Off Ordering Party information
	Then the Call Off Ordering Party ODS code is autopopulated from the User's organisation
	And the Call Off Ordering Party Organisation Name is autopopulated from the User's organisation
	And the Call Off Ordering Party Organisation Address is autopopulated from the User's organisation
	And the Call Off Agreement ID is displayed in the page title

Scenario: Call Off Ordering Party - Unable to edit Call Off Ordering Party Organisation ODS code
	Given an incomplete order exists
	And the Call Off Ordering Party section is not complete
	When the User chooses to edit the Call Off Ordering Party information
	Then the User is unable to edit the ODS code

Scenario: Call Off Ordering Party - Unable to edit Call Off Ordering Party Organisation Name
	Given an incomplete order exists
	And the Call Off Ordering Party section is not complete
	When the User chooses to edit the Call Off Ordering Party information
	Then the User is unable to edit the Organisation Name

Scenario: Call Off Ordering Party - Unable to edit Call Off Ordering Party Organisation Address
	Given an incomplete order exists
	And the Call Off Ordering Party section is not complete
	When the User chooses to edit the Call Off Ordering Party information
	Then the User is unable to edit Address

Scenario: Call Off Ordering Party - Go back
	Given an incomplete order exists
	And the Call Off Ordering Party section is not complete
	And the User chooses to edit the Call Off Ordering Party information
	When the User chooses to go back
	Then the Order dashboard is presented

Scenario: Call Off Ordering Party - Mandatory data missing
	Given the user is managing the Call Off Ordering Party section
	And mandatory data are missing 
	When the User chooses to save
	Then Call Off Ordering Party section is not saved
	And the reason is displayed

Scenario: Call Off Ordering Party - Data exceeds the maximum length
	Given the user is managing the Call Off Ordering Party section
	And the User has entered data into a field that exceeds the maximum length of 100 characters
	When the User chooses to save
	Then the Call Off Ordering Party section is not saved 
	And the reason is displayed

Scenario: Call Off Ordering Party - Validation Error Message Anchors
	Given the user is managing the Call Off Ordering Party section
	And the validation has been triggered
	When the user selects an error link in the Error Summary
	Then they will be navigated to the relevant part of the page

Scenario: Call Off Ordering Party - All data are valid
	Given the user is managing the Call Off Ordering Party section
	And the user has entered a valid Call Off Ordering Party contact for the order
	When the User chooses to save
	Then the Order is saved
	And the content validation status of the ordering-party section is complete 
	And the Call Off Agreement ID is generated
	And the Call Off Ordering Party section is saved in the DB
