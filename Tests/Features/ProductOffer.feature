Feature: Product offer
This feature covers product offer creation

Background: 
	Given Matrix called "TestingMatrix" is already created
	And Matrix's manufacturer is "Manufacturer A"

@ProductOfferCleanup
Scenario Outline: Create new Product Offer with different user roles NEG/CS/MFG
	Given '<User>' is logged in
	And User clicks on Create Product Offer button
	And Selects Product called "Branko Test Product 1"
	And CPC is automatically filled with value "Weight Loss"
	And Effective date is current date
	And Participant assignment is "Humana"
	And Primary Rate type is "Base Rebate"
	And 1:1 has Bid with value of 10%
	And PB Footnote text is "Testing PB footnote" with "Humana" participants
	And NPB Rate has bid of 10% for Access Restricted
	And NPB Footnote text is "NPB Footnote" for All Participants
	When User clicks on Create button
	Then Succesfully added dialog is shown
	And New Product Offer is shown in the list
	And Primary Rate type should be "Base Rebate"
	And CPC should be "Anticoagulant"
	And 1:1 in Controlled Rebates should be "10"
	And Footnote text should be "Testing footnote"
	And Effective date should be current date
	And Status is "New"

	Examples: 
	| User |
	| Neg  |
	| CS   |
	| MFG  |

@ProductOfferCleanup
Scenario Outline: Create Product Offer with own secondary rate type
	Given '<User>' is logged in
	And User clicks on Create Product Offer button
	And Selects Product called "X Product"
	And Effective date is current date
	And Participant assignment is "Humana"
	And Primary Rate type is "Base Rebate"
	And Secondary Rate type is new one called "Testing secondary type"
	And Secondary Participant Assignment is "Humana"
	When User clicks on Create button
	Then Secondary type should be "Testing secondary type"

	Examples: 
	| User |
	| Neg  |
	| CS   |
	| MFG  |
Scenario Outline: Product Offer cannot be created without Effective date, Participant assignment and Primary Rate with different user roles NEG/CS/MFG
	Given '<User>' is logged in
	And User clicks on Create Product Offer button
	And Selects Product called "Lemons"
	When Users click on Create button
	Then "Effective date is required" message is shown under the Effective date
	And "Participants is required" message is shown under the Ascent rates participants
	And "Primary Rate type is required" message is shown under the Primary Rates type
	
	Examples: 
	| User |
	| Neg  |
	| CS   |
	| MFG  |


Scenario Outline: Product Offer edit with different user roles NEG/CS/MFG
	Given '<User>' is logged in
	And Product Offer called "" is already created
	And User opens edit dialog for current Product Offer
	And Participant assignment is changed to "Prime"
	And Rebate value 1:1 is removed
	When User clicks on Edit button
	Then Participant should be "Prime"
	And Rebate value 1:1 should be "No bid"
	And Footnote is removed
		
	Examples: 
	| User |
	| Neg  |
	| CS   |
	| MFG  |
