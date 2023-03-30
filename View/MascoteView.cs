using Model.Mascote;

namespace View.MascoteView {
    public class MascoteView {
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

}