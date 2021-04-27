Feature: Additional Services - Delete an Additional Service
    As a Buyer
    I want to delete a Additional Service from my order
    So that the information is correct

Background:
    Given an incomplete order with catalogue items exists
    And an additional service with a flat price variable On Demand order type with the quantity period per year is saved to the order
    And the Order Form for the existing order is presented
    And the edit Additional Service form is displayed

Scenario: Delete an Additional Service - Ask User to confirm delete
    When the User chooses to delete the Additional Service 
    Then the User is asked to confirm the choice to delete the Additional Service
    And the Call-off Agreement ID is displayed
    And the order description is displayed

Scenario: Delete an Additional Service - Go back 
    When the User chooses to delete the Additional Service 
    And the User chooses to go back
    Then the edit Additional Service form is presented

Scenario: Delete an Additional Service - Not chosen to delete
    When the User chooses to delete the Additional Service
    And the User chooses not to delete the Additional Service
    Then the edit Additional Service form is presented

Scenario: Delete an Additional Service - User confirmed deleted
    When the User chooses to delete the Additional Service
    And the User chooses to confirm the delete
    Then the User is informed the Additional Service is deleted

Scenario: Delete an Additional Service - Additional Service dashboard
    When the User chooses to delete the Additional Service
    And the User chooses to confirm the delete
    And the User chooses to continue
    Then the Additional Service dashboard is presented
