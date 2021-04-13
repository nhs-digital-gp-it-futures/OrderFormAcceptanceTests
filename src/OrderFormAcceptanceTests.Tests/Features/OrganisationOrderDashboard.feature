Feature: Organisation Order Dashboard
	As a buyer
	I want to view my orders on my organisation's orders dashboard
	So that they are correct
	
Scenario: Organisation order dashboard – orders presented in descending order
	Given my organisation has one or more orders
	When my organisation's orders dashboard is presented
	Then the completed orders are presented in descending order by the date completed

Scenario: Organisation order dashboard – completed orders
	Given an order is completed
	When the order dashboard is presented
	Then there is a control I can use to view the completed Version of the order summary

Scenario: Organisation order dashboard – order completed date
	Given an order is completed
	When the order dashboard is presented
	Then there isn't a last updated column in the completed orders table
	And there is a date completed column
	And the date that the order was completed is displayed in the date completed column
