
using BackEnd.Common;
using BackEnd.Repositories;

namespace BackEnd.Services
{
    public class PeriodicJsonGenerator : BackgroundService
    {
        private bool go = true;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(AppConstants.IntervaltenSeconds);     //For debug

                /* var nextTimeRun = DateTime.Now.TimeOfDay - new TimeSpan (AppConstants.ScheduleHourForJson,
                                                                          AppConstants.ScheduleMinuteForJson,
                                                                          AppConstants.ScheduleSecondForJson);
                 await Task.Delay (nextTimeRun);*/

                CSVTalker.ReadCSV();
                JsonTalker.WriteJson(DatabaseTalker.SelectTransactionsFromDatabase());
                Console.WriteLine("Génération d'un fichier JSON...");
                
            }
        }
        public void Stop()
        {
            go = false;
            Console.WriteLine("stop");
        }

        
    }
}
