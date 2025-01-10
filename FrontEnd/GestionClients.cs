using FrontEnd;
using FrontEnd.Datas;
using FrontEnd.Models;
using FrontEnd.Services;
using Microsoft.EntityFrameworkCore;

public static class GestionClients
{

    private static List<Client> clients = new List<Client>();
    public static void InitialiserDonnees()
    {
        
        clients.Add(new ClientParticulier
        {
            Id = 1,
            Nom = "BETY",
            Email = "bety@gmail.com",
            Adresse = new Adresse
            {
                Libelle = "12, rue des Oliviers",
                Complement = "",
                CodePostal = "94000",
                Ville = "CRETEIL"
            },
            Prenom = "Daniel",
            DateNaissance = new DateTime(1985, 11, 12),
            Sexe = "M"
        });

        clients.Add(new ClientParticulier
        {
            Id = 3,
            Nom = "BODIN",
            Email = "bodin@gmail.com",
            Adresse = new Adresse
            {
                Libelle = "10, rue des Olivies",
                Complement = "Etage 2",
                CodePostal = "94300",
                Ville = "VINCENNES"
            },
            Prenom = "Justin",
            DateNaissance = new DateTime(1965, 5, 5),
            Sexe = "M"
        });

       
        clients.Add(new ClientProfessionnel
        {
            Id = 2,
            Nom = "AXA",
            Email = "info@axa.fr",
            Adresse = new Adresse
            {
                Libelle = "125, rue LaFayette",
                Complement = "Digicode 1432",
                CodePostal = "94120",
                Ville = "FONTENAY SOUS BOIS"
            },
            Siret = "12548795641122",
            StatutJuridique = "SARL",
            AdresseSiege = new Adresse
            {
                Libelle = "125, rue LaFayette",
                Complement = "Digicode 1432",
                CodePostal = "94120",
                Ville = "FONTENAY SOUS BOIS"
            }
        });

        clients.Add(new ClientProfessionnel
        {
            Id = 4,
            Nom = "PAUL",
            Email = "info@paul.fr",
            Adresse = new Adresse
            {
                Libelle = "36, quai des Orfèvres",
                Complement = "",
                CodePostal = "93500",
                Ville = "ROISSY EN FRANCE"
            },
            Siret = "87459564455444",
            StatutJuridique = "EURL",
            AdresseSiege = new Adresse
            {
                Libelle = "10, esplanade de la Défense",
                Complement = "",
                CodePostal = "92060",
                Ville = "LA DEFENSE"
            }
        });
    }



    public static bool Authentification()
    {
        Console.Write("Login: ");
        string login = Console.ReadLine();
        Console.Write("Mot de passe: ");
        string motDePasse = Console.ReadLine();

        if (login == "login" && motDePasse == "login")
        {
            return true;
        }

        Console.WriteLine("Login ou mot de passe incorrect.");
        return false;
    }

    public static void MenuPrincipal()
    {
        while (true)
        {
            Console.WriteLine("\nMenu Principal:");
            Console.WriteLine("1. Ajouter un client");
            Console.WriteLine("2. Ajouter un compte bancaire");
            Console.WriteLine("3. Afficher tous les clients");
            Console.WriteLine("4. Effectuer une opération");
            Console.WriteLine("5. Importer des transactions");
            Console.WriteLine("6. Générer un rapport XML");
            Console.WriteLine("7. Afficher les opérations d'un client");
            Console.WriteLine("8. Quitter");
            Console.Write("Votre choix: ");
            var choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    ClientManager.AjouterClient();
                    break;
                case "2":
                    Console.Write("ID du client: ");
                    int clientId = int.Parse(Console.ReadLine());
                    CompteManager.AjouterCompte(clientId);
                    break;
                case "3":
                    ClientManager.AfficherClients();
                    break;       
                case "4":
                    Console.Write("ID du client: ");
                    clientId = int.Parse(Console.ReadLine());
                    CompteManager.EffectuerOperation(clientId);
                    break;
                case "5":
                    ImporterTransactions();
                    break;
                case "6":
                    GenererRapport();
                    break;
                case "7":
                    Console.Write("ID du client pour afficher les opérations: ");
                    int clientIdbis = int.Parse(Console.ReadLine());
                    AfficherOperationDunClient(clientIdbis);
                    break;
                case "8":
                    return;
                default:
                    Console.WriteLine("Choix invalide. Veuillez réessayer.");
                    break;

            }
        }
    }

    public static void ImporterTransactions()
    {
        Console.WriteLine("Importation des transactions...");
        string filePath = "../../../../Transaction.json"; // Path to the JSON file

        // Créer une instance de TransactionService avec le chemin du fichier
        IncomingTransactionService incomingTransactionService = new IncomingTransactionService(filePath);

        // Charger et ajouter les transactions dans la base de données
        incomingTransactionService.LoadAndAddTransactionsToDatabase();
        Console.WriteLine("Import terminé.");
    }

    public static void GenererRapport()
    {
        using (FrontEndMyDbContext dbContext = new FrontEndMyDbContext())
        {
            XmlGenerator.SerializeToXml<Operation>(dbContext.Operations.AsNoTracking().ToList());
        }
        Console.WriteLine("Rapport XML généré...");
    }

    public static void AfficherOperationDunClient(int clientId)
    {
        // Dans votre code, vous instanciez le service et appelez la méthode avec l'ID du client
        var clientService = new ClientService(new FrontEndMyDbContext());
        clientService.AfficherClientEtCartes(clientId); // Remplacez 1 par l'ID du client à afficher
    }
}