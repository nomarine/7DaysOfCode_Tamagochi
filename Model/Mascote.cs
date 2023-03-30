namespace Model.Mascote{
    public class Mascote {
        public string name { get; set; }
        public int height { get; set; }
        public int weight { get; set; }
        public List<Abilities> abilities { get; set; }
        public List<Types> types { get; set; }
        
        public class Abilities {
            public Ability ability { get; set; }

            public class Ability {
                public string name { get; set; }
            }
        }

        public class Types {
            public Type type { get; set; }

            public class Type {
                public string name { get; set; }
            }
        }
        
    }

    public class AdoptedMascote : Mascote {
        public AdoptedMascote(Mascote mascoteEscolhido) {
            name = mascoteEscolhido.name;
            height = mascoteEscolhido.height;
            weight = mascoteEscolhido.weight;
            abilities = mascoteEscolhido.abilities;
            types = mascoteEscolhido.types;
        }
    }

}