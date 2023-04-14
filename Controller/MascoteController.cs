using Model.Mascote;

using RestSharp;
using System.Text.Json;

public class MascoteController {
    public Mascote CreateMascote(RestResponse mascoteInfo) {
        Mascote mascote = JsonSerializer.Deserialize<Mascote>(mascoteInfo.Content);
        return mascote;
    }

    public AdoptedMascote AdoptMascote(Mascote mascote) {
        AdoptedMascote adoptedMascote = new AdoptedMascote(mascote);
        return adoptedMascote;
    }    
}