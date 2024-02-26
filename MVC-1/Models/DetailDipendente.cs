using System;

namespace MVC_1.Models
{
    public class DetailDipendente
    {
        public int IDDipendente { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal Ammontare { get; set; }
        public string Tipo { get; set; }
    }
}
