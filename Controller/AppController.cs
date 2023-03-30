using View.AppView;
using Model.App;

namespace Controller.AppController {

    public class AppController {

        public static void startApp() {
            AppView appView = new AppView();

            appView.showAppTitle();
            App.username = getUsername();
            appView.login();

            do {
                appView.showMainMenu(App.username, App.MainMenuOptions);
            }
            while (App.isActive == true);
        }

        public static string getUsername() {
            string username = Console.ReadLine();
            if(username == string.Empty) {
                return username = "Anônimo";
            }
            return username;
        }

        public void runMainMenuOption(){
            string choice = Console.ReadLine();
            Console.WriteLine("\n==========================");
            if (Enum.TryParse<App.MainMenuOptions>(choice, out var opcaoSelecionada)){
                switch (opcaoSelecionada)
                {
                    case App.MainMenuOptions.Adotar:
                        searchMascote();
                        break; 
                    case App.MainMenuOptions.Sair:
                        App.isActive = false;
                        break; 
                    default:
                        AnsiConsole.MarkupLine("[red]Opção inválida.[/]");
                        break;
                }
            }
        }

        public void searchMascote(){
            PokemonAPI.searchMascote();
            if(response.StatusCode == HttpStatusCode.OK) {
                Mascote mascote = JsonSerializer.Deserialize<Mascote>(response.Content);
                return mascote;
            } else {
                return null;
            }

        }

    }

}