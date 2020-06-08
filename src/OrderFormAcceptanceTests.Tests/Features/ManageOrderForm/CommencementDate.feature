Feature: Commencement Date
	As a Buyer
	I want to manage the Commencement Date of Order Form
	So that I can ensure the information is correct

Background:
	Given an unsubmitted order exists
	When the Order Form for the existing order is presented

Scenario: Commencement Date - Sections presented	
	Then there is a list of sections
	And the user is able to manage the Commencement Date section

Scenario: Commencement Date - Order Description, Supplier and Call Off Ordering Party sections complete	
	Then the Call Off Agreement ID is displayed
	And there is the Order description section
	And the enabled sections are Order description
	And Call Off Ordering Party
	And Supplier
	And Commencement date
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the Submit order button is disabled

Scenario: Commencement Date - Mandatory data missing
	When the user chooses to manage the Commencement Date Section
	And the User chooses to save
	Then Commencement Date section is not saved
	And the reason is displayed

Scenario: Commencement Date - Data type is not valid
	Given the user chooses to manage the Commencement Date Section
	And the Day is set to <Day>
	And the Month is set to <Month>
	And the Year is set to <Year>
	When the User chooses to save
	Then the Commencement Date section is not saved 
	And the reason is displayed

	Examples: 
	| Day    | Month  | Year   |
	| 12     | 12     | banana |
	| 12     | banana | 2020   |
	| banana | 12     | 2020   |

Scenario Outline: Commencement Date - Data exceeds the maximum length
	Given the user chooses to manage the Commencement Date Section
	And the Day is set to <Day>
	And the Month is set to <Month>
	And the Year is set to <Year>
	When the User chooses to save
	Then the Commencement Date section is not saved 
	And the reason is displayed

	Examples: 
	| Day | Month | Year  |
	| 12  | 12    | 20202 |
	| 12  | 125   | 2020  |
	| 123 | 12    | 2020  |

Scenario: Commencement Date - Data are less than the minimum length
	Given the user chooses to manage the Commencement Date Section
	And the Day is set to <Day>
	And the Month is set to <Month>
	And the Year is set to <Year>
	When the User chooses to save
	Then the Commencement Date information is not saved 
	And the reason is displayed	
	And the date remains <Day>, <Month> and <Year>

	Examples: 
	| Day | Month | Year |
	| 12  | 12    | 202  |
	| 12  |       | 2020 |
	|     | 12    | 2020  |

Scenario: Commencement Date - Validation Error Message Anchors
	When the user chooses to manage the Commencement Date Section
	And the User chooses to save
	And the user selects an error link in the Error Summary
	Then they will be navigated to the relevant part of the page

Scenario: Commencement Date - All data are valid
	Given the user chooses to manage the Commencement Date Section
	And a valid date is entered
	When the User chooses to save
	Then the content validation status of the commencement-date section is complete

Scenario: Commencement Date - Earliest Commencement Date is not 60 days before today
	Given the user chooses to manage the Commencement Date Section
	And the Commencement Date entered is 59 days earlier than today's date
	When the User chooses to save
	Then the content validation status of the commencement-date section is complete

Scenario: Commencement Date - Earliest Commencement Date is 60 days before today
	Given the user chooses to manage the Commencement Date Section
	And the Commencement Date entered is 60 days earlier than today's date
	When the User chooses to save
	Then the section is not saved
	And the reason is displayed	

Scenario: Commencement Date - Go Back
	Given the user chooses to manage the Commencement Date Section
	When the User chooses to go back
	Then the Order dashboard is presented