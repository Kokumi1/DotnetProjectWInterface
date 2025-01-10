using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Models
{
    public class IncomingTransactionEntity
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public int Type { get; set; }
        public double Amount { get; set; }
        public string Date { get; set; }
        public string Currency { get; set; }  // Renommé "Devise" en "Currency"
        public double ExchangeRate { get; set; }
    }
}
