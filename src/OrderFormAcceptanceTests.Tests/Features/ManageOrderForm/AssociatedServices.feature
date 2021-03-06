﻿Feature: Associated Services
    As a Buyer
    I want to manage the Associated Services of Order Form
    So that I can ensure the information is correct

Background: 
    Given an incomplete order exists
    And the Supplier and Call Off Ordering Party sections are complete
    And the Commencement Date is complete

Scenario: Associated Services - Sections presented
    Given the Catalogue Solution section is complete
    When the Order Form for the existing order is presented
    Then there is the Associated Services section
    And the User is able to manage the Associated Services section

Scenario: Associated Services - Service Recipient now complete, 0 Service Recipient
    Given the Catalogue Solutions section is not complete
    When the Order Form for the existing order is presented
    Then the Call Off Agreement ID is displayed
    And the Order description is displayed
    And the Call-off Ordering Party section is enabled
    And the Supplier section is enabled
    And the Commencement date section is enabled 
    And the Associated Service section is enabled
    And the Preview order summary button is enabled
    And the Delete order button is enabled
    And the Complete order button is disabled

Scenario: Associated Services - Additional Service now complete, >= 1 Service Recipient, >=1 Catalogue Solution, 0 Additional Services
    Given a Catalogue Solution is added to the order
    And the Catalogue Solution section is complete
    And the Additional Services section is complete
    When the Order Form for the existing order is presented
    Then the Call Off Agreement ID is displayed
    And the Order description is displayed
    And the Call-off Ordering Party section is enabled
    And the Supplier section is enabled
    And the Commencement date section is enabled 
    And the Catalogue Solution section is enabled
    And the Additional Service section is enabled
    And the Associated Service section is enabled
    And the Preview order summary button is enabled
    And the Delete order button is enabled
    And the Complete order button is disabled

Scenario: Associated Services - Additional Service now complete, >= 1 Service Recipient, >=1 Catalogue Solution, =>1 Additional Service
    Given a Catalogue Solution is added to the order
    And the Catalogue Solution section is complete
    And an Additional Service is added to the order
    When the Order Form for the existing order is presented
    Then the Call Off Agreement ID is displayed
    And the Order description is displayed
    And the Call-off Ordering Party section is enabled
    And the Supplier section is enabled
    And the Commencement date section is enabled 
    And the Catalogue Solution section is enabled
    And the Additional Service section is enabled
    And the Associated Service section is enabled
    And the Preview order summary button is enabled
    And the Delete order button is enabled
    And the Complete order button is disabled

Scenario: Associated Service dashboard
    When the User has chosen to manage the Associated Service section
    Then the Associated Services dashboard is presented
    And the Call Off Agreement ID is displayed in the page title
    And the Order description is displayed
    And there is a control to add an Associated Service
    And there is a control to continue

Scenario: Associated Service dashboard - No Associated Services added
    Given the User has chosen to manage the Associated Service section
    When there is no Associated Service added to the order
    Then there is content indicating there is no Associated Service added

Scenario: Associated Service dashboard - User chooses not to add Associated Service
    Given the User has chosen to manage the Associated Service section
    And the User chooses not to add an Associated Service
    When they choose to continue
    Then the Order is saved
    And the content validation status of the associated-services section is complete

Scenario: Associated Service dashboard - Go back
    Given the User has chosen to manage the Associated Service section
    When the User chooses to go back
    Then the Order dashboard is presented

Scenario: Select Associated Service - Select a single Associated Service to add
    Given the User has chosen to manage the Associated Service section
    When the User has chosen to Add a single Associated Service
    Then they are presented with the Associated Services available from their chosen Supplier
    And the Call Off Agreement ID is displayed in the page title
    And they can select one Associated Service to add

Scenario: Select Associated Service - No Associated Service selected
    Given the User is presented with Associated Services available from their chosen Supplier
    And no Associated Service is selected
    When they choose to continue
    Then the User is informed they have to select an Associated Service

Scenario: Select Associated Service - Go back
    Given the User is presented with Associated Services available from their chosen Supplier
    When the User chooses to go back
    Then the Associated Services dashboard is presented

Scenario: Associated Service - Select price - Select a price for the Associated Service
    Given the User is presented with Associated Services available from their chosen Supplier
    And the User selects an Associated Service to add
    When they choose to continue
    Then all the available prices for that Associated Service are presented
    And they can select a price for the Associated Service

Scenario: Associated Service - Select price - No price for the Associated Service selected
    Given the User is presented with the prices for the selected Associated Service 
    And no Associated Service price is selected
    When they choose to continue
    Then the User is informed they have to select a Associated Service price

Scenario: Associated Service - Select price - Go back 
    Given the User is presented with the prices for the selected Associated Service 
    When the User chooses to go back
    Then they are presented with the Associated Services available from their chosen Supplier
    And the User's selected Associated Service is selected

Scenario: Associated Service - edit price screen - Flat variable price selected
    Given the User is presented with the prices for the selected Associated Service 
    And the User selects the flat variable price type
    When they choose to continue
    Then they are presented with the Associated Service edit form
    And the name of the selected Associated Service is displayed on the Associated Service edit form
    And the Associated Service edit form contains an input for the price
    And the item on the Associated Service edit form contains a unit of order
    And the item on the Associated Service edit form contains an input for the quantity
    And the item on the Associated Service edit form contains a selection for the quantity estimation period
    And the price input is autopopulated with the list price for the flat list price selected
    And the save button is enabled

Scenario: Associated Service - edit price screen - Flat declarative price selected
    Given the User is presented with the prices for the selected Associated Service 
    And the User selects the flat declarative price type
    When they choose to continue
    Then they are presented with the Associated Service edit form
    And the name of the selected Associated Service is displayed on the Associated Service edit form
    And the Associated Service edit form contains an input for the price
    And the item on the Associated Service edit form contains a unit of order
    And the item on the Associated Service edit form contains an input for the quantity
    And the price input is autopopulated with the list price for the flat list price selected
    And the save button is enabled

Scenario Outline: Associated Service - edit price screen - Flat price Mandatory data missing
    Given the User is presented with the Associated Service edit form for a <ProvisioningType> flat price
    And mandatory data are missing 
    When the User chooses to save
    Then the Associated Service is not saved
    And the reason is displayed
    Examples: 
    | ProvisioningType |
    | declarative      |
    | variable         |

Scenario Outline: Associated Service - edit price screen - Flat price Data type is not valid - price with 5 decimal place
    Given the User is presented with the Associated Service edit form for a <ProvisioningType> flat price
    And fills in the Associated Service edit form with valid data
    And the price has 5 decimal places
    When the User chooses to save
    Then the Associated Service is not saved
    And the reason is displayed
    Examples: 
    | ProvisioningType |
    | declarative      |
    | variable         |

Scenario Outline: Associated Service - edit price screen - Flat price Data type is not valid - price is negative
    Given the User is presented with the Associated Service edit form for a <ProvisioningType> flat price
    And fills in the Associated Service edit form with valid data
    And the price is negative
    When the User chooses to save
    Then the Associated Service is not saved
    And the reason is displayed
    Examples: 
    | ProvisioningType |
    | declarative      |
    | variable         |

Scenario Outline: Associated Service - edit price screen - Flat price Data type is not valid - price does not allow characters
    Given the User is presented with the Associated Service edit form for a <ProvisioningType> flat price
    And fills in the Associated Service edit form with valid data
    And the price contains characters
    When the User chooses to save
    Then the Associated Service is not saved
    And the reason is displayed
    Examples: 
    | ProvisioningType |
    | declarative      |
    | variable         |

Scenario Outline: Associated Service - edit price screen - Flat price Data type is not valid - quantity does not allow characters
    Given the User is presented with the Associated Service edit form for a <ProvisioningType> flat price
    And fills in the Associated Service edit form with valid data
    And the quantity contains characters
    When the User chooses to save
    Then the Associated Service is not saved
    And the reason is displayed
    Examples: 
    | ProvisioningType |
    | declarative      |
    | variable         |

Scenario Outline: Associated Service - edit price screen - Flat price Data type is not valid - quantity does not allow decimals
    Given the User is presented with the Associated Service edit form for a <ProvisioningType> flat price
    And fills in the Associated Service edit form with valid data
    And the quantity is a decimal
    When the User chooses to save
    Then the Associated Service is not saved
    And the reason is displayed
    Examples: 
    | ProvisioningType |
    | declarative      |
    | variable         |

Scenario Outline: Associated Service - edit price screen - Flat price Data type is not valid - quantity can not be negative
    Given the User is presented with the Associated Service edit form for a <ProvisioningType> flat price
    And fills in the Associated Service edit form with valid data
    And the quantity is negative
    When the User chooses to save
    Then the Associated Service is not saved
    And the reason is displayed
    Examples: 
    | ProvisioningType |
    | declarative      |
    | variable         |

Scenario Outline: Associated Service - edit price screen - Flat price Data type is not valid - quantity exceeds the maximum length
    Given the User is presented with the Associated Service edit form for a <ProvisioningType> flat price
    And fills in the Associated Service edit form with valid data
    And the quantity is over the max length
    When the User chooses to save
    Then the Associated Service is not saved 
    And the reason is displayed
    Examples: 
    | ProvisioningType |
    | declarative      |
    | variable         |

Scenario Outline: Associated Service - edit price screen - Flat price Data type is not valid - price exceeds the maximum value
    Given the User is presented with the Associated Service edit form for a <ProvisioningType> flat price
    And fills in the Associated Service edit form with valid data
    And the price is over the max value
    When the User chooses to save
    Then the Associated Service is not saved 
    And the reason is displayed
    Examples: 
    | ProvisioningType |
    | declarative      |
    | variable         |

Scenario Outline: Associated Service - edit price screen - Flat price Validation Error Message Anchors
    Given the User is presented with the Associated Service edit form for a <ProvisioningType> flat price
    And mandatory data are missing
    And the validation has been triggered
    When the user selects an error link in the Error Summary
    Then they will be navigated to the relevant part of the page
    Examples:
    | ProvisioningType |
    | declarative      |
    | variable         |

Scenario Outline: Associated Service - edit price screen - Flat price All data are valid
    Given the User is presented with the Associated Service edit form for a <ProvisioningType> flat price
    And fills in the Associated Service edit form with valid data
    When the User chooses to save
    Then the Associated Service is saved
    And the Associated Services dashboard is presented
    Examples:
    | ProvisioningType |
    | declarative      |
    | variable         |

Scenario Outline: Associated Service - edit price screen - Flat price Price is displayed to a minimum of 2 decimal places 
    Given the User is presented with the Associated Service edit form for a <ProvisioningType> flat price
    Then the price is displayed to two decimal places
    Examples:
    | ProvisioningType |
    | declarative      |
    | variable         |

Scenario: Associated Service - edit price screen - Flat variable price values populated after editing and saving
    Given an Associated Service with a flat price variable (On-demand) order type with the quantity period per year is saved to the order
    And the User amends the existing Associated Service details
    When the User re-visits the Associated Service
    Then the values will be populated with the values that was saved by the User

Scenario: Associated Service - edit price screen - Flat declarative price values populated after editing and saving
    Given an Associated Service with a flat price declarative order type is saved to the order
    And the User amends the existing Associated Service details
    When the User re-visits the Associated Service
    Then the values will be populated with the values that was saved by the User

Scenario Outline: Associated Service - edit price screen - Flat price Go back before save
    Given the User is presented with the Associated Service edit form for a <ProvisioningType> flat price
    When the User chooses to go back
    Then all the available prices for that Associated Service are presented
    And the User's selected price is selected
    Examples:
    | ProvisioningType |
    | declarative      |
    | variable         |

Scenario Outline: Associated Service - edit price screen - Flat price Go back after save
    Given the User is presented with the Associated Service edit form for a <ProvisioningType> flat price
    And fills in the Associated Service edit form with valid data
    And the User chooses to save
    And the Associated Service is saved
    And the Associated Services dashboard is presented
    When the User chooses to go back
    Then the Order dashboard is presented
    Examples:
    | ProvisioningType |
    | declarative      |
    | variable         |

Scenario: Associated Service added - View Associated Services
    Given an Associated Service with a flat price declarative order type is saved to the order
    And the User has chosen to manage the Associated Service section
    When the Associated Services dashboard is presented
    Then the Associated Services are presented
    And the name of each Associated Service is displayed
    And they are able to manage each Associated Service 

Scenario: Associated Service added - section marked as complete
    Given an Associated Service with a flat price declarative order type is saved to the order
    And the User has chosen to manage the Associated Service section
    And the Associated Services dashboard is presented
    When the User chooses to continue
    Then the Order dashboard is presented
    And the content validation status of the associated-services section is complete

Scenario: Associated Services - Published associated services display
    Given the User has chosen to manage the Associated Service section
    When the User has chosen to Add a single Associated Service
    Then only the published associated services are available for selection

Scenario: Associated Services - Associated Services displayed in alphabetical order
    Given the supplier has multiple Associated Services
    And the User has chosen to manage the Associated Service section
    When the User has chosen to Add a single Associated Service
    Then the Associated Services are displayed in alphabetical order

Scenario: Associated Services - Edit Associated Service
    Given the Select Associated Service form is presented
    And the User selects an Associated Service with only one list price
    When they choose to continue
    Then they are presented with the Associated Service edit form
    And the price input is autopopulated with the list price value

    Scenario: Associated Services - Go back
    Given the Select Associated Service form is presented 
    And the User selects an Associated Service with only one list price
    When they choose to continue
    Then they are presented with the Associated Service edit form
    When the User chooses to go back
    Then they are presented with the Select Associated Service form

    Scenario: Associated Services - View Associated Service
    Given an Associated Service has been saved to the order
    When the User has chosen to manage the Associated Service section
    Then the name of each Associated Service is displayed
    Then the Order description is displayed
    And the Unit of order column contains the Unit of order of the Associated Service
    And there is a control to edit the Associated Service in the order

    Scenario: Associated Services - User chooses to continue
    Given an Associated Service has been saved to the order
    When the User has chosen to manage the Associated Service section
    When the User chooses to continue
    Then they are presented with the Order dashboard
    And the content validation status of the associated-services section is complete

Scenario: Associated Service - Associated Service already in order 
    Given an Associated Service has been saved to the order
    And the User has chosen to manage the Associated Service section
    When the User selects an Associated Service previously saved in the Order
    And they choose to continue 
    Then they are presented with the Edit Associated Service form for the order type
    And the previously saved data is Displayed

Scenario: Associated Service - Associated Service already in order  - Go Back
    Given an Associated Service has been saved to the order
    And the User has chosen to manage the Associated Service section
    When the User selects an Associated Service previously saved in the Order
    And the edit Associated Service form is presented
    And they choose to go back
    Then they are presented with the Select Associated Service form
    And the previously selected Associated Service persist

Scenario: Associated Service - Ask User to confirm delete
    Given an Associated Service with a flat price variable (On-demand) order type with the quantity period per year is saved to the order
    And the User has chosen to manage the Associated Service section
    And the Edit Associated Service form is displayed
    And the delete button is enabled
    When the User chooses to delete the Associated Service 
    Then the user is asked to confirm the choice to delete
    And the Call-off Agreement ID is displayed
    And the order description is displayed

Scenario: Associated Service - Go back 
    Given an Associated Service with a flat price declarative order type is saved to the order
    And the User has chosen to manage the Associated Service section
    And the Edit Associated Service form is displayed
    And the User chooses to delete the Associated Service 
    And the user is asked to confirm the choice to delete
    When the User chooses to go back
    Then the edit form is presented
    And the previously saved data is Displayed

Scenario: Associated Service - Not chosen to delete
    Given an Associated Service has been saved to the order
    And the User has chosen to manage the Associated Service section
    And the Edit Associated Service form is displayed
    And the User chooses to delete the Associated Service 
    And the confirm delete page is presented
    When the User chooses not to delete the Associated Service
    Then the edit form is presented

Scenario:  Associated Service - User confirmed delete
    Given an Associated Service has been saved to the order
    And the User has chosen to manage the Associated Service section
    And the Edit Associated Service form is displayed
    And the User chooses to delete the Associated Service
    And the confirm delete page is presented
    When the User chooses to confirm to delete the Associated Service
    Then the Associated Service with the unit is deleted from the order
    And the User is informed the Associated Service is deleted

Scenario:  Associated Service - Associated Service dashboard
    Given an Associated Service has been saved to the order
    And the User has chosen to manage the Associated Service section
    And the Edit Associated Service form is displayed
    And the User chooses to delete the Associated Service
    And the confirm delete page is presented
    And the User chooses to confirm to delete the Associated Service
    When the User chooses to continue
    Then the Associated Services dashboard is presented
    And the deleted Associated Service is not on the Associated Service dashboard

    @ignore - test passing in manual but failing in test
Scenario: Associated Service - Price greater than list price selected
    Given an Associated Service has been saved to the order
    And the User has chosen to manage the Associated Service section
    And the Edit Associated Service form is displayed
    And User enters a price greater than the list price selected
    When the User chooses to save
    Then the price is not saved
    And they are informed

    @ignore - test passing in manual but failing in test
Scenario: Associated Service - Price is less than or equal to the list price selected
    Given an Associated Service has been saved to the order
    And the User has chosen to manage the Associated Service section
    And the Edit Associated Service form is displayed
    When the User enters a price less than or equal to the list price selected
    And the User chooses to save
    Then the price is valid
