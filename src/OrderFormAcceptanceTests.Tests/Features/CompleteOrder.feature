Feature: Complete Order

Scenario: Enable Complete Button - Funding Source now complete
	Given the order is complete enough so that the Complete order button is enabled with Funding Source option 'yes' selected
	And a catalogue solution has been added to the order
	When the Order Form for the existing order is presented
	Then the Call Off Agreement ID is displayed
	And the Order description is displayed
	And the Call-off Ordering Party section is enabled
	And the Supplier section is enabled
	And the Commencement date section is enabled 
	And the Catalogue Solution section is enabled
	And the Additional Service section is enabled
	And the Associated Service section is enabled
	And the Funding Source section is enabled
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the Complete order button is enabled

Scenario: Enable Complete Button - Funding Source now complete, 0 Catalogue Solution, >=1 Associated Service
	Given the order is complete enough so that the Complete order button is enabled with Funding Source option 'yes' selected
	When the Order Form for the existing order is presented
	Then the Call Off Agreement ID is displayed
	And the Order description is displayed
	And the Call-off Ordering Party section is enabled
	And the Supplier section is enabled
	And the Commencement date section is enabled 
	And the Catalogue Solution section is enabled
	And the Additional Service section is not enabled
	And the Associated Service section is enabled
	And the Funding Source section is enabled
	And the Preview order summary button is enabled
	And the Delete order button is enabled
	And the Complete order button is enabled

Scenario: Complete Order - Complete order screen if Funding Source was 'yes'
	Given the order is complete enough so that the Complete order button is enabled with Funding Source option 'yes' selected
	And the Order Form for the existing order is presented
	When the User chooses to complete the Order
	Then the confirm complete order screen is displayed
	And there is specific content related to the User answering 'yes' on the Funding Source question
	And the Call Off Agreement ID is displayed in the page title
	And the Order description is displayed
	And there is a control to complete order
	And there is a control to continue editing order

Scenario: Complete Order - Go back before complete order
	Given that the User is on the confirm complete order screen with Funding Source option 'yes' selected
	And the User has not completed the Order
	When the User chooses to go back
	Then the Order dashboard is presented

Scenario: Complete Order - Order completed if Funding Source was 'yes' confirmation screen
	Given that the User is on the confirm complete order screen with Funding Source option 'yes' selected
	When the User confirms to complete the Order
	Then the Order completed screen is displayed
	And the Call Off Agreement ID is displayed in the page title
	And there is specific content related to the User answering 'yes' on the Funding Source question on the completed screen

Scenario: Complete Order - Go Back after Order completed
	Given that the User has completed their Order
	When the User chooses to go back
	Then the Organisation Orders Dashboard is displayed

Scenario: Complete Order - Complete order screen if Funding Source was 'no'
	Given the order is complete enough so that the Complete order button is enabled with Funding Source option 'no' selected
	And the Order Form for the existing order is presented
	When the User chooses to complete the Order
	Then the confirm complete order screen is displayed
	And there is specific content related to the User answering 'no' on the Funding Source question
	And the Call Off Agreement ID is displayed in the page title
	And the Order description is displayed
	And there is a control to complete order
	And there is a control to continue editing order

Scenario: Complete Order - Order completed if Funding Source was 'no' confirmation screen
	Given that the User is on the confirm complete order screen with Funding Source option 'no' selected
	When the User confirms to complete the Order
	Then the Order completed screen is displayed
	And the Call Off Agreement ID is displayed in the page title
	And there is specific content related to the User answering 'no' on the Funding Source question on the completed screen
	And there is a control that allows the User to download a .PDF version of the Order Summary

Scenario: Complete Order - Indicate if automatically processed or not if Funding Source was Yes
	Given that the User is on the confirm complete order screen with Funding Source option 'yes' selected
	And the User confirms to complete the Order
	When the User chooses to go back
	And the Organisation Orders Dashboard is displayed
	And the Order is in the 'Completed Orders' table
	Then there is an indication that the Order has been processed automatically
	            
Scenario: Complete Order - Indicate if automatically processed or not if Funding Source was No
	Given that the User is on the confirm complete order screen with Funding Source option 'no' selected
	And the User confirms to complete the Order
	When the User chooses to go back
	And the Organisation Orders Dashboard is displayed
	And the Order is in the 'Completed Orders' table
	Then there is an indication that the Order has not been processed automatically

Scenario: Complete Order - Continue editing order
	Given that the User is on the confirm complete order screen with Funding Source option 'no' selected
	When the User chooses to continue editing order 
	Then the Order dashboard is presented
	And the Order is not completed

@ignore Order summary not working correctly
Scenario: View Completed Order Summary
	Given a User has completed an Order 
	When they choose to view the Completed Order from their Organisation's Orders Dashboard
	Then the Completed version of the Order Summary is presented
	And the completed order summary has specific content related to the order being completed
	And the completed order summary contains the date the Order was completed

@ignore Order summary not working correctly
Scenario: View Completed Order Summary - Button to get Order Summary
	Given a User has completed an Order
	When the Completed Order Summary is displayed
	Then there is a button to get the Order Summary at the top and bottom of it 
