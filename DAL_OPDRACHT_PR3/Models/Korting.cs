namespace DAL_OPDRACHT_PR3.Models
{
    /// <summary>
    ///     Ik maak gebruik van Fluent API om EF Conventions te overschrijven
    ///     Zoals Tabelnaam, required, maxlength
    ///     Fluent API is gedefinieerd in ProjectContext (map Resources)
    /// </summary>
    public class Korting
    {
        public int ID { get; set; }

        /*
            Via Fluent API kan er niet gecontroleerd worden of iets tussen bepaalde waarde ligt
            Via Data Annotation wordt dit ook niet in database opgeslagen [Range(0,99)]
            Er gaat wel validatie komen in de view
        */
        public int LeeftijdVan { get; set; }
        public int LeeftijdTot { get; set; }
        public double Coefficient { get; set; }
    }
}
