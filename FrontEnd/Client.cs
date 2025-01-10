using FrontEnd;

namespace FrontEnd
{
    public abstract class Client
    {
        public int Id { get; set; }
        public string Nom { get; set; }

        public int AdresseId { get; set; } // FK
        public Adresse Adresse { get; set; }
        public string Email { get; set; }
        public List<CompteBancaire> Comptes { get; set; } = new List<CompteBancaire>();

        public abstract override string ToString();
    }

    public class ClientParticulier : Client
    {
        public string Prenom { get; set; }
        public DateTime DateNaissance { get; set; }
        public string Sexe { get; set; }

        public override string ToString() =>
            $"ID: {Id}, Nom: {Nom}, Prénom: {Prenom}, Date de Naissance: {DateNaissance:dd/MM/yyyy}, Sexe: {Sexe}, Adresse: {Adresse}, Email: {Email}";
    }

    public class ClientProfessionnel : Client
    {
        public string Siret { get; set; }
        public string StatutJuridique { get; set; }

        public int? AdresseSiegeId { get; set; }
        public Adresse AdresseSiege { get; set; }

        public override string ToString() =>
            $"ID: {Id}, Nom: {Nom}, SIRET: {Siret}, Statut: {StatutJuridique}, Adresse Siège: {AdresseSiege}, Adresse: {Adresse}, Email: {Email}";
    }
}