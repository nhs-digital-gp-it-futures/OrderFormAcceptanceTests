Feature: Login - Authorisation - Order Form
	As a Buyer
	I want to to be authorised to use the Order Form
	So that I can use the Order Form if I have permission

Scenario: Login as a Buyer via Login
	Given that a User is not authenticated and the user logs in using the 'login' function
	When the User is a Buyer User
	Then the User will be logged in
	And the Buyer will be able to access the Order Form feature without having to authenticate again

Scenario: Login as a Buyer via Tile
	Given that a User is not authenticated and the user selects the order form tile
	And the User is prompted to login
	When the User is a Buyer User
	Then the User will be logged in
	And will be taken to the Order Form Feature

Scenario: Login as via Tile but not a buyer
	Given that a User is not authenticated and the user selects the order form tile
	And the User is prompted to login
	When the User is not a Buyer User
	Then the User will be logged in
	And the User will be informed they cannot access that feature