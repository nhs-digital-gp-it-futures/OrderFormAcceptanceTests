Feature: Preview Order Summary
	As a Buyer
	I want to download a preview of my Order Summary
	So that I can include it in a contract

Scenario: Button to get Order Summary
	Given that the User is on the Order Summary
	And the User has not completed the Order
	When the User chooses to get the Preview Order Summary
	Then there is a button to get the Preview Order Summary at the top and bottom of it 

# Chrome update has scuppered this test
@ignore
Scenario: Choose to get Order Summary
	Given that the User is on the Order Summary
	And the User has not completed the Order
	And the User chooses to get the Preview Order Summary
	When the User chooses to print the Preview Order Summary
	Then a new tab will open
	And the tab will contain the printable version of the Order Summary
	And the Print Dialog within the Browser will appear automatically
