Feature: Logged In Buyer Display Name And Organisation Name
	As a Buyer
	I want the Organisation I am representing to be visible on the order form
	So that I know which Organisation's order I am managing

Scenario: Display Buyer display name and organisation on order form dashboard
	Then the page displays who is logged in and the primary organisation name
	And the new order page displays the logged in display name and organisation name
