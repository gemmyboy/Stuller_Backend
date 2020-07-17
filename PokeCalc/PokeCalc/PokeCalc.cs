/*
 *      PokeCalc.cs
 *          by Marcus Shannon
 * 
 *      The idea is to isolate the class PokeCalc using the Singleton design pattern.  We do
 *      this because when we implement caching: it will simplify the overall code structure,
 *      and reduce web requests to the web API.
 * 
 */
using System;
using System.Collections.Generic;
using PokeApiNet;

namespace PokeCalc
{
    //PokeCalc -: Class is used to interface with the Web API and cache data as needed.
    public sealed class PokeCalc
    {

        //--------------------------------
        //-------- Member Vars -----------

        private static PokeCalc PokeCalcInstance = null;                //Object Instance
        private const int str_maxlength = 57;                           //Max Length of String for Console Output Window
        private PokeApiClient PAC = new PokeApiClient();

        //Caching mechanism used to minimize web API requests.
        private Dictionary<string, Pokemon> Cache_PokemonNameToObj = new Dictionary<string, Pokemon>();
        private Dictionary<string, PokeApiNet.Type> Cache_PokemonTypeToTypeObj = new Dictionary<string, PokeApiNet.Type>();


        //--------------------------------
        //------ Member Functions --------


        private PokeCalc() { }  //Constructor

        //Instance -: returns PokeCalc instance and instantiates if it wasn't called previously.
        public static PokeCalc Instance() 
        {
            if (PokeCalcInstance == null) PokeCalcInstance = new PokeCalc();
            return PokeCalcInstance;
        }//End Instance()

        //Calculate -: takes in the name of a pokemon and calculates the type strengths/weaknesses.
        public async void Calculate(string pokemonname)
        {
            Pokemon pokemon;

            //Check Cache: if it's not there, pull data and store in cache.
            if (!Cache_PokemonNameToObj.TryGetValue(pokemonname, out pokemon))
            {
                try { pokemon = await PAC.GetResourceAsync<Pokemon>(pokemonname); }
                catch(Exception) { Console.WriteLine("Invalid Pokemon Name or Command!"); return; }
                Cache_PokemonNameToObj.Add(pokemonname, pokemon);
            }

            Output_PokemonData(pokemon.Name, pokemon.Types);

            //Pokemon can be multiple types i.e.: Flying/Normal
            for(int t_iter = 0; t_iter < pokemon.Types.Count; t_iter++)
            {
                PokemonType t_type = pokemon.Types[t_iter];
                PokeApiNet.Type type;

                //Check Cache: if it's not there, pull data and store in cache.
                if (!Cache_PokemonTypeToTypeObj.TryGetValue(t_type.Type.Name, out type))
                {
                    type = await PAC.GetResourceAsync<PokeApiNet.Type>(t_type.Type.Name);
                    Cache_PokemonTypeToTypeObj.Add(t_type.Type.Name, type);
                }

                Output_TypeVsTypeData(type, t_iter, (t_iter == (pokemon.Types.Count - 1)));
            }//End foreach
        }//End Calculate()


        //--------------------------------
        //------ Helper Functions --------

        //Output_PokemonData -: Pretty Prints base Pokemon data
        private void Output_PokemonData(string pokename, List<PokemonType> pokemontype)
        {
            if (string.IsNullOrWhiteSpace(pokename) || pokemontype.Count == 0) throw new System.ArgumentException();

            //String is 57 Characters long
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("| Pokemon Base Data                                     |");
            Console.WriteLine("|                                                       |");

            string str_pokemonname = "| Pokemon Name: ";
            string str_end = "|";

            //Note: Calculates even character length and pads the rest with spaces; also capitalizes first letter of var pokename
            Console.WriteLine(str_pokemonname + new System.Globalization.CultureInfo("en-US", false).TextInfo.ToTitleCase(pokename)
                + (new string(' ', str_maxlength - str_pokemonname.Length - pokename.Length - str_end.Length)) + str_end);

            foreach(var str_type in pokemontype)
            {
                string str_pokemontype = "| Pokemon Type: ";

                //Note: Calculates even character length and pads the rest with spaces; also capitalizes first letter of var str_type.Type.Name
                Console.WriteLine(str_pokemontype + new System.Globalization.CultureInfo("en-US", false).TextInfo.ToTitleCase(str_type.Type.Name)
                    + (new string(' ', str_maxlength - str_pokemontype.Length - str_type.Type.Name.Length - str_end.Length)) + str_end);
            }
            Console.WriteLine("---------------------------------------------------------");

        }//End Output_PokemonData()

        //Output_TypeVsTypeData -: Pretty Prints Type vs Type data
        //Note: function called iteratively with bool flag to mark last iter
        private void Output_TypeVsTypeData(PokeApiNet.Type type, int iter, bool end)
        {
            if (iter == 0)
            {
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("| Pokemon Type: Strengths and Weaknesses Calculation    |");
                Console.WriteLine("|                                                       |");
            }

            string str_pokemontype = "| Type: ";
            string str_end = "|";

            Console.WriteLine(str_pokemontype + new System.Globalization.CultureInfo("en-US", false).TextInfo.ToTitleCase(type.Name)
                    + (new string(' ', str_maxlength - str_pokemontype.Length - type.Name.Length - str_end.Length)) + str_end);

            string[] str_outputprefix =
            {
                "| Receives Double Damage from: ",
                "| Deals Double Damage to: ",
                "| Receives Half Damage from: ",
                "| Deals Half Damage to: ",
                "| Receives No Damage from: ",
                "| Deals No Damage to: ",
            };

            List<NamedApiResource<PokeApiNet.Type>>[] str_damagerelations =
            {
                type.DamageRelations.DoubleDamageFrom,
                type.DamageRelations.DoubleDamageTo,
                type.DamageRelations.HalfDamageFrom,
                type.DamageRelations.HalfDamageTo,
                type.DamageRelations.NoDamageFrom,
                type.DamageRelations.NoDamageTo,
            };

            //ASSERT just incase someone comes back to change this later and messes it up
            System.Diagnostics.Debug.Assert(str_outputprefix.Length == str_damagerelations.Length, "The number of damage relations must have of the same number output prefixes!");

            //There are 6 types of Damage Relations
            for(int t_iter_damagerelations = 0; t_iter_damagerelations < str_damagerelations.Length; t_iter_damagerelations++)
            {
                //There can be multiple types of each kind of Damage Relation
                for(int t_iter_relationtype = 0; t_iter_relationtype < str_damagerelations[t_iter_damagerelations].Count; t_iter_relationtype++)
                {
                    Console.WriteLine(str_outputprefix[t_iter_damagerelations] 
                        + new System.Globalization.CultureInfo("en-US", false).TextInfo.ToTitleCase(str_damagerelations[t_iter_damagerelations][t_iter_relationtype].Name)
                        + (new string(' ', 
                            str_maxlength - str_outputprefix[t_iter_damagerelations].Length - str_damagerelations[t_iter_damagerelations][t_iter_relationtype].Name.Length - str_end.Length)) 
                        + str_end);
                }//End for - DamageRelation Type
            }//End for - DamageRelation

            if(end)
                Console.WriteLine("---------------------------------------------------------");
        }//End Output_TypeVsTypeData()

    }//End class PokeCalc
}//End namespace PokeCalc
