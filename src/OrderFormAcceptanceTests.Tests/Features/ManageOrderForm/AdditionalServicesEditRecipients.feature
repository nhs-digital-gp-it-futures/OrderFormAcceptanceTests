Feature: Additional Services Edit Recipients
    As a Buyer
    I want to be able to edit service recipient after saving the Additional Service with variable (patient numbers) order type to my order
    So that I can select correct service recipients for my order

Background:
    Given an incomplete order with catalogue items exists
    And an additional service with a flat price variable Patient order type is saved to the order
    And the User has chosen to manage the Additional Services section

Scenario: Additional Services Edit Recipients - Edit Service Recipients
    Given the User chooses to edit a saved Additional Service
    When the User chooses to edit the service recipients
    And the User adds another service recipient to the order
    And the User chooses to continue
    Then the Edit Price form displays the expected number of recipients

Scenario: Additional Services Edit Recipients - Edit Service Recipients - No Additional Recipients
    Given the User chooses to edit a saved Additional Service
    When the User chooses to edit the service recipients
    And the User chooses to continue
    Then the Edit Price form displays the same number of recipients as earlier

Scenario: Additional Services Edit Recipients - Edit Service Recipients - Go Back
    Given the User chooses to edit a saved Additional Service
    When the User chooses to edit the service recipients
    And the User chooses to go back
    Then the Edit Price form displays the same number of recipients as earlier
