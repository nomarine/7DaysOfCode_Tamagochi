using Model.Mascote;

using System.Net;
using System.Text.Json;
using RestSharp;

namespace Controller.MascoteController {
    public class MascoteController {
    public string getUsername() {
        string username = Console.ReadLine();
        if(username == string.Empty) {
            return username = "Anônimo";
        }
        return username;
    }

    public Mascote? searchMascote(string param) {
        param = param.ToLower();
        var client = new RestClient("https://pokeapi.co/api/v2");
        var request = new RestRequest($"/pokemon/{param}", Method.Get);
        var response = client.Execute(request);
        if(response.StatusCode == HttpStatusCode.OK) {
            Mascote mascote = JsonSerializer.Deserialize<Mascote>(response.Content);
            return mascote;
        } else {
            return null;
        }
    }

    public AdoptedMascote adoptMascote(Mascote mascote) {
        AdoptedMascote adoptedMascote = new AdoptedMascote(mascote);
        return adoptedMascote;
    }

}    
}