using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrontEnd.Datas;
using Microsoft.EntityFrameworkCore;

namespace FrontEnd.Services
{
    public class ClientService
    {
        private readonly FrontEndMyDbContext _context;

        public ClientService(FrontEndMyDbContext context)
        {
            _context = context;
        }

        public void AfficherClientEtCartes(int clientId)
        {
            // Récupérer le client avec ses comptes bancaires et cartes bancaires
            var client = _context.Clients
                .Include(c => c.Comptes)
                    .ThenInclude(compte => compte.Cartes)
                .FirstOrDefault(c => c.Id == clientId);

            if (client == null)
            {
                Console.WriteLine("Client non trouvé.");
                return;
            }

            // Afficher les données du client
            Console.WriteLine("Données du Client:");
            Console.WriteLine($"Nom: {client.Nom}");
            Console.WriteLine($"Email: {client.Email}");
            //Console.WriteLine($"Adresse: {client.Adresse.Libelle}, {client.Adresse.CodePostal} {client.Adresse.Ville}");
            Console.WriteLine();

            // Afficher les données des comptes bancaires du client
            Console.WriteLine("Comptes Bancaires:");
            foreach (var compte in client.Comptes)
            {
                Console.WriteLine($"Compte: {compte.NumeroCompte}, Solde: {compte.Solde:C}");
                Console.WriteLine($"Date d'Ouverture: {compte.DateOuverture:dd/MM/yyyy}");

                // Afficher les cartes bancaires associées au compte
                Console.WriteLine("Cartes Bancaires:");
                foreach (var carte in compte.Cartes)
                {
                    Console.WriteLine($"Carte: {carte.NumeroCarte}");

                    // Récupérer les opérations bancaires pour cette carte
                    var operations = _context.Operations
                        .Where(o => o.CardNumber == carte.NumeroCarte)
                        .ToList();

                    if (operations.Any())
                    {
                        Console.WriteLine("Opérations de la carte bancaire:");
                        foreach (var operation in operations)
                        {
                            Console.WriteLine($"Opération ID: {operation.Id}, Montant: {operation.Amount:C}, Date: {operation.Date:dd/MM/yyyy}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Aucune opération pour cette carte bancaire.");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
