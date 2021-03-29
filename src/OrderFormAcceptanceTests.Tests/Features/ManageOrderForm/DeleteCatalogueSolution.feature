Feature: Delete Catalogue Solution
As a Buyer
I want to delete a Catalogue Solution from my order
So that the information is correct

Background:
	Given an incomplete order with catalogue items exists

Scenario: Delete Catalogue Solution - Ask User to confirm delete
Given the User has chosen to manage the Catalogue Solution section
And the User chooses to edit a saved Catalogue Solution
And the delete button is enabled
When the User chooses to delete the Catalogue Solution 
Then the User is asked to confirm the choice to delete the catalogue solution
And the Call-off Agreement ID is displayed
And the order description is displayed

Scenario: Go back 
Given the User doesn't make changes to the previously saved data on the edit Catalogue Solution form
And they choose to delete Catalogue Solution
And the confirm delete page is presented
When the User chooses to go back
Then the edit form is presented
And all the previously saved data on the edit form persists



