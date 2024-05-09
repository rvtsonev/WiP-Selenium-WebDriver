Feature: Ebay

Background: 
	Given user navigates to 'https://www.ebay.com/'

Scenario: DEMO: using most generic step implementation
	Then user is able to see element 'img' with attribute 'id' with value 'gh-logo' displayed

Scenario: DEMO: using Path builder
	Then user is able to see Ebay logo in the header element displayed

Scenario: DEMO: define component by sub-element text
	Then user is able to see popular category with name 'Luxury' displayed
	And popular category with name 'Luxury' has image '01_PopularDestination_Luxury.jpg'
