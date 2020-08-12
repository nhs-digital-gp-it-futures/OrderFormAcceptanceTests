Feature: Buyer View Orders
	As a Buyer
	I want to view my Orders
	So that they are correct

Scenario: View Orders
	Given an incomplete order exists
	When the User is presented with the Organisation's Orders dashboard
	Then there is a list of my Organisation's Orders
	And each item includes the Call Off Agreement ID
	And each item includes the Order Description
	And each item includes the Display Name of the User who made most recent edit
	And each item includes the date of the most recent edit
	And each item includes the date it was created
	And there is a table titled Incomplete orders
	And there is a table titled Completed orders
	And there is a control to go back to the homepage 
	And there is a control to create a new order

Scenario: View Orders - Footer 
	Given an incomplete order exists
	When the User is presented with the Organisation's Orders dashboard
	Then the Organisation's Orders dashboard contains the standard Public browse footer 
	And the new order page displays the standard Public browse footer 

Scenario: View Orders - Headers
	Given an incomplete order exists
	When the User is presented with the Organisation's Orders dashboard
	Then the Organisation's Orders dashboard contains the standard Public browse header including the Beta banner
	And the new order page displays the standard Public browse header

Scenario: View Orders - Go back to homepage
	Given an incomplete order exists
	When the User is presented with the Organisation's Orders dashboard
	When the User choose to go back to the homepage
	Then the Public Browse homepage is presented 
