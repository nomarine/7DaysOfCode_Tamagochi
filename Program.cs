using Model.App;

public class Program {
    public static void Main(string[] args) {
        App app = new App();
        View appView = new View();
        appView.showAppTitle();
        appView.login(app);
        do{
            appView.showMainMenu();
        }
        while (app.isActive == true);
    }
}

