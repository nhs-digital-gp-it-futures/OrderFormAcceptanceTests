Feature: Preview Order Summary
	As a Buyer
	I want to view a large version of the Order Summary
	So that I can preview the Order

Background: 
	Given an unsubmitted order exists

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

@ignore
Scenario: Preview Order Summary - Flat with Variable (On-demand) order type per year estimation period
	Given a catalogue solution with a flat price variable (On-demand) order type with the quantity period per year is saved to the order
	When the Order Summary is displayed
	Then the Order items (recurring cost) table is populated
	And the Recipient name (ODS code) of each item is the concatenation "[Service Recipient name] [(ODS code)]"
	And the item ID of each item is displayed
	And the item name of each item is the Catalogue Solution name
	And the Price unit of order of each item is the concatenation "[Price] [unit]"
	And the Quantity of each item is the concatenation "[Quantity] [Estimation period]" i.e. [Quantity] per year
	And the Planned delivery date of each item is displayed
	And the item year cost of each item is the result of the Flat calculation [Price] * [Quantity] rounded up to two decimal places

@ignore
Scenario: Preview Order Summary - Flat with Variable (On-demand) order type per month estimation period
	Given a catalogue solution with a flat price variable (On-demand) order type with the quantity period per month is saved to the order
	When the Order Summary is displayed
	Then the Order items (recurring cost) table is populated
	And the Recipient name (ODS code) of each item is the concatenation "[Service Recipient name] [(ODS code)]"
	And the item ID of each item is displayed
	And the item name of each item is the Catalogue Solution name
	And the Price unit of order of each item is the concatenation "[Price] [unit]"
	And the Quantity of each item is the concatenation "[Quantity] [Estimation period]" i.e. [Quantity] per month
	And the Planned delivery date of each item is displayed
	And the item year cost of each item is the result of the Flat calculation [Price] * [Quantity] * 12 rounded up to two decimal places

@ignore
Scenario: Preview Order Summary - Flat with Variable (Per-Patient) order type
Given a catalogue solution with a flat price variable (Per-Patient) order type is saved to the order
When the Order Summary is displayed
Then the Order items (recurring cost) table is populated
And the Recipient name (ODS code) of each item is the concatenation "[Service Recipient name] [(ODS code)]"
And the item ID of each item is displayed
And the item name of each item is the Catalogue Solution name
And the Price unit of order of each item is the concatenation "[Price] [unit]"
And the Quantity of each item is the concatenation [Quantity] per month
And the Planned delivery date of each item is displayed
And the item year cost of each item is the result of the Flat calculation [Price] * [Quantity] rounded up to two decimal places
