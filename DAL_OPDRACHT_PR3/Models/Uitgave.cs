using System;

namespace DAL_OPDRACHT_PR3.Models
{
    /// <summary>
    ///     Ik maak gebruik van Fluent API om EF Conventions te overschrijven
    ///     Zoals Tabelnaam, required, maxlength
    ///     Fluent API is gedefinieerd in ProjectContext (map Resources)
    /// </summary>
    public class Uitgave
    {
        public int ID { get; set; }

        /*
            Kan niet controleren op enkel positieve bedragen via Fluent API.
            Via Data Annotation wordt dit ook niet in database opgeslagen [Range(0.0,9999)]
            Er gaat wel validatie komen in de view
        */
        public decimal Prijs { get; set; }
        public string Plaats { get; set; }
        public string Opmerking { get; set; }
        public DateTime UitgaveDatum { get; set; }
        public bool IsVerekend { get; set; } = false; //bij niet invullen => false
        public int GezinID { get; set; }
        public Gezin Gezin { get; set; }

        public Uitgave()
        {
            UitgaveDatum = DateTime.Now; //Bij niet invullen van deze property => huidige datum
        }
    }
}
