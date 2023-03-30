using Model.App;
using Model.Mascote;

using Figgle;
using Spectre.Console;

namespace View.AppView {
    public class AppView {
        Dictionary<string, string> textStates = new Dictionary<string, string>()
        {
            {"prompt", "blue"},
            {"variable", "orange3"},
            {"success", "green"},
            {"error", "red"}
        };

        public void showAppTitle(){
            Console.WriteLine(
            FiggleFonts.ThreePoint.Render("M A S C O T C H I"));
        }

        public void login(){
            AnsiConsole.Markup($"[{textStates["prompt"]}]> Como posso te chamar? [/]");
        }

        public void showMainMenu(string username) {
            Console.WriteLine("\n==========================");
            AnsiConsole.MarkupLine($"[{textStates["prompt"]}]> [{textStates["variable"]}]{username}[/], o que você deseja?[/]");
            
            var options = (App.MainMenuOptions[])Enum.GetValues(typeof(App.MainMenuOptions));
            
            foreach (App.MainMenuOptions option in options){
                Console.WriteLine($"{(int)option} - {option}");
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
    }
}