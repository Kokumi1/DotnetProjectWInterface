using FrontEnd;

public static class ClientManager
{
    private static List<Client> clients = new List<Client>();

    public static List<Client> Clients => clients;

    public static void AjouterClient()
    {
        Console.WriteLine("\nAjouter un client:");
        Console.WriteLine("1. Particulier");
        Console.WriteLine("2. Professionnel");
        Console.Write("Type de client: ");
        var typeClient = Console.ReadLine();

        Client client;

        Console.Write("Nom: ");
        string nom = Console.ReadLine();
        Console.Write("Email: ");
        string email = Console.ReadLine();
        //var adresse = SaisirAdresse();

        if (typeClient == "1")
        {
            Console.Write("Prénom: ");
            string prenom = Console.ReadLine();
            Console.Write("Date de naissance (dd/MM/yyyy): ");
            DateTime dateNaissance = DateTime.Parse(Console.ReadLine());
            Console.Write("Sexe (M/F): ");
            string sexe = Console.ReadLine();

            client = new ClientParticulier
            {
                Id = clients.Count + 1,
                Nom = nom,
                Email = email,
                //Adresse = adresse,
                Prenom = prenom,
                DateNaissance = dateNaissance,
                Sexe = sexe
            };
        }
        else if (typeClient == "2")
        {
            Console.Write("SIRET: ");
            string siret = Console.ReadLine();
            Console.Write("Statut juridique (SARL, SA, SAS, EURL): ");
            string statut = Console.ReadLine();
            Console.WriteLine("Adresse du siège:");
            //var adresseSiege = SaisirAdresse();

            client = new ClientProfessionnel
            {
                Id = clients.Count + 1,
                Nom = nom,
                Email = email,
                //Adresse = adresse,
                Siret = siret,
                StatutJuridique = statut,
                //AdresseSiege = adresseSiege
            };
        }
        else
        {
            Console.WriteLine("Type de client invalide.");
            return;
        }

        clients.Add(client);
        Console.WriteLine("Client ajouté avec succès!");
    }

    public static void AfficherClients()
    {
        if (clients.Count == 0)
        {
            Console.WriteLine("Aucun client à afficher.");
            return;
        }

        foreach (var client in clients)
        {
            Console.WriteLine(client);
            Console.WriteLine("Comptes bancaires:");
            foreach (var compte in client.Comptes)
            {
                Console.WriteLine($"  - {compte}");
            }
            Console.WriteLine();
        }
    }

    public static Client ObtenirClientParId(int clientId)
    {
        return clients.FirstOrDefault(c => c.Id == clientId);
    }
}