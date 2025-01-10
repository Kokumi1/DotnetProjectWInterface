namespace BackEnd.Common
{
    public static class AppConstants
    {
        // Constantes pour génerer un fichier CSV tous les X millisecondes
        public const int IntervalFiveMilliseconds = 5000;
        public const int IntervalFifteenSeconds = 15000;
        public const int IntervaltenSeconds = 10000;
        public const int IntervalFiveMinutes = 300000;

        // Constante pour définir une heure de génération du fichier Json
        public const int ScheduleHourForJson = 8;
        public const int ScheduleMinuteForJson = 0;
        public const int ScheduleSecondForJson = 0;


        // Chemin relatif pour stocker les fichiers
        public const string CsvDirectoryPath = "GeneratedCsvFiles";
        public const string ArchiveDirectoryPath = "Archive";

        //API key for openExchangeRate
        public const string ApiKey = "07d3c9e28c484b45a57fe067a38bc446";
    }
}
