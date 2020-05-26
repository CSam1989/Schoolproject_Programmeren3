using System.Collections.Generic;

namespace BLL_Opdracht_PR.ModelDto
{
    public class GezinForUitgebreidLijstDto
    {
        public int ID { get; set; }
        public string Gezinsnaam { get; set; }
        public string Straat { get; set; }
        public string Huisnummer { get; set; }
        public int? GemeenteID { get; set; }
        public GemeenteForGezinLijstDto Gemeente { get; set; }
        public ICollection<PersonenForGezinLijstDto> Personen { get; set; }
        public double Korting { get; set; }
    }
}
