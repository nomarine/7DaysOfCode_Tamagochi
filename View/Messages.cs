public class Messages{
    public class Error{
        public const string InvalidOption = "Opção inválida!";
        public const string MascoteNotFound = "Nenhum mascote informado";
        public static string NoParam(string param){
            return $"{param} em branco.";
        } 
    }
}