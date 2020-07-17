﻿Project:	PokeCalc
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

	Input Validation
		Pokemon Names only contain letters a-z, A-Z, and space(s); anything else is illegal.

	Console will be REPL with command options:
		help - list commands and explain what they do
		quit - exist the application

------------------------------------------------------------------------------------------------------------------------
Technical Notes:
	IDE: Visual Studios 2019
	Unit Tests: NUnit
	OS: Windows 7
	Target Framework: .NET Core 3.1
	Output Type: Command Console