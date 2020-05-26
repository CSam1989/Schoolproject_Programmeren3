using System.Collections.Generic;

namespace DAL_OPDRACHT_PR3.Models
{
    /// <summary>
    ///     Ik maak gebruik van Fluent API om EF Conventions te overschrijven
    ///     Zoals Tabelnaam, required, maxlength
    ///     Fluent API is gedefinieerd in ProjectContext (map Resources)
    /// </summary>
    public class Gezin
    {
        public int ID { get; set; }
        public string Gezinsnaam { get; set; }
        public string Straat { get; set; }
        public string Huisnummer { get; set; }
        public int? GemeenteID { get; set; }
        public Gemeente Gemeente { get; set; }
        public ICollection<Uitgave> Uitgaven { get; set; }
        public ICollection<Persoon> Personen { get; set; }

    }
}
