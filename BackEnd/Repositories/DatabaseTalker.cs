using BackEnd.Datas;
using BackEnd.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public static class DatabaseTalker
    {

        public static List<Transaction> SelectTransactionsFromDatabase()
        {
            try
            {
                using (BackEndDbContext context = new BackEndDbContext())
                {
                    return [.. context.Transactions.AsNoTracking()];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return [];
            }
        }

        public static List<Anomaly> SelectAnomaliesFromDatabase()
        {
            try
            {
                using (BackEndDbContext context = new BackEndDbContext())
                {
                    return [.. context.Annomalies.AsNoTracking()];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return [];
            }
        }

        //Insert transaction list to the database by BackEndDbContext
        public static bool InsertTransactionListToDatabase(List<Transaction> transactionsList)
        {
            try
            {
                using (BackEndDbContext context = new BackEndDbContext())
                {
                    context.Transactions.AddRange(transactionsList);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        //Add a single Transaction to Database
        public static bool InsertTransactionToDatabase(Transaction transaction)
        {
            try
            {
                using (BackEndDbContext context = new BackEndDbContext())
                {
                    context.Transactions.Add(transaction);
                    context.SaveChanges();
                }
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false ;
            }
        }

        //Insert Anomalies list to the database by BackEndDbContext
        public static bool InsertAnomalyListToDatabase(List<Anomaly> AnomaliesList)
        {
            try
            {
                using (BackEndDbContext context = new BackEndDbContext())
                {
                    context.Annomalies.AddRange(AnomaliesList);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        //Insert a single anomaly in Database
        public static bool InsertAnomalyToDatabase(Anomaly transaction)
        {
            try
            {
                using (BackEndDbContext context = new BackEndDbContext())
                {
                    context.Annomalies.Add(transaction);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
