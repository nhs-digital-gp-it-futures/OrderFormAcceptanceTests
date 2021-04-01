Feature: Delete Catalogue Solution
As a Buyer
I want to delete a Catalogue Solution from my order
So that the information is correct

Background:
	Given an incomplete order with catalogue items exists
    And the User has chosen to manage the Catalogue Solution section
    And the User chooses to edit a saved Catalogue Solution

Scenario: Delete Catalogue Solution - Ask User to confirm delete
    Given the delete button is enabled
    When the User chooses to delete the Catalogue Solution 
    Then the User is asked to confirm the choice to delete the catalogue solution
    And the Call-off Agreement ID is displayed
    And the order description is displayed

Scenario: Delete Catalogue Solution - Go back 
    Given the User chooses to delete the Catalogue Solution
    When the User chooses to go back
    Then they are presented with the Catalogue Solution edit form

Scenario: Delete Catalogue Solution - Not chosen to delete
    Given the User chooses to delete the Catalogue Solution
    When the User chooses not to delete the Catalogue Solution
    Then they are presented with the Catalogue Solution edit form

Scenario: Delete Catalogue Solution - User confirmed delete
    Given the User chooses to delete the Catalogue Solution
    When the User chooses to confirm the delete
    Then only the Catalogue Solution with the unit is deleted from the order
    And the User is informed the Catalogue Solution is deleted

Scenario: Delete Catalogue Solution - Catalogue Solution dashboard
    Given the User chooses to delete the Catalogue Solution
    And the User chooses to confirm the delete
    And the User is informed the Catalogue Solution is deleted
    When the User chooses to continue
    Then the Catalogue Solution dashboard is presented
    And the deleted Catalogue Solution with the unit is not on the Catalogue Solution dashboard
