Project:	PokeCalc
Written by: Marcus Shannon

Goal: Create a console application that can be used to determine a Pokemon's effectiveness versus other pokemon types

Data API is provided by: https://pokeapi.co/  - flat JSON data

Requirements:
	Input -:	Pokemon Name
	Output -:	Current Pokemon Type, Types Strong Against, Types Weak Against

Bonus:
	Unit Tests

------------------------------------------------------------------------------------------------------------------------
Design Notes:
	Data API only works with GET requests and only returns JSON or undefined.
	GET Request to get initial pokemon data: 
		https://pokeapi.co/api/v2/pokemon/{pokemonname}
		ex: https://pokeapi.co/api/v2/pokemon/ditto
	GET Request to get Type Data
		https://pokeapi.co/api/v2/type/{typename}
		ex: https://pokeapi.co/api/v2/type/normal

	Console will be REPL with command options:
		<pokemon-name> - any legal pokemon name that stored being the web-api
		help - list commands and explain what they do
		quit - exist the application

	Create a simplified API that minimizes network requests while still getting optimal performance.

	Pulled in this Library/API to simplify JSON serialization along with using their pre-built models.
	https://github.com/mtrdp642/PokeApiNet

------------------------------------------------------------------------------------------------------------------------
Technical Notes:
	Developed utilizing the following configuration:
	IDE: Visual Studios 2019
	Unit Tests: NUnit
	OS: Windows 7
	Target Framework: .NET Core 3.1
	Output Type: Command Console