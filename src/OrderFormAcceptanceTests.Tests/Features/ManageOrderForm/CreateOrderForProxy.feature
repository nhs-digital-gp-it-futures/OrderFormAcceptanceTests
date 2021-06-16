Feature: CreateOrderForProxy
	As a Proxy Buyer,
    I want to change organisations,
    So that I can create orders on behalf of other organisations

Scenario: Select Single Organisation
    Given the user is logged in as a proxy buyer
    When the user chooses to create manager orders
    Then the user is presented with a list of all organisations they can create orders for
    And no organisation is pre-selected

Scenario: Error Message
    Given that the user is presented with a list of organisations they can act on behalf of
    When the user chooses to continue without selecting an organisation
    Then an error message is presented

Scenario: Go Back 
    Given that the user is presented with a list of organisations they can act on behalf of
    When the user chooses to go back
    Then the organisation's order dashboard is presented

Scenario: Begin Order Form Journey
    Given that the user wants to create a new order
    And the list of organisations is presented
    When the user selects the organisation they want to act on behalf of
    Then the organisation's order dashboard is presented
