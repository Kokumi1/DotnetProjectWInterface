using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace FrontEnd.Models
{
    public class Operation
    {
        [Key]
        public int Id { get; set; }

        [XmlIgnore]
        public string CardNumber { get; set; }
        public string Type { get; set; }
        public double Amount { get; set; }
        public DateOnly Date { get; set; }
       // public string Devise { get; set; }
       // public double ExchangeRate { get; set; }
    }
}
