using System;

namespace BLL_Opdracht_PR.ModelDto
{
    public class UitgavenForWijzigDto
    {
        public int ID { get; set; }
        public decimal Prijs { get; set; }
        public string Plaats { get; set; }
        public string Opmerking { get; set; }
        public DateTime UitgaveDatum { get; set; }
        public bool IsVerekend { get; set; }
        public int GezinID { get; set; }
        public GezinForUitgaveLijstDto Gezin { get; set; }
    }
}
