Feature: Order Description section
	As a Buyer
	I want to manage the Order Description section
	So that the information is correct

Scenario: Display Order Description
	Given the User has chosen to manage a new Order Form
	When the Order Form is presented
	Then there is the Order description section
	And the user is able to manage the Order Description section
	
Scenario: Order Description - Mandatory data missing
	Given the user is managing the Order Description section
	And mandatory data are missing 
	When the User chooses to save
	Then Order Description section is not saved
	And the reason is displayed

Scenario 2: Data type is not valid
Given the User has entered data into a field
And the data type is not valid
When the User chooses to save
Then the Order Description section is not saved
And the reason is displayed

Scenario: Order Description - Data exceeds the maximum length
	Given the user is managing the Order Description section
	And the User has entered data into a field that exceeds the maximum length of 100 characters
	When the User chooses to save
	Then the Order Description section is not saved 
	And the reason is displayed

Scenario: Order Description - Validation Error Message Anchors
	Given the user is managing the Order Description section
	And the validation has been triggered
	When the user selects an error link in the Error Summary
	Then they will be navigated to the relevant part of the page

Scenario 6: All data are valid
Given all data are valid
When the User saves the Order Description section
Then the Order is saved
And the section is saved
And the content validation status of the section is Complete 
And the Call Off Agreement ID is generated
And the generated ID is set on the Order

Scenario 7: Section status
Given the User saves the Order Description section 
When the section has been successfully saved
Then the section content validation status of Complete is displayed on the Orders dashboard