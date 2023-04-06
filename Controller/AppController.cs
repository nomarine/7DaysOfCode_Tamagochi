using Model.App;
using Model.Mascote;
using View.AppView;
using Service.PokemonAPI;

using System.Net;
using System.Text.Json;

namespace Controller.AppController {

    public class AppController {
        static AppView appView = new AppView();
        static void startApp() {
            appView.showAppTitle();
            App.username = getUsername();
            appView.login();
            do {
                appView.showMainMenu(App.username, mainMenuOptions);
                runMainMenuOption();
            }
            while (App.isActive == true);
        }

        static string getUsername() {
            string username = Console.ReadLine();
            if(username == string.Empty) {
                return username = "Anônimo";
            }
            return username;
        }

        public static void runMainMenuOption(){
            string input = Console.ReadLine();
            if(Enum.TryParse<MainMenuOptions>(input, out var choice)){
                switch(choice){
                    case MainMenuOptions.Buscar:
                        searchMascote();
                        break; 
                    case MainMenuOptions.Sair:
                        App.isActive = false;
                        break; 
                    default:
                   //     AnsiConsole.MarkupLine("[red]Opção inválida.[/]");
                        break;
                }    
            }
        }

        enum SearchMascoteStatus : int {
            MascoteFound = 1,
            NoParam = 2,
            NotFound = 3,
        }

        public static Dictionary<string, int> searchMascoteStatus = new Dictionary<string, int>() {
            { "Mascote encontrado", ((int)SearchMascoteStatus.MascoteFound) },
            { "Sem parâmetros informados", ((int)SearchMascoteStatus.NoParam) },
            { "Mascote não encontrado", ((int)SearchMascoteStatus.NotFound) },
        };

        static Mascote searchMascote(){
            bool continuar = true;
            do {
                int result = 0;
                appView.searchMascote(result);
                string mascoteName = Console.ReadLine();
                PokemonAPI pokemonAPI = new PokemonAPI();
                var response = pokemonAPI.getPokemon(mascoteName);
                if(response.StatusCode == HttpStatusCode.OK) {
                    Mascote mascote = JsonSerializer.Deserialize<Mascote>(response.Content);
                    //CONTINUAR DAQUI, AGORA LIDAR COM O MASCOTE CONTROLLER
                    return mascote;
                } else {
                    result = ((int)SearchMascoteStatus.NotFound);
                    return null;
                }
                    SearchMascoteStatus result = SearchMascoteStatus.NotFound;
                    
            } while (continuar == true);


        }

        enum MainMenuOptions : int {
            Buscar = 1,
            Ver,
            Sair = 0
        }

        public static Dictionary<string, int> mainMenuOptions = new Dictionary<string, int>() {
            { "Sair", ((int)MainMenuOptions.Sair) },
            { "Ver Mascotes Adotados", ((int)MainMenuOptions.Ver) },
            { "Buscar Novos Mascotes", ((int)MainMenuOptions.Buscar) },
        };

        enum AdoptionMenuOptions : int {
            SaberMais = 1,
            Adotar,
            Voltar = 0
        }
        public static Dictionary<string, int> adoptionMenuOptions = new Dictionary<string, int>() {
            { "Saber Mais", ((int)AdoptionMenuOptions.SaberMais) },
            { "Adotar Mascote", ((int)AdoptionMenuOptions.Adotar) },
            { "Voltar", ((int)AdoptionMenuOptions.Voltar) },
        };

    }

}