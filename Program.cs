using System.Net;
using System.Text.Json;
using RestSharp;

public class App {
    public static void Main(string[] args) {
        Console.Write("Procure um Pokémon: ");
        string? param = Console.ReadLine();
        if(param != null){
            param = param.ToLower();
            var client = new RestClient("https://pokeapi.co/api/v2");
            var request = new RestRequest($"/pokemon/{param}", Method.Get);
            var response = client.Execute(request);
            if(response.StatusCode == HttpStatusCode.OK) {
                Mascote mascote = JsonSerializer.Deserialize<Mascote>(response.Content);
                Console.WriteLine($"{char.ToUpper(mascote.name[0])}{mascote.name.Substring(1)}");
                Console.WriteLine("Altura (m): {0}", (mascote.height / 10.0));
                Console.WriteLine("Peso (kg): {0}", (mascote.weight / 10.0));
                Console.Write("Tipo: ");
                foreach (Mascote.Types types in mascote.types) {
                        Console.WriteLine(String.Join(" ", types.type.name.Split('-').Select(p => p.Substring(0,1).ToUpper() + p.Substring(1))));
                } 
                Console.WriteLine("Habilidades: ");
                foreach (Mascote.Abilities abilities in mascote.abilities) {
                        Console.WriteLine(String.Join(" ", abilities.ability.name.Split('-').Select(p => p.Substring(0,1).ToUpper() + p.Substring(1))));
                } 
            } else {
                Console.WriteLine("Pokémon não encontrado.");
            }
        }
    }
}

public class Mascote {
    public string name { get; set; }
    public int height { get; set; }
    public int weight { get; set; }
    public List<Abilities> abilities { get; set; }
    public List<Types> types { get; set; }
    
    public class Abilities {
        public Ability ability { get; set; }

        public class Ability {
            public string name { get; set; }
        }
    }

    public class Types {
        public Type type { get; set; }

        public class Type {
            public string name { get; set; }
        }
    }
}

public class Frontend {
    public bool isAtivo { get; set; }

    void getUsername() {
        string username = Console.ReadLine();
        if(username == string.Empty) {
            username = "Anônimo";
        }
        Console.WriteLine($"username, o que você deseja?");
        string option = Console.ReadLine();
    }

    public enum MenuOptions : byte {
        Adotar = 1,
        Ver,
        Sair
    }

    void exibirMenu() {

    }

}