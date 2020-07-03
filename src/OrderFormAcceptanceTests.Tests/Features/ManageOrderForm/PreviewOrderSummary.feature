Feature: Preview Order Summary
	As a Buyer
	I want to view a large version of the Order Summary
	So that I can preview the Order

Background: 
	Given an unsubmitted order exists
@ignore
Scenario: Preview Order Summary
	Given the Order Form for the existing order is presented
	When the User chooses to preview the Order Summary
	Then the Order Summary is presented
	And the Call Off Agreement ID is displayed
	And the Order description is displayed
	And the date the Order Summary was produced
	And the Call Off Ordering Party information is displayed
	And the Supplier information is displayed
	And the Commencement date is displayed
	And Order items (one-off cost) table is displayed
	And Order items (recurring cost) table is displayed
	And the total one-off cost is displayed
	And the total monthly cost is displayed
	And the total annual cost is displayed
	And the total cost of ownership is displayed
@ignore
Scenario: Preview Order Summary - Data presented
	Given the Order Summary is displayed
	Then it displays the Call-off Agreement ID data
	And the Order description data saved in the order
	And the Call-off Ordering Party data saved in the order
	And the Call-off Ordering Party names are concatenated
	And the Supplier data saved in the order
	And the Supplier first name and last name are concatenated
	And the Commencement date data saved in the order
@ignore
Scenario: Preview Order Summary - Go back
	Given the Order Summary is displayed
	When the User chooses to go back
	Then the Order dashboard is presented