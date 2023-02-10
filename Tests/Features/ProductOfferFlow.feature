Feature: ProductOfferFlow
In order to present the main flow of a Product Offer
As a manufacturer and negotiator
I want to be able to create it, send it to Ascent, review it and finally send to an approval

Scenario Outline: Send created Product Offer for negotiation
	Given Unique matrix is created with a new product offer in it
	And '<User>' is logged in
	When I send created product offer to Ascent for negotiation
	Then I will get confirmation that sending is successful
	And 'Neg' will see sent product offer on his dashboard 'Assigned to me for negotiation'
Examples:
	| user |
	| MFG  |
	| Neg  |