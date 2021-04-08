Feature: Additional Services Ordering
    As a Buyer
    I want the Additional Services for the Catalogue Solutions in my order to be displayed in alphabetical order
    So that I can easily identify them

Scenario: Additional Services displayed in alphabetical order
    Given an incomplete order exists
    And the supplier has multiple Additional Services
    And the Catalogue Solution for the Additional Services has been added
    And the Order Form for the existing order is presented
    When the User has chosen to manage the Additional Service section
    And the User chooses to add a single Additional Service
    Then the Additional Services are displayed in alphabetical order
