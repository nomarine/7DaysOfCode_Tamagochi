using Model.App;
using Model.Mascote;

using Util.Beautifier;

using Figgle;
using Spectre.Console;

namespace View.AppView {
    public class AppView {
        private Dictionary<string, string> textStates;
        private Beautifier beautifier;
        private Messages messages;

        public AppView(){
            Styles styles = new Styles();
            textStates = styles.textStates;
        }

        public void showAppTitle(){
            Console.WriteLine(
            FiggleFonts.ThreePoint.Render("M A S C O T C H I"));
        }

        public void login(){
            AnsiConsole.Markup($"[{textStates["prompt"]}]> Como posso te chamar? [/]");
        }

        public void showMainMenu(string username, Dictionary<string, int> options) {
            Console.WriteLine("\n==========================");
            AnsiConsole.MarkupLine($"[{textStates["prompt"]}]> [{textStates["variable"]}]{username}[/], o que você deseja?[/]");
            
            options.ToList().ForEach(
                kvp => Console.WriteLine($"{kvp.Value} - {kvp.Key}")
            );
        }

        void showAdoptionMenu(Mascote mascote, Dictionary<int, string> options) {
            AnsiConsole.MarkupLine($"[{textStates["prompt"]}]> [{textStates["variable"]}]{App.username}[/], deseja adotar o [/][{textStates["variable"]}]{beautifier.toCamelCase(mascote.name)}?[/]");
            options.ToList().ForEach(
                kvp => Console.WriteLine($"{kvp.Key} - {kvp.Value}")
            );
        }

        public void searchMascote(int state) {
            switch(state){
                case 0:
                    AnsiConsole.Markup($"[{textStates["prompt"]}]> Informe o nome do mascote para a gente achar ele aqui: [/]");
                    break;
                case 1:
                    AnsiConsole.MarkupLine($"[{textStates["prompt"]}]>[/] [{textStates["variable"]}]{App.username}[/][{textStates["prompt"]}], quer continuar a procurar por mascotes?: [/]");
                    Console.WriteLine("1 - Sim");
                    Console.WriteLine("0 - Não");
                    break;
        }
            

            continuar = (Console.ReadLine() == "1") ? true : false;
            Console.WriteLine("\n==========================");

        public void InvalidOption(){
            AnsiConsole.MarkupLine($"[{textStates["prompt"]}]{messages}}[/]");
        }
    }
}