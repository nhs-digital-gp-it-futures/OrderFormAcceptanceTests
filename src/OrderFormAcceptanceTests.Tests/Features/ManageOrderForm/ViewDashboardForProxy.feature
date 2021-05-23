Feature: View Dashboard For Proxy
    As a Proxy Buyer,
    I want to view a specific organisation’s order dashboard,
    So that I can see the status of all orders

Scenario: View Dashboard For Proxy - Organisation displayed
    Given the user is logged in as a proxy buyer 
    When the user chooses to create and manager orders 
    Then the organisation's order dashboard is presented
    And the current organisation section is presented
    And there is a control to change organisation

Scenario: View Dashboard For Proxy - Change Organisation
    Given the user is on the organisation's order dashboard
    When the user chooses to change organisation
    Then the user is presented with a list of all organisations they can create orders for
    And no organisation is pre-selected

Scenario: View Dashboard For Proxy - View Another Organisation's Dashboard
    Given the list of organisations the user can create orders for is displayed
    And the user selects an organisation
    When the user chooses to continue
    Then they are presented with the selected organisation's orders dashboard
    And the correct selected organisation's name is displayed in the current organisation section

Scenario: View Dashboard For Proxy - Error Message
    Given the user is on the organisation's order dashboard
    When the user chooses to change organisation
    And the user chooses to continue without selecting an organisation
    Then an error message is presented

Scenario: View Dashboard For Proxy - Go Back
    Given the user has previously selected an organisation
    When the user chooses to change organization
    And the user chooses to go back 
    Then the correct selected organisation's name is displayed in the current organisation section

Scenario: View Dashboard For Proxy - Go Back Button Updated
    Given the user is on the change organisation page
    And the user has selected a customer Organisation to act on behalf of
    And the user chooses to manage orders for the organisation 
    When the user chooses to go back
    Then the user is taken back to a page with the correct Organisation ID in the URL
