Feature: Logged In Buyer Display Name And Organisation Name
	As a Buyer
	I want the Organisation I am representing to be visible on the order form
	So that I know which Organisation's order I am managing

	Background: 
	Given that a buyer user has logged in

Scenario: Display Buyer display name and organisation on order form dashboard
	Given the User is presented with the Organisation's Orders dashboard
	Then the page displays who is logged in and the primary organisation name
	And the new order page displays the logged in display name and organisation name
