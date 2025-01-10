using FrontEnd;

public static class CompteManager
{
    public static void AjouterCompte(int clientId)
    {
        var client = ClientManager.ObtenirClientParId(clientId);

        if (client == null)
        {
            Console.WriteLine("Client introuvable.");
            return;
        }

        Console.Write("Numéro de compte: ");
        string numeroCompte = Console.ReadLine();
        var compte = new CompteBancaire
        {
            NumeroCompte = numeroCompte,
            DateOuverture = DateTime.Now
        };

        client.Comptes.Add(compte);
        Console.WriteLine("Compte ajouté avec succès!");
    }

    public static void EffectuerOperation(int clientId)
    {
        var client = ClientManager.ObtenirClientParId(clientId);

        if (client == null)
        {
            Console.WriteLine("Client introuvable.");
            return;
        }

        Console.WriteLine("Choisissez le compte bancaire:");
        for (int i = 0; i < client.Comptes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {client.Comptes[i]}");
        }
        Console.Write("Votre choix: ");
        int choixCompte = int.Parse(Console.ReadLine()) - 1;

        if (choixCompte < 0 || choixCompte >= client.Comptes.Count)
        {
            Console.WriteLine("Compte invalide.");
            return;
        }

        var compte = client.Comptes[choixCompte];
        Console.WriteLine("Type d'opération : 1. Dépôt, 2. Retrait, 3. Paiement CB");
        string type = Console.ReadLine() switch
        {
            "1" => "Dépôt",
            "2" => "Retrait",
            "3" => "Paiement CB",
            _ => "Invalide"
        };

        if (type == "Invalide")
        {
            Console.WriteLine("Type invalide.");
            return;
        }

        Console.Write("Montant: ");
        decimal montant = decimal.Parse(Console.ReadLine());

        if (type == "Retrait" || type == "Paiement CB")
        {
            if (compte.Solde < montant)
            {
                Console.WriteLine("Fonds insuffisants.");
                return;
            }
            montant = -montant;
        }

        compte.Solde += montant;
        compte.Operations.Add(new OperationBancaire
        {
            Date = DateTime.Now,
            Type = type,
            Montant = montant,
            Description = $"Opération: {type}"
        });

        Console.WriteLine("Opération effectuée avec succès!");
    }
}