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
        View app = new View();
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

public class View {
    private Controller controller = new Controller();

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

    string toCamelCase(string input) {
        string formattedString = String.Join(" ", input.Split('-').Select(p => p.Substring(0,1).ToUpper() + p.Substring(1)));
        return formattedString;
    }

    public void showAppTitle(){
        Console.WriteLine(
        FiggleFonts.ThreePoint.Render("M A S C O T C H I"));
    }

    public void login(){
        AnsiConsole.Markup($"[{textStates["prompt"]}]> Como posso te chamar? [/]");
        App.username = controller.getUsername();
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
                default:
                    AnsiConsole.MarkupLine("[red]Opção inválida.[/]");
                    break;
            }
        }
    }

    void showAdoptionMenu(Mascote mascote) {
        bool continuar = true;
        do {
            AnsiConsole.MarkupLine($"[{textStates["prompt"]}]> [{textStates["variable"]}]{App.username}[/], deseja adotar o [/][{textStates["variable"]}]{toCamelCase(mascote.name)}?[/]");
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
                        showMascoteDetails(mascote);
                        break;
                    case AdoptionMenuOptions.Adotar:
                        AdoptedMascote adoptedMascote = controller.adoptMascote(mascote);
                        adoptMascote(adoptedMascote);
                        continuar = false;
                        break; 
                    case AdoptionMenuOptions.Voltar:
                        continuar = false;
                        break; 
                    default:
                        AnsiConsole.MarkupLine("[red]Opção inválida.[/]");
                        break;
                }
            }
        } while (continuar == true);
    }

    void searchMascote() {
        bool continuar = true;
        do {
            AnsiConsole.Markup("[blue]> Informe o nome do mascote para a gente achar ele aqui: [/]");
            string param = Console.ReadLine();
            if (param != "") {
                Mascote mascote = controller.searchMascote(param);
                if (mascote != null) {
                    showAdoptionMenu(mascote);
                } else {
                    AnsiConsole.MarkupLine("[red]Mascote não localizado.[/]");
                }
            } else {
                AnsiConsole.Markup("[red]Nenhum mascote informado.[/]");
            }
            Console.WriteLine("\n==========================");
            AnsiConsole.MarkupLine($"[{textStates["prompt"]}]>[/] [{textStates["variable"]}]{App.username}[/][{textStates["prompt"]}], quer continuar a procurar por mascotes?: [/]");
            Console.WriteLine("1 - Sim");
            Console.WriteLine("0 - Não");
            continuar = (Console.ReadLine() == "1") ? true : false;
            Console.WriteLine("\n==========================");
        } while (continuar == true);
    }

    void showMascoteDetails(Mascote mascote){
        Console.WriteLine($"Nome: {char.ToUpper(mascote.name[0])}{mascote.name.Substring(1)}");
        Console.WriteLine("Altura (m): {0}", (mascote.height / 10.0));
        Console.WriteLine("Peso (kg): {0}", (mascote.weight / 10.0));
        Console.Write("Tipo: ");
        foreach (Mascote.Types types in mascote.types) {
                Console.WriteLine(toCamelCase(types.type.name));
        } 
        Console.WriteLine("Habilidades: ");
        foreach (Mascote.Abilities abilities in mascote.abilities) {
                Console.WriteLine(toCamelCase(abilities.ability.name));
        }
    }

    void adoptMascote(AdoptedMascote adoptedMascote) {
        AnsiConsole.MarkupLine($"[{textStates["prompt"]}]>[/] [{textStates["variable"]}]{App.username}[/][{textStates["prompt"]}], você adotou [{textStates["variable"]}]{toCamelCase(adoptedMascote.name)}[/]! Agora é só aguardar o ovo chocar! [/]");
    }
}

public class Controller {
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