Feature: Delete Order
	As a Buyer
	I want to delete an Order
	So that it cannot be progressed on the Buying Catalogue

Scenario: Delete Order - Confirm delete order 
	Given an incomplete order exists
	And the Order Form for the existing order is presented
	When the User chooses to delete the order
	Then the User is asked to confirm the choice to delete
	And the Call Off Agreement ID is displayed in the page title
	And the Order description is displayed

Scenario: Delete Order - Go back before deleting
	Given the confirm delete page is displayed
	When the User chooses to go back
	Then the Order dashboard is presented

@ignore Delete confirmation screen not working
Scenario: Delete Order - Order Deleted
	Given the confirm delete page is displayed
	When the User chooses to delete
	Then the Order is deleted
	And the User is informed that the Order has been deleted
	And the Order has a Deleted status
	And the Order is not on the Organisation's Orders Dashboard

Scenario: Delete Order - Not chosen to delete order
	Given the confirm delete page is displayed
	When the User chooses not to delete the Order
	Then the Order dashboard is presented
	And the status of the Order does not change to deleted

@ignore Delete confirmation screen not working
Scenario: Delete Order - Go back to all orders after deleting
	Given the Order deleted page is presented
	When the User chooses to go back
	Then the Organisation Orders Dashboard is displayed
