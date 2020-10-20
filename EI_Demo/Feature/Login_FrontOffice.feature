Feature: Login_FrontOffice
	Login Front Office

@mytag
Scenario: Login Front Office
	Given Open Front Office Page <SRF_URL>
	When And Input User <ID> <PW> and click Login
	Then Login Front Office succesful

	Examples: 
	| SRF_URL                                                       | ID                             | PW       |
	| https://www.baidu.com | AA | AA |
