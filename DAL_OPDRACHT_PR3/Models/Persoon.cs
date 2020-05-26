using System;

namespace DAL_OPDRACHT_PR3.Models
{
    /// <summary>
    ///     Ik maak gebruik van Fluent API om EF Conventions te overschrijven
    ///     Zoals Tabelnaam, required, maxlength
    ///     Fluent API is gedefinieerd in ProjectContext (map Resources)
    /// </summary>
    public class Persoon
    {
        public int ID { get; set; }
        public string Voornaam { get; set; }
        public string Naam { get; set; }
        public DateTime Geboortedatum { get; set; }
        public int GezinID { get; set; }
        public Gezin Gezin { get; set; }
    }
}
