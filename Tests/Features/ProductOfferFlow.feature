Feature: ProductOfferFlow
In order to present the main flow of a Product Offer
As a manufacturer and negotiator
I want to be able to create it, send it to Ascent, review it and finally send to an approval
Background: Admin user creates unique matrix
	Given Unique matrix is created

@DeleteMatrix
Scenario Outline: Send created Product Offer for negotiation
	Given I login as user '<User>'
	And I create New Product Offer
	When I send created product offer to Ascent for negotiation
	Then I will get confirmation that sending is successful
	And Status of that product offer will be 'In review'
	And 'Neg' will see sent product offer on his dashboard 'Assigned to me for negotiation'
Examples:
	| User |
	| MFG  |
	| Neg  |

@DeleteMatrix
Scenario: Send Product Offer for approval
	Given I login as user 'MFG'
	And I create New Product Offer
	And I send created product offer to Ascent for negotiation
	And I login as user 'Neg'
	And I prepare for sending created product for approval to 'CS'
	When I send selected product offer for CS review
	Then I will get confirmation that sending is successful
	And Status of that product offer will be 'In review'
	And 'CS' will see sent product offer on his dashboard 'Assigned to Ascent'
