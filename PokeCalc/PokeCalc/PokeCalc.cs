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
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using PokeApiNet;

namespace PokeCalc
{
    //PokeCalc -: Class is used to interface with the Web API and cache data as needed.
    public sealed class PokeCalc
    {

        //--------------------------------
        //-------- Member Vars -----------

        private static PokeCalc PokeCalcInstance = null;                //Object Instance
        private const string BaseURL = "https://pokeapi.co/api/v2/";    //Base URL
        private PokeApiNet.PokeApiClient PAC = new PokeApiClient();

        //Caching mechanism used to minimize web API requests.
        private Dictionary<string, Pokemon> Cache_PokemonNameToObj = new Dictionary<string, Pokemon>();
        //private Dictionary<string, List<string>> Cache_PokemonTypevsType = new Dictionary<string, List<string>>();


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
            if (!Cache_PokemonNameToObj.TryGetValue(pokemonname, out pokemon))
                pokemon = await PAC.GetResourceAsync<Pokemon>(pokemonname);

            PokeApiNet.Type type = await PAC.GetResourceAsync<PokeApiNet.Type>(pokemon.Types[0].Type.Name);
            
            



        }//End Calculate()


        //--------------------------------
        //------ Helper Functions --------

        /*
        //RequestTypeVsType -: chains together the url to make the request
        private List<string> RequestTypeVsType(string typename)
        {
            List<string> t_name;
            if (Cache_PokemonTypevsType.TryGetValue(typename, out t_name))
                return t_name;
            else
            {
                t_name = FindTypeData(RequestData(BaseURL + "/type/" + typename).Result);
                Cache_PokemonTypevsType.Add(typename, t_name);
                return t_name;
            }
        }//End RequestTypeVsType()

        //RequestPokemonType -: chains together the url to make the request
        private List<string> RequestPokemonType(string pokemonname)
        {
            List<string> t_name;
            if (Cache_PokemonNameType.TryGetValue(pokemonname, out t_name)) 
                return t_name;
            else
            {
                t_name = FindPokemonType(RequestData(BaseURL + "/pokemon/" + pokemonname).Result);
                Cache_PokemonNameType.Add(pokemonname, t_name);
                return t_name;
            }
        }//End RequestPokemonType()

        //RequestData -: use the provided URL to send a GET request and return web api data.
        private async Task<string> RequestData(string url)
        {
            using(HttpClient client = new HttpClient())
            {
                HttpResponseMessage hrm = await client.GetAsync(url);
                try { hrm.EnsureSuccessStatusCode(); }
                catch (Exception) { Console.WriteLine("Failed to service request: " + url);}

                return await hrm.Content.ReadAsStringAsync();
            }
        }//End RequestData()

        //FindPokemonType -: finds the pokemon type inside the json.
        //Note: this data is requested using pokemon name.
        private List<string> FindPokemonType(string json)
        {
            List<string> typedata = new List<string>();
            PokeAPI.Pokemon data = JsonSerializer.Deserialize<PokeAPI.Pokemon>(json);

            return typedata;
        }//End FindPokemonType

        //FindTypeData -: finds the type data inside the json.
        //Note: this data is requested using the pokemon's type that was named.
        private List<string> FindTypeData(string json)
        {
            List<string> typedata = new List<string>();
            dynamic data = JsonSerializer.Deserialize<dynamic>(json);

            return typedata;
        }//End FindTypeData()

        */



    }//End class PokeCalc
}//End namespace PokeCalc
