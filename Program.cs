using System.Net;
using System.Text.Json;
using RestSharp;
using Figgle;
using Spectre.Console;

public class App {
    public static bool isActive { get; set; }
    public static string username { get; set; }
    
    public static void Main(string[] args) {
        isActive = true;
        Frontend app = new Frontend();
        app.showAppTitle();
        app.login();
        do{
            app.showMainMenu();
        }
        while (App.isActive == true);
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

public class AdoptedMascote : Mascote {
    public AdoptedMascote(Mascote mascoteEscolhido) {
        name = mascoteEscolhido.name;
        height = mascoteEscolhido.height;
        weight = mascoteEscolhido.weight;
        abilities = mascoteEscolhido.abilities;
        types = mascoteEscolhido.types;
    }
}

public class Frontend {
    private Backend backend = new Backend();

    Dictionary<string, string> textStates = new Dictionary<string, string>()
    {
        {"prompt", "blue"},
        {"variable", "orange3"},
        {"success", "green"},
        {"error", "red"}
    };

    enum MainMenuOptions : int {
        Adotar = 1,
        Ver,
        Sair = 0
    }

    enum AdoptionMenuOptions : int {
        SaberMais = 1,
        Adotar,
        Voltar = 0
    }

    public void showAppTitle(){
        Console.WriteLine(
        FiggleFonts.ThreePoint.Render("M A S C O T C H I"));
    }

    public void login(){
        AnsiConsole.Markup($"[{textStates["prompt"]}]> Como posso te chamar? [/]");
        App.username = backend.getUsername();
        Console.WriteLine("\n==========================");
    }

    public void showMainMenu() {
        AnsiConsole.MarkupLine($"[{textStates["prompt"]}]> [{textStates["variable"]}]{App.username}[/], o que você deseja?[/]");
        MainMenuOptions[] options = (MainMenuOptions[])Enum.GetValues(typeof(MainMenuOptions));
        foreach (MainMenuOptions option in options){
            Console.WriteLine($"{(int)option} - {option}");
        }
        string choice = Console.ReadLine();
        Console.WriteLine("\n==========================");
        if (Enum.TryParse<MainMenuOptions>(choice, out var opcaoSelecionada)){
            switch (opcaoSelecionada)
            {
                case MainMenuOptions.Adotar:
                    searchMascote();
                    break; 
                case MainMenuOptions.Sair:
                    App.isActive = false;
                    break; 
            }
        }
    }

    void searchMascote() {
        bool continuar = true;
        do {
            AnsiConsole.Markup("[blue]> Informe o nome do mascote para a gente achar ele aqui: [/]");
            string param = Console.ReadLine();
            backend.searchMascote(param);
            Console.WriteLine("\n==========================");
            AnsiConsole.MarkupLine($"[{textStates["prompt"]}]>[/] [{textStates["variable"]}]{App.username}[/][{textStates["prompt"]}], quer continuar a procurar por mascotes?: [/]");
            Console.WriteLine("1 - Sim");
            Console.WriteLine("0 - Não");
            continuar = (Console.ReadLine() == "1") ? true : false;
            Console.WriteLine("\n==========================");
        } while (continuar == true);
    }

    void showAdoptionMenu() {
        AnsiConsole.MarkupLine($"[{textStates["prompt"]}]> [{textStates["variable"]}]{App.username}[/], deseja continuar a adoção?[/]");
        AdoptionMenuOptions[] options = (AdoptionMenuOptions[])Enum.GetValues(typeof(AdoptionMenuOptions));
        foreach (AdoptionMenuOptions option in options){
            Console.WriteLine($"{(int)option} - {option}");
        }
        string choice = Console.ReadLine();
        Console.WriteLine("\n==========================");
        if (Enum.TryParse<AdoptionMenuOptions>(choice, out var opcaoSelecionada)){
            switch (opcaoSelecionada)
            {
                case AdoptionMenuOptions.SaberMais:
                    searchMascote();
                    break; 
                case AdoptionMenuOptions.Adotar:
                    //adoptMascote(adoptedMascote);
                    break; 
                case AdoptionMenuOptions.Voltar:
                    break; 
            }
        }
    }

    void adoptMascote(AdoptedMascote adoptedMascote) {
        //AnsiConsole.MarkupLine($"[{textStates["prompt"]}]>[/] [{textStates["variable"]}]{App.username}[/][{textStates["prompt"]}], você adotou {mascoteAdotado}! Agora é só aguardar o ovo chocar! [/]");
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

    public void searchMascote(string param = "") {
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

    public AdoptedMascote adotarMascote(Mascote mascote) {
        AdoptedMascote mascoteAdotado = new AdoptedMascote(mascote);

        return mascoteAdotado;
    }
}