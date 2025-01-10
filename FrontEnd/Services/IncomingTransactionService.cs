using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrontEnd.Datas;
using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Services
{
    public class IncomingTransactionService
    {
        private string filePath;

        public IncomingTransactionService(string filePath)
        {
            this.filePath = filePath;
        }

        // Méthode pour charger les transactions à partir du fichier JSON et les ajouter à la base de données
        public void LoadAndAddTransactionsToDatabase()
        {
            if (File.Exists(filePath))
            {
                Console.WriteLine("Le fichier existe. Chargement des données...");

                try
                {
                    // Lire le contenu du fichier JSON
                    string jsonContent = File.ReadAllText(filePath);

                    // Désérialiser les données JSON en une liste de transactions
                    var transactions = JsonConvert.DeserializeObject<List<IncomingTransactionEntity>>(jsonContent);

                    // Convertir les transactions en objets Operation
                    List<Operation> operations = new List<Operation>();

                    foreach (var transaction in transactions)
                    {
                        DateTime.TryParse(transaction.Date, out DateTime parsedDate);
                        double AmountInEur = transaction.Amount * transaction.ExchangeRate;
                        var operation = new Operation
                        {
                            //Id = transaction.Id,
                            CardNumber = transaction.CardNumber,
                            Type = GetTransactionType(transaction.Type),
                            Amount = Math.Truncate(AmountInEur * 100) / 100,
                            Date = DateOnly.FromDateTime(parsedDate),
                            //Devise = transaction.Currency ?? "EUR",
                            //ExchangeRate = transaction.ExchangeRate
                        };
                        operations.Add(operation);
                    }

                    foreach (var operation in operations)
                    {
                        Console.WriteLine($"Ajouter Operation - Id: {operation.Id}, CardNumber: {operation.CardNumber}, Type: {operation.Type}, Amount: {operation.Amount}");
                    }

                    // Ajouter les opérations à la base de données
                    using (var db = new FrontEndMyDbContext()) // Créez une instance de votre DbContext
                    {
                        // Ajouter une seule opération
                        // db.Operations.Add(operation); 

                        // Ou ajouter une collection d'opérations
                        db.Operations.AddRange(operations); // Ajoute toutes les opérations à la base
                        Console.WriteLine("Liste des entités suivies avant SaveChanges:");
                        foreach (var entry in db.ChangeTracker.Entries())
                        {
                            Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
                        }
                        db.SaveChanges(); // Enregistre les modifications dans la base de données
                    }

                    Console.WriteLine("Les données ont été ajoutées avec succès à la base de données.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Une erreur est survenue lors de la lecture du fichier : {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Le fichier n'existe pas.");
            }
        }

        // Convertir le type de transaction en string (par exemple, "CarteBancaire", "Depot", etc.)
        private string GetTransactionType(int type)
        {
            switch (type)
            {
                case 0: return "CarteBancaire";
                case 1: return "Depot";
                case 2: return "Retrait";
                default: return "Inconnu";
            }
        }
    }
}
