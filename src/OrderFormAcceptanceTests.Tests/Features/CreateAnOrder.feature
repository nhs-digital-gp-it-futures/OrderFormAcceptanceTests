Feature: Create An Order
	As a Buyer
	I want to create an Order
	So that I can order from the Buying Catalogue

Background: 
	Given that a buyer user has logged in

Scenario: Create a new Order
	Given the User is presented with the Organisation's Orders dashboard
	When they choose to create a new Order
	Then the new Order is presented
	And the User is presented with a control to return to the Organisation's Orders dashboard
	And the User is unable to delete the order
	And the User is unable to preview the order summary
	And the User is unable to complete the order
	And there is alt text content on the disabled Delete order button
	And there is alt text content on the disabled Preview Order Summary button
	And there is alt text content on the disabled Complete button
