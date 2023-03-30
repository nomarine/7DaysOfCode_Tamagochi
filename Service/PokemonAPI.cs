using RestSharp;

namespace Service.PokemonAPI {
    public class PokemonAPI {  
        public RestResponse searchMascote(string param) {
            param = param.ToLower();
            var client = new RestClient("https://pokeapi.co/api/v2");
            var request = new RestRequest($"/pokemon/{param}", Method.Get);
            var response = client.Execute(request);
            return response;
        }
    }
}