using Model.Mascote;
using Util.Beautifier;

using Spectre.Console;

namespace View.MascoteView {
    public class MascoteView {
        private Dictionary<string, string> textStates;
        private Beautifier beautifier;

        public MascoteView(){
            Styles styles = new Styles();
            textStates = styles.textStates;
        }

        public void showMascoteDetails(Mascote mascote){
            Console.WriteLine($"Nome: {char.ToUpper(mascote.name[0])}{mascote.name.Substring(1)}");
            Console.WriteLine("Altura (m): {0}", (mascote.height / 10.0));
            Console.WriteLine("Peso (kg): {0}", (mascote.weight / 10.0));
            Console.Write("Tipo: ");
            foreach (Mascote.Types types in mascote.types) {
                    Console.WriteLine(beautifier.toCamelCase(types.type.name));
            } 
            Console.WriteLine("Habilidades: ");
            foreach (Mascote.Abilities abilities in mascote.abilities) {
                    Console.WriteLine(beautifier.toCamelCase(abilities.ability.name));
            }
        }

        void adoptMascote(string username, AdoptedMascote adoptedMascote) {
            AnsiConsole.MarkupLine($"[{textStates["prompt"]}]>[/] [{textStates["variable"]}]{username}[/][{textStates["prompt"]}], você adotou [{textStates["variable"]}]{beautifier.toCamelCase(adoptedMascote.name)}[/]! Agora é só aguardar o ovo chocar! [/]");
        }
    }

}