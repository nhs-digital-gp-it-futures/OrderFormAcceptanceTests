Feature: Pass an Order Form's Purchasing Data to Finance System
	As an Authority User
	I want an Order Form's Purchasing Data to be passed to the Finance System for invoicing
	So that invoices are issued
@ignore
Scenario: Pass an Order Form's Purchasing Data
	Given that the User is on the confirm complete order screen with Funding Source option 'yes' selected
	When the User confirms to complete the Order
	Then a .CSV is sent to the specified mailbox
	And the .CSV to the desired specification is produced (call off-id only)
