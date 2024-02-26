namespace MVC_1.Models
{
    public class Dipendente
    {
        public int IDDipendente { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Indirizzo { get; set; }
        public string CodiceFiscale { get; set; }
        public string Mansione { get; set; }
        public bool Coniugato { get; set; }
        public int NFigliACarico { get; set; }
    }
}
