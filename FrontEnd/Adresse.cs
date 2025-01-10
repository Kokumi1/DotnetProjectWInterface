namespace FrontEnd
{
    public class Adresse
    {
        public int Id { get; set; } // PK
        public string Libelle { get; set; }
        public string Complement { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }

        public override string ToString() => $"{Libelle}, {Complement}, {CodePostal} {Ville}";
    }
}