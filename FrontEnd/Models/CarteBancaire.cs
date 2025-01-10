using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Models
{
    public class CarteBancaire
    {
        public int Id { get; set; }
        public string NumeroCarte { get; set; }

        // Clé étrangère vers CompteBancaire
        public int CompteBancaireId { get; set; }
        public CompteBancaire CompteBancaire { get; set; }

        public override string ToString() => $"Carte: {NumeroCarte}";
    }
}
