using Model.Mascote;
using Service.PokemonAPI;

using RestSharp;
using System.Net;
using System.Text.Json;

namespace Controller.MascoteController {
    
    public class MascoteController {
        public Mascote createMascote(RestResponse mascoteInfo) {
            Mascote mascote = JsonSerializer.Deserialize<Mascote>(mascoteInfo.Content);
            return mascote;
        }

        public AdoptedMascote adoptMascote(Mascote mascote) {
            AdoptedMascote adoptedMascote = new AdoptedMascote(mascote);
            return adoptedMascote;
        }    
    }

}