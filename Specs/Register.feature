Feature: Register for the configuration
	As an unregistered user
	I want to register an account for the smart mirror configuration
	So that i can log in and design my personal interface for the mirror

Scenario: Registration succeeded
	Given I can't log in
	And I don't have an account to log in
	When I click on "register"
	Then I fill in "firstname" with "Hans"
	And I fill in "lastname" with "Meier"
	And I fill in "email" with "email@provider.domain"
	And I fill in "password" with "password"
	And I fill in "passwordConfirm" with "password"
	When I click on "upload"
	Then I can fill "thumbnail" with the "path to my image"
	And I click on "record" 
	Then I go to the mirror and speak the shown phrase
	And I click on "Registrate"
	Then I can login to the configuration

Scenario: Registration failed
	Given I can't log in
	And I don't have an account to log in
	When i don't fill all information
	And I click on "Registrate"
	Then I can't registrate an account