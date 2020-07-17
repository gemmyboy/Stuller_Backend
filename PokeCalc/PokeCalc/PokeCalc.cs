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
using System.Text.Json;
using System.Web.Helpers;

namespace PokeCalc
{
    //PokeCalc -: Class is used to interface with the Web API and cache data as needed.
    public sealed class PokeCalc
    {

        //--------------------------------
        //-------- Member Vars -----------

        private static PokeCalc PokeCalcInstance = null;            //Object Instance
        private const string URL = "https://pokeapi.co/api/v2/";    //Base URL

        //Caching mechanism used to minimize web API requests.
        private static Dictionary<string, List<string>> Cache_PokemonNameType = new Dictionary<string, List<string>>();
        private static Dictionary<string, List<string>> Cache_PokemonTypevsType = new Dictionary<string, List<string>>();


        //--------------------------------
        //------ Member Functions --------


        private PokeCalc() { }  //Constructor

        //Instance -: returns PokeCalc instance and instantiates if it wasn't called previously.
        public static PokeCalc Instance() 
        {
            if (PokeCalcInstance == null) PokeCalcInstance = new PokeCalc();
            return PokeCalcInstance;
        }//End Instance()


        //--------------------------------
        //------ Helper Functions --------

        

    }//End class PokeCalc
}//End namespace PokeCalc
