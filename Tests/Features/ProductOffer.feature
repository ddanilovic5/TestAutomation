Feature: Create Product offer
In order to be able to have Product Offer with certain data
As a manufacturer, negotiator and contract specialist
I want to be able to successfully set these data in create Product Offer form

@DeleteMatrix
Scenario Outline: Create new Product Offer with different user roles NEG/CS/MFG
	Given Unique matrix is created
	And I login as user '<User>'
	And I start to create new Product Offer
	And I select Product called "TA_Product"
	And Effective date is current date
	And Participant assignment is "Humana"
	And Primary Rate type is "Base Rebate"
	And Controlled 1:1 has Bid with value of 10%
	And Add unique PB Footnote with "Humana" participants
	And NPB Rate has bid of 5% for Access Restricted
	And Add unique NPB Footnote for "All Participants"
	When I create Product Offer
	Then Success message is shown
	And New Product Offer is shown in the list
	And Primary Rate type should be "Base Rebate"
	And CPC should be "Anticoagulant"
	And 1:1 in Controlled Rebates should be "10"
	And Restricted NP Brand Rates should be "5"
	And PB Footnote text should be adequate
	And NPB Footnote text should be adequate
	And Effective date should be current date

	Examples: 
	| User |
	| Neg  |
	| CS   |
	| MFG  |

