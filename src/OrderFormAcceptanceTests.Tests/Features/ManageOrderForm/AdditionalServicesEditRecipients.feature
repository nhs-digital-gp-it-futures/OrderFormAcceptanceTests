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

Scenario: Additional Services - Delete and Edit Buttons are disabled on validation
    Given the User is on the Edit Price form
    When the User chooses to save
    Then the Additional Service is not saved
    And the Delete Additional Button is disabled
    And the Edit Service Recipients Button is Disabled 
    And the Delete Additional Services Button is showing the correct text

Scenario: Additional Services - Enabled Delete Button shows correct Text
    Given the User chooses to edit a saved Additional Service
    When the user triggers a validation message 
    Then the Additional Service is not saved
    And the Delete Additional Button is enabled
    And the Delete Additional Services Button is showing the correct text

Scenario: Additional Services - Flat variable (on demand) list price selected - saved
    Given A User has saved the Additional Service to the order
    And the User has chosen to manage the Additional Services section
    And the User chooses to edit service recipients
    Then they are presented with the select Service Recipient form
    And the Service Recipient previously saved by the User for the Additional Service persists

Scenario: Additional Services - No selection or deselection of Service Recipient
    Given A User has saved the Additional Service to the order
    And the User has chosen to manage the Additional Services section
    And the User chooses to edit service recipients
    Then they are presented with the select Service Recipient form
    When they choose to continue
    Then the Edit Price form displays the same number of recipients as earlier 
    
Scenario: Additional Services - Newly selected Service Recipient
    Given A User has saved the Additional Service to the order
    And the User has chosen to manage the Additional Services section
    And the User chooses to edit service recipients
    Then they are presented with the select Service Recipient form
    And the User selects one or more new Service Recipients for the Additional Service
    When they choose to continue
    Then the Edit Price form displays the expected number of recipients
    And the Service Recipients are presented in ascending alphabetical order by Presentation Name

Scenario: Additional Services - Deselected Service Recipient
    Given A User has saved the Additional Service to the order
    And the User has chosen to manage the Additional Services section
    And the User chooses to edit service recipients
    Then they are presented with the select Service Recipient form
    When the User deselects one or more Service Recipients for the Additional Service
    And they choose to continue
    Then the deselected Service Recipients' record is removed from the table

Scenario: Additional Services - Go Back
   Given A User has saved the Additional Service to the order
    And the User has chosen to manage the Additional Services section
    And the User chooses to edit service recipients
    Then they are presented with the select Service Recipient form
    When they choose to go back
    Then the Edit Price form displays the same number of recipients as earlier 


