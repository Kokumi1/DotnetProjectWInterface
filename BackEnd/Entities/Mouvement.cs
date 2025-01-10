using System.ComponentModel.DataAnnotations;

namespace BackEnd.Entities
{
    public abstract class Mouvement
    {
        [Key]
        public int Id { get; set; }
        public enum TransactionType { counterDeposite, creditCardInvoice, ATMWithdrawals }
        public string CardNumber { get; set; }
        public TransactionType Type { get; set; }
        public double Amount { get; set; }
        public DateOnly Date { get; set; }
        public string Devise { get; set; }
        public double ExchangeRate { get; set; }
    }
}
