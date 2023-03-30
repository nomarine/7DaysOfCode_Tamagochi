namespace Model.App {
    public class App {
        public static bool isActive { get; set; }
        public static string username { get; set; }

        public enum MainMenuOptions : int {
            Adotar = 1,
            Ver,
            Sair = 0
        }

        public enum AdoptionMenuOptions : int {
            SaberMais = 1,
            Adotar,
            Voltar = 0
        }

        public App(bool isActive = true){

        }
    }
}