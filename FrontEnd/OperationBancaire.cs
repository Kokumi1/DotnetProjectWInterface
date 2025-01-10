using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd
{
    public class OperationBancaire
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; } // "Dépôt", "Retrait", "Paiement CB"
        public decimal Montant { get; set; }
        public string Description { get; set; }

        public override string ToString() => $"{Date:dd/MM/yyyy} - {Type}: {Montant:C} ({Description})";
    }
}

