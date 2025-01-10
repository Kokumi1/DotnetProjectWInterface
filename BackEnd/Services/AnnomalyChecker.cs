using BackEnd.Entities;

namespace BackEnd.Services
{
    public static class AnnomalyChecker
    {
        //Check if a transaction is normal
        public static bool CheckAnomaly(Transaction transaction)
        {
            return new CreditCard(transaction.CardNumber).LuhnCheck();
        }

        /// <summary>
        /// Check if all transactions in the list is normal
        /// </summary>
        /// <param name="transactionList"></param>
        /// <returns>
        /// 0 = list of normal transaction
        /// 1 = list of anormal transaction
        /// </returns>
        public static List<Transaction>[] CheckAnnomaly(List<Transaction> transactionList)
        {
            List<Transaction> normalTransaction = new List<Transaction>();
            List<Transaction> AnormalTransaction = new List<Transaction>();

            foreach (Transaction transaction in transactionList)
            {
                if (new CreditCard(transaction.CardNumber).LuhnCheck())
                {
                    normalTransaction.Add(transaction);
                }
                else
                {
                    AnormalTransaction.Add(transaction);
                }
            }
            return [normalTransaction, AnormalTransaction];
        }
    }
}
