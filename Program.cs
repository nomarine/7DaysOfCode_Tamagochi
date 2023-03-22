using System.Net;
using RestSharp;

Console.Write("Procure um Pokémon: ");
string? pokemon = Console.ReadLine();

if(pokemon != null){
    pokemon = pokemon.ToLower();
    var client = new RestClient("https://pokeapi.co/api/v2");
    var request = new RestRequest($"/pokemon/{pokemon}", Method.Get);
    var response = client.Execute(request);
    if(response.StatusCode == HttpStatusCode.OK) {
        Console.WriteLine(response.Content);
    } else {
        Console.WriteLine("Pokémon não encontrado.");
    }
    
}



