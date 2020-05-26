using System.Collections.Generic;

namespace DAL_OPDRACHT_PR3.Models
{
    /// <summary>
    ///     Ik maak gebruik van Fluent API om EF Conventions te overschrijven
    ///     Zoals Tabelnaam, required, maxlength
    ///     Fluent API is gedefinieerd in ProjectContext (map Resources)
    /// </summary>
    public class Gemeente
    {
        public int ID { get; set; }
        public string Postcode { get; set; }
        public string GemeenteNaam { get; set; }
        public ICollection<Gezin> Gezinnen { get; set; }
    }
}
