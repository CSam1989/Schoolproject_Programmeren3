using System;

namespace BLL_Opdracht_PR.ModelDto
{
    public class UitgavenForCreateDto
    {
        public decimal Prijs { get; set; }
        public string Plaats { get; set; }
        public string Opmerking { get; set; }
        public DateTime UitgaveDatum { get; set; }
        public int GezinID { get; set; }
    }
}
