using System;
using System.Collections.Generic;

namespace MVC_1.Models
{
    public class CreatePagamentiWithSelect
    {
        public int IDPagamento { get; set; }
        public int IDDipendente { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal Ammontare { get; set; }
        public string Tipo { get; set; }
        public List<Dipendente> Dipendenti { get; set; }
    }
}
