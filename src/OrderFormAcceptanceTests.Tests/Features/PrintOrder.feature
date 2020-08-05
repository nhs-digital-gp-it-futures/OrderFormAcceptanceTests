Feature: Print Order

@ignore
Scenario: Choose to Download PDF from the Order completed screen
	Given that the User is on the confirm complete order screen
	And the Funding Source section is complete with 'no' selected
	And the User chooses to complete the Order
	And the Order completed screen is displayed
	When the User chooses to download a PDF of their Order Summary
	Then a new tab will open
	And the tab will contain the printable version of the Order Summary
	And the Print Dialog within the Browser will appear automatically
