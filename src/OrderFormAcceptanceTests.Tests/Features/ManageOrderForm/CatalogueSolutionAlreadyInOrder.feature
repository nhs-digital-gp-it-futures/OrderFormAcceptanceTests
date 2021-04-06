Feature: CatalogueSolutionAlreadyInOrder
As a Buyer
I don't want to be able to add a duplicate Catalogue Solution for the same Service Recipient to my order

Background:
	Given an incomplete order with catalogue items exists
    And the User has chosen to manage the Catalogue Solution section

Scenario: Catalogue Solution Already In Order - Catalogue Solution 
    Given the User selects a Catalogue Solution previously saved in the Order
    When they choose to continue 
    Then they are presented with the Edit Catalogue Solution form for the order type
    And the previously saved data is displayed

    Scenario: Catalogue Solution Already In Order - Go Back
	Given the User selects a Catalogue Solution previously saved in the Order
    When they choose to continue 
    And the edit Catalogue Solution form is presented
    When they choose to go back
    Then the select Catalogue Solution form is presented 
    And the previously selected Catalogue Solution persist
