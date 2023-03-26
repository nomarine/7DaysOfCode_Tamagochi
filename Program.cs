using System.Net;
using System.Text.Json;
using RestSharp;
using Figgle;
using Spectre.Console;

public class App {
    public static bool isAtivo { get; set; }
    public static string username { get; set; }
    
    public static void Main(string[] args) {
        isAtivo = true;
        Frontend app = new Frontend();
        app.exibirTitulo();
        app.login();
        do{
            app.exibirMenu();
        }
        while (App.isAtivo == true);
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
    private Backend backend = new Backend();

    enum MenuOptions : byte {
        Adotar = 1,
        Ver,
        Sair = 0
    }

    public void exibirTitulo(){
        Console.WriteLine(
        FiggleFonts.ThreePoint.Render("M A S C O T C H I"));
    }

    public void login(){
        Console.Write("> Como posso te chamar?: ");
        App.username = backend.getUsername();
        Console.WriteLine("\n==========================");
    }

    public void exibirMenu() {
        Console.WriteLine($"> {App.username}, o que você deseja?");
        MenuOptions[] options = (MenuOptions[])Enum.GetValues(typeof(MenuOptions));
        foreach (MenuOptions option in options){
            Console.WriteLine($"{(int)option} - {option}");
        }
        string choice = Console.ReadLine();
        Console.WriteLine("\n==========================");
        if (Enum.TryParse<MenuOptions>(choice, out var opcaoSelecionada)){
            switch (opcaoSelecionada)
            {
                case MenuOptions.Adotar:
                    pesquisarMascote();
                    break; 
                case MenuOptions.Sair:
                    App.isAtivo = false;
                    break; 
            }
        }
    }

    void pesquisarMascote() {
        bool continuar = true;
        do {
            Console.Write("> Informe o nome do mascote para a gente achar ele aqui: ");
            string param = Console.ReadLine();
            backend.pesquisarMascote(param);
            Console.WriteLine("\n==========================");
            Console.WriteLine("> Quer continuar a procurar por mascotes?: ");
            Console.WriteLine("1 - Sim");
            Console.WriteLine("0 - Não");
            continuar = (Console.ReadLine() == "1") ? true : false;
            Console.WriteLine("\n==========================");
        } while (continuar == true);
    }

}

public class Backend {
    public string getUsername() {
        string username = Console.ReadLine();
        if(username == string.Empty) {
            return username = "Anônimo";
        }
        return username;
    }

    public void pesquisarMascote(string param = "") {
        if(param != ""){
            param = param.ToLower();
            var client = new RestClient("https://pokeapi.co/api/v2");
            var request = new RestRequest($"/pokemon/{param}", Method.Get);
            var response = client.Execute(request);
            if(response.StatusCode == HttpStatusCode.OK) {
                Mascote mascote = JsonSerializer.Deserialize<Mascote>(response.Content);
                Console.WriteLine($"Nome: {char.ToUpper(mascote.name[0])}{mascote.name.Substring(1)}");
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
                Console.WriteLine("Não identificamos esse mascote");
            }
        } else {
            Console.WriteLine("Mascote não informado");
        }

    }
}