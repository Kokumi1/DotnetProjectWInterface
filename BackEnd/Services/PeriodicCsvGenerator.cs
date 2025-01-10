using System.Globalization;
using System.Text;
using BackEnd.Common;

namespace BackEnd.Services
{
    public class PeriodicCsvGenerator : BackgroundService
    {
        private static readonly Random RandomGenerator = new Random();
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Effacer les fichiers CSV existants avant de commencer à générer des nouveaux fichiers
            //DeleteExistingCsvFiles();

            while (!stoppingToken.IsCancellationRequested)
            {

                Console.WriteLine("Génération d'un fichier CSV...");
                GenerateCsvFile();

                await Task.Delay(AppConstants.IntervalFifteenSeconds, stoppingToken); 
                //await Task.Delay(AppConstants.IntervalFiveMinutes, stoppingToken);
            }
        }

        private void DeleteExistingCsvFiles()
        {
            // Vérifie si le répertoire existe avant de tenter de lister les fichiers
            if (Directory.Exists(AppConstants.CsvDirectoryPath))
            {
                string[] existingFiles = Directory.GetFiles(AppConstants.CsvDirectoryPath, "*.csv");
                foreach (var file in existingFiles)
                {
                    try
                    {
                        File.Delete(file);
                        Console.WriteLine($"Fichier supprimé : {file}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erreur lors de la suppression du fichier {file}: {ex.Message}");
                    }
                }
            }
        }

        private void GenerateCsvFile()
        {
            // Chemin du fichier CSV (le nom inclut un timestamp pour éviter les doublons)
            string fileName = $"operations_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
            string filePath = Path.Combine(AppConstants.CsvDirectoryPath, fileName);

            // Création du répertoire si nécessaire
            Directory.CreateDirectory(AppConstants.CsvDirectoryPath);

            // Génération des données pour le fichier
            int recordCount = RandomGenerator.Next(51, 52); // Nombre d'enregistrements entre 1 et 10
            var csvContent = new StringBuilder();

            for (int i = 0; i < recordCount; i++)
            {
                csvContent.AppendLine(GenerateRandomOperation());
            }

            // Écriture dans le fichier
            File.WriteAllText(filePath, csvContent.ToString(), Encoding.UTF8);
            Console.WriteLine($"Fichier CSV généré : {filePath}");
        }

        private string GenerateRandomOperation()
        {
            // Données randomisées pour une opération
            string cardNumber = GenerateRandomCardNumber();
            decimal amount = GenerateRandomAmount(); // Montant avec deux décimales
            string[] operationTypes = { "creditCardInvoice", "counterDeposite", "ATMWithdrawals" };
            string operationType = operationTypes[RandomGenerator.Next(operationTypes.Length)];
            string date = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture); // Format de la date
            string[] currencies = { "EUR", "USD", "CNY" };
            string currency = currencies[RandomGenerator.Next(currencies.Length)];

            // Retourne une ligne au format CSV
            return $"{cardNumber};{amount.ToString("F2", CultureInfo.InvariantCulture)};{operationType};{date};{currency}";

        }

        private string GenerateRandomCardNumber()
        {
            // Base du numéro de carte
            string baseCardNumber = "497401850223";
            string lastFourDigits = string.Join("", Enumerable.Range(0, 4).Select(x => RandomGenerator.Next(0, 10)));
            return baseCardNumber + lastFourDigits;
        }

        private static decimal GenerateRandomAmount()
        {
            // Générer un montant entre 1 et 5000, avec deux décimales
            decimal integerPart = RandomGenerator.Next(1, 5000); // Partie entière
            decimal fractionalPart = (decimal)RandomGenerator.NextDouble(); // Partie décimale

            // Retourne le montant total avec 2 décimales
            return decimal.Round(integerPart + fractionalPart, 2);
        }
    }
}
