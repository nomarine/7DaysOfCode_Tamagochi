namespace Util{
    public class Beautifier{
        string toCamelCase(string input) {
            string formattedString = String.Join(" ", input.Split('-').Select(p => p.Substring(0,1).ToUpper() + p.Substring(1)));
            return formattedString;
        }
    }
}