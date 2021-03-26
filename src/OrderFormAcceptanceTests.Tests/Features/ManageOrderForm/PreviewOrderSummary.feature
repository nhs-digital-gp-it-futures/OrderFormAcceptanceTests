Feature: Preview Order Summary
	As a Buyer
	I want to view a large version of the Order Summary
	So that I can preview the Order

Background: 
	Given an incomplete order exists
	And a supplier has been selected
	And the Call Off Ordering Party section is complete
    And the Commencement Date is complete

Scenario: Preview Order Summary
	Given the Order Form for the existing order is presented
	When the User chooses to preview the Order Summary
	Then the Order Summary is presented
	And the Call Off Agreement ID is displayed
	And the Order description is displayed
	And the date the Order Summary was produced
	And the Call Off Ordering Party information is displayed
	And the Supplier information is displayed
	And the Commencement date is displayed
	And Order items (one-off cost) table is displayed
	And Order items (recurring cost) table is displayed
	And the total one-off cost is displayed
	And the total monthly cost is displayed
	And the total annual cost is displayed
	And the total cost of ownership is displayed

Scenario: Preview Order Summary - Data presented
	Given the Order Summary is displayed
	Then it displays the Call-off Agreement ID data
	And the Order description data saved in the order
	And the Call-off Ordering Party data saved in the order
	And the Call-off Ordering Party names are concatenated
	And the Supplier data saved in the order
	And the Supplier first name and last name are concatenated
	And the Commencement date data saved in the order

Scenario: Preview Order Summary - Go back
	Given the Order Summary is displayed
	When the User chooses to go back
	Then the Order dashboard is presented

Scenario: Preview Order Summary - Flat with Variable (On-demand) order type per year estimation period
	Given a catalogue solution with a flat price variable (On-demand) order type with the quantity period per year is saved to the order
	When the Order Summary is displayed
	Then the Order items (recurring cost) table is populated
	And the item ID of each item is displayed
	And the item name of each item is the Catalogue Solution name
	And the Price unit of order of each item is the concatenation "[Price] [unit]"
	And the Quantity of each item is the concatenation "[Quantity] [Estimation period]" i.e. [Quantity] per year
	And the Planned delivery date of each item is displayed
	And the item year cost of each item is the result of the Flat calculation [Price] * [Quantity] rounded up to two decimal places

Scenario: Preview Order Summary - Flat with Variable (On-demand) order type per month estimation period
	Given a catalogue solution with a flat price variable (On-demand) order type with the quantity period per month is saved to the order
	When the Order Summary is displayed
	Then the Order items (recurring cost) table is populated
	And the item ID of each item is displayed
	And the item name of each item is the Catalogue Solution name
	And the Price unit of order of each item is the concatenation "[Price] [unit]"
	And the Quantity of each item is the concatenation "[Quantity] [Estimation period]" i.e. [Quantity] per month
	And the Planned delivery date of each item is displayed
	And the item year cost of each item is the result of the Flat calculation [Price] * [Quantity] * 12 rounded up to two decimal places

Scenario: Preview Order Summary - Flat with Variable (Per-Patient) order type
	Given a catalogue solution with a flat price variable (Per-Patient) order type is saved to the order
	When the Order Summary is displayed
	Then the Order items (recurring cost) table is populated
	And the item ID of each item is displayed
	And the item name of each item is the Catalogue Solution name
	And the Price unit of order of each item is the concatenation "[Price] [unit]"
	And the Quantity of each item is the concatenation [Quantity] per year
	And the Planned delivery date of each item is displayed
	And the item year cost of each item is the result of the Flat calculation [Price] * [Quantity] rounded up to two decimal places

Scenario: Preview Order Summary - Catalogue Solution Flat with Variable (Declarative) order type
	Given a catalogue solution with a flat price variable (Declarative) order type is saved to the order 1 
	When the Order Summary is displayed
	Then the Order items (recurring cost) table is populated
	And the item ID of each item is displayed
	And the item name of each item is the Catalogue Solution name
	And the Price unit of order of each item is the concatenation "[Price] [unit]"
	And the Quantity of each item is the concatenation [Quantity] per year
	And the Planned delivery date of each item is displayed
	And the item year cost of each item is the result of the Flat calculation [Price] * [Quantity] * 12 rounded up to two decimal places

Scenario: Preview Order Summary - Additional Service Flat with Variable (Declarative) order type
	Given an additional service with a flat price variable Declarative order type is saved to the order
	When the Order Summary is displayed
	Then the Order items (recurring cost) table is populated    
	And the item ID of each item is displayed
	And the item name of each item is the Additional Service name
	And the Price unit of order of each item is the concatenation "[Price] [unit]"
	And the Quantity of each item is the concatenation [Quantity] per year
	And the Planned delivery date of each item is displayed
	And the item year cost of each item is the result of the Flat calculation [Price] * [Quantity] * 12 rounded up to two decimal places

Scenario: Preview Order Summary - Additional Service Flat with Variable (Patient) order type
	Given an additional service with a flat price variable Patient order type is saved to the order
	When the Order Summary is displayed
	Then the Order items (recurring cost) table is populated
	And the item ID of each item is displayed
	And the item name of each item is the Additional Service name
	And the Price unit of order of each item is the concatenation "[Price] [unit]"
	And the Quantity of each item is the concatenation [Quantity] per year
	And the Planned delivery date of each item is displayed
	And the item year cost of each item is the result of the Flat calculation [Price] * [Quantity] rounded up to two decimal places

Scenario: Preview Order Summary - Additional Service Flat with Variable (OnDemand) order type per year
	Given an associated service with a flat price variable (On-Demand) order type with the quantity period per year is saved to the order
	When the Order Summary is displayed
	Then the Order items (recurring cost) table is populated
	And the item ID of each item is displayed
	And the item name of each item is the Additional Service name
	And the Price unit of order of each item is the concatenation "[Price] [unit]"
	And the Quantity of each item is the concatenation [Quantity] per year
	And the Planned delivery date of each item is displayed
	And the item year cost of each item is the result of the Flat calculation [Price] * [Quantity] rounded up to two decimal places

Scenario: Preview Order Summary - Additional Service Flat with Variable (On-Demand) order type per month
	Given an additional service with a flat price variable On-Demand order type with the quantity period per month is saved to the order
	When the Order Summary is displayed
	Then the Order items (recurring cost) table is populated
	And the item ID of each item is displayed
	And the item name of each item is the Additional Service name
	And the Price unit of order of each item is the concatenation "[Price] [unit]"
	And the Quantity of each item is the concatenation [Quantity] per month
	And the Planned delivery date of each item is displayed
	And the item year cost of each item is the result of the Flat calculation [Price] * [Quantity] * 12 rounded up to two decimal places
	
Scenario: Preview Order Summary - Associated Service Flat with Declarative order type
	Given an Associated Service with a flat price declarative order type is saved to the order
	When the Order Summary is displayed
	Then the Order items (one off) table is populated
	And the item ID of each item is displayed
	And the item name of each item is the Associated Service name
	And the Price unit of order of each item is the concatenation "[Price] [unit]"
	And the Quantity of each item is the concatenation [Quantity] per year
	And the item year cost of each item is the result of the Flat calculation [Price] * [Quantity] rounded up to two decimal places
	
Scenario: Preview Order Summary - Associated Service Flat with Variable (OnDemand) order type per year
	Given an associated service with a flat price variable (On-Demand) order type with the quantity period per year is saved to the order
	When the Order Summary is displayed
	Then the Order items (recurring cost) table is populated
	And the item ID of each item is displayed
	And the item name of each item is the Additional Service name
	And the Price unit of order of each item is the concatenation "[Price] [unit]"
	And the Quantity of each item is the concatenation [Quantity] per year
	And the Planned delivery date of each item is displayed
	And the item year cost of each item is the result of the Flat calculation [Price] * [Quantity] rounded up to two decimal places

Scenario: Preview Order Summary - Associated Service Flat with Variable (On-Demand) order type per month
	Given an associated service with a flat price variable (On-Demand) order type with the quantity period per month is saved to the order
	When the Order Summary is displayed
	Then the Order items (recurring cost) table is populated
	And the item ID of each item is displayed
	And the item name of each item is the Additional Service name
	And the Price unit of order of each item is the concatenation "[Price] [unit]"
	And the Quantity of each item is the concatenation [Quantity] per month
	And the Planned delivery date of each item is displayed
	And the item year cost of each item is the result of the Flat calculation [Price] * [Quantity] * 12 rounded up to two decimal places
	
Scenario: Preview Order Summary - Total cost for one year
	Given an associated service with a flat price variable (On-Demand) order type with the quantity period per month is saved to the order
	When the Order Summary is displayed
	Then the Order items (recurring cost) table is populated
	And the total annual cost is displayed
	And the Total cost for one year is the result of the Total cost for one year calculation
	And the Total cost for one year is expressed as two decimal places

Scenario: Preview Order Summary - Total monthly cost
	Given an associated service with a flat price variable (On-Demand) order type with the quantity period per month is saved to the order
	When the Order Summary is displayed
	Then the total monthly cost is displayed
	And the Total monthly cost is the result of the Total monthly cost calculation
	And the Total monthly cost is expressed as two decimal places

Scenario: Preview Order Summary - Total one-off cost
	Given an Associated Service with a flat price declarative order type is saved to the order
	When the Order Summary is displayed
	Then the total one-off cost is displayed
	And the Total one-off cost is the result of the Total one-off cost calculation

Scenario: Preview Order Summary - Total cost of contract
	Given an associated service with a flat price variable (On-Demand) order type with the quantity period per month is saved to the order
	And an Associated Service with a flat price declarative order type is saved to the order
	When the Order Summary is displayed
	Then the Total cost of contract is displayed
	And the Total cost of contract is the result of the Total cost of contract calculation Total one-off cost + (3 * Total cost for one year calculation)
	And the Total cost of contract is expressed as two decimal places
	And the order items recurring cost table is sorted by service recipient name

Scenario: Preview Order Summary - Sort by service recipient name
	Given multiple order items with different service recipient have been added to the order
	When the Order Summary is displayed
	Then the Order items (recurring cost) table is populated
	And the order items recurring cost table is sorted by service recipient name

Scenario: Preview Order Summary - Second Sort by item name
	Given multiple order items with the same service recipient have been added to the order
	When the Order Summary is displayed
	Then the Order items (recurring cost) table is populated
	And the order items recurring cost table is second sorted by item name

Scenario: Preview Order Summary - One-Off table sorted by item name
	Given multiple one-off order items have been added to the order
	When the Order Summary is displayed
	Then the Order items (one off) table is populated
	And the order items one-off cost table is sorted by item name

Scenario: Preview Order Summary - Service Instance ID displayed
	Given an associated service with a flat price variable (On-Demand) order type with the quantity period per month is saved to the order
	When the Order Summary is displayed
	Then the Order items (recurring cost) table is populated
	And the Service Instance ID of each saved Catalogue Solution item is displayed
