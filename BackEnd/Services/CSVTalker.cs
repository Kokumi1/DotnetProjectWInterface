using System.Globalization;
using BackEnd.Common;
using BackEnd.Entities;
using BackEnd.Repositories;

namespace BackEnd.Services
{
    public static class CSVTalker
    {
        /// <summary>
        /// Read all CSV file in the GeneratedCsvFiles
        /// </summary>
        public static void ReadCSV()
        {
            Console.WriteLine("Lecture des fichiers CSV");
            string[] fileNames = FileFinder();
            List<Transaction> transactions = new List<Transaction>();

            foreach (string file in fileNames)
            {
                var lines = File.ReadAllLines(file);

                foreach (var line in lines)
                {
                    var values = line.Split(';');
                    double exchangeRate = 1;
                    if (values[4] != "EUR")
                    {
                        Task.Run(async () =>//
                        {
                            exchangeRate = await JsonTalker.CallOpenExchangeRatesApi(values[4]);
                            CreateTransaction(values, exchangeRate);
                        });
                    }
                    else
                    {
                        CreateTransaction(values, exchangeRate);
                    }
                }
            }
            Console.WriteLine("call archiveCsvFile Method");
            ArchiveCSVFile(fileNames);
        }

        /// <summary>
        /// Get the name of all files in directory
        /// </summary>
        /// <returns>Tab of file's name</returns>
        public static string[] FileFinder()
        {
            if (Directory.Exists(AppConstants.CsvDirectoryPath))
            {
                // Récupérer tous les fichiers dans le dossier
                string[] files = Directory.GetFiles(AppConstants.CsvDirectoryPath);

                return files;
            }
            else
            {
                Console.WriteLine($"Le dossier {AppConstants.CsvDirectoryPath} n'existe pas.");
                return [];
            }
        }

        /// <summary>
        /// Create transaction object and check if it's normal or not
        /// </summary>
        /// <param name="pValues">values transaction get from CSV file</param>
        /// <param name="pExchangeRate">Exchange rate between Euro and transaction's currency</param>
        public static void CreateTransaction(string[] pValues, double pExchangeRate)
        {
            Transaction transaction = new Transaction()
            {
                CardNumber = new CreditCard(pValues[0]).getCreditCard(),
                Amount = double.Parse(pValues[1], CultureInfo.InvariantCulture),
                Type = (Transaction.TransactionType)Enum.Parse(typeof(Transaction.TransactionType), pValues[2]),
                Date = DateOnly.FromDateTime(DateTime.ParseExact(pValues[3], "yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture)),
                Devise = pValues[4],
                ExchangeRate = pExchangeRate
            };

            if (AnnomalyChecker.CheckAnomaly(transaction))
            {
                DatabaseTalker.InsertTransactionToDatabase(transaction);
            }
            else
            {
                DatabaseTalker.InsertAnomalyToDatabase(new Anomaly()
                {
                    CardNumber   = transaction.CardNumber,
                    Amount       = transaction.Amount,
                    Type         = transaction.Type,
                    Date         = transaction.Date,
                    Devise       = transaction.Devise,
                    ExchangeRate = transaction.ExchangeRate
                });
            }
        }

        public static void ArchiveCSVFile(string[] FileTab)
        {
            Console.WriteLine("start archive csv file");
            foreach (string file in FileTab)
            {
                string archivedFile = file.Replace(AppConstants.CsvDirectoryPath,AppConstants.ArchiveDirectoryPath);

                try
                {
                    File.Move(file, archivedFile);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.WriteLine("Achivage terminée");
        }
    }

}
