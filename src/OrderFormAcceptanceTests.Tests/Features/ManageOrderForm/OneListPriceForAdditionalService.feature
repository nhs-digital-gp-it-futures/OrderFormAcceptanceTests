Feature: One List Price For Additional Service Added
    As a Buyer
    I don't to want to have to select the list price of an Additional Service that has only one list price
    So that I save time and effort when completing my order

Background:
	Given an incomplete order with catalogue items exists
    When the Order Form for the existing order is presented
    Then there is the Additional Service section
	And the User is able to manage the Additional Services section
    
Scenario: One List Price For Additional Service Added - Select Service Recipient form presented
    Given the Select Additional Service form is presented
    And the User selects an Additional Service with only one list price
    When they choose to continue
    Then they are presented with select Service Recipient form

Scenario: One List Price For Additional Service Added - Go back
    Given the Select Additional Service form is presented
    And the User selects an Additional Service with only one list price
    And the Select Service Recipient form is presented
    When the User chooses to go back
    Then they are presented with the Select Additional Service form
    And their Additional Service selection persists
