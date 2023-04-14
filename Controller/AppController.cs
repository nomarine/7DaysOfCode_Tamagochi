using Model.App;
using Model.Mascote;
using View.AppView;
using View.MascoteView;
using Service.PokemonAPI;

using System.Net;
using System.Text.Json;

namespace Controller.AppController {

    public class AppController {
        static AppView appView = new AppView();
        static MascoteView mascoteView = new MascoteView();

        static MascoteController mascoteController = new MascoteController();

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
                        appView.InvalidOption();
                        break;
                }    
            }
        }

        static void startAdoptionMenu(Mascote mascote){
            bool isActive = true;
            do {
                string choice = Console.ReadLine();
                if (Enum.TryParse<AdoptionMenuOptions>(choice, out var opcaoSelecionada)){
                    switch (opcaoSelecionada)
                    {
                        case AdoptionMenuOptions.SaberMais:
                            mascoteView.showMascoteDetails(mascote);
                            break;
                        case AdoptionMenuOptions.Adotar:
                            AdoptedMascote adoptedMascote = mascoteController.AdoptMascote(mascote);
                            isActive = false;
                            break; 
                        case AdoptionMenuOptions.Voltar:
                            isActive = false;
                            break; 
                        default:
                            appView.;
                            break;
                    }
                }
            } while (isActive == true);
        }

        enum SearchMascoteStatus : int {
            MascoteFound,
            NoParam,
            NotFound
        }

        static void searchMascote(){
            bool isActive = true;
            do {
                int result = 0;
                appView.searchMascote(result);
                string mascoteName = Console.ReadLine();
                PokemonAPI pokemonAPI = new PokemonAPI();
                var response = pokemonAPI.getPokemon(mascoteName);
                if(response.StatusCode == HttpStatusCode.OK) {
                    Mascote mascote = JsonSerializer.Deserialize<Mascote>(response.Content);
                    startAdoptionMenu(mascote);
                    isActive = false;
                } else {
                    result = ((int)SearchMascoteStatus.NotFound);
                    appView.searchMascote(result);
                }
            } while(isActive = true);
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