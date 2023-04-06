using RestSharp;

namespace Service.PokemonAPI {
    public class PokemonAPI {  
        public RestResponse getPokemon(string pokemonName) {
            pokemonName = pokemonName.ToLower();
            var client = new RestClient("https://pokeapi.co/api/v2");
            var request = new RestRequest($"/pokemon/{pokemonName}", Method.Get);
            var response = client.Execute(request);
            return response;
        }
    }
}