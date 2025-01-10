using FrontEnd.Models;

namespace FrontEnd
{
    public class CompteBancaire
    {
        public int Id { get; set; }
        public string NumeroCompte { get; set; }
        public DateTime DateOuverture { get; set; }
        public decimal Solde { get; set; } = 1000.00m;

        public override string ToString() => $"Compte: {NumeroCompte}, Ouvert le: {DateOuverture:dd/MM/yyyy}, Solde: {Solde:C}";

        public List<OperationBancaire> Operations { get; set; } = new List<OperationBancaire>();

        // Ajoutez cette propriété pour la clé étrangère
        public int ClientId { get; set; } // FK vers Client
        public Client Client { get; set; } // Navigation vers Client

        // Collection de Cartes Bancaires
        public List<CarteBancaire> Cartes { get; set; } = new List<CarteBancaire>();

    }
}