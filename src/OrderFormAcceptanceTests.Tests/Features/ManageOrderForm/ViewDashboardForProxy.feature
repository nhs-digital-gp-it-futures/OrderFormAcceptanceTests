Feature: As a Proxy Buyer,
    I want to view a specific organisation’s order dashboard,
    So that I can see the status of all orders



Scenario: View Dashboard - Organisation displayed
    Given the user is logged in as a proxy buyer - create order 17
    When the user chooses to create and manager orders 
    Then the organisation's order dashboard is presented
    And the current organisation section is presented
    And there is a control to change organisation
    And the name of the user's organisation is presented 

Scenario: View Dashboard - Change Organisation
    Given the user is on the organisation's order dashboard
    When the user chooses to change organisation
    Then the user is presented with a list of all organisations they can create orders for
    And the user's own organisation is at the top of the list
    And all organisations below the user's own organisation are in alphabetical order
    And the user can only select 1 organisation
    And no organisation is pre-selected

Scenario: View Dashboard - View Another Organisation's Dashboard
    Given the list of organisations the user can create orders for is displayed,
    And the user selects an organisation,
    When the user chooses to continue,
    Then they are presented with the selected organisation's orders dashboard,
    And the selected organisation's name is displayed in the current organisation section,
    And the organisation's name is displayed on every page of the order form

Scenario: View Dashboard - Error Message
    Given the user is on the change organisation page
    When the user chooses to continue without selecting an organisation
    Then an error message is presented
    And the user cannot continue without selecting an organisation 

Scenario: View Dashboard - Go Back
    Given the user is on the change organisation page
    When the user chooses to go back
    Then the organisation's order dashboard is presented
    And there is no change to the organisation
