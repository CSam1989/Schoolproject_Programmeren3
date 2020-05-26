using BLL_Opdracht_PR.Helpers;
using DAL_OPDRACHT_PR3.Models;
using System.Data.Entity;
using System.Linq;

namespace DAL_OPDRACHT_PR3.Resources
{
    public class KortingRepo
    {
        #region Constructor
        private readonly ProjectContext _context;

        public KortingRepo(ProjectContext context)
        {
            this._context = context;
        }
        #endregion

        #region cRud methodes (enkel read)
        /// <summary>
        ///     Ik heb hier geopteerd voor al de kortingcoefficienten te berekenen in de repository
        ///     Omdat de kortingstabel voor niets anders kan/moet dienen
        /// </summary>
        public double GetTotaleKortingCoefficient()
        {
            double totaleKortingsCoefficient = 0;
            var listGezinnen = _context.Gezinnen
                .Include(g => g.Personen)
                .ToList();

            foreach (var gezin in listGezinnen)
            {
                totaleKortingsCoefficient += GetKortingsCoefficient(gezin);
            }

            return totaleKortingsCoefficient;
        }

        public double GetKortingCoeffiecentPerGezin(int gezinsID)
        {
            var gezin = _context.Gezinnen
                .Include(g => g.Personen)
                .FirstOrDefault(g => g.ID == gezinsID);

            return GetKortingsCoefficient(gezin);

        }
        #endregion

        #region private methode => coefficient per gezin
        private double GetKortingsCoefficient(Gezin gezin)
        {
            double kortingsCoefficient = 0;

            foreach (var persoon in gezin.Personen)
            {
                int leeftijd = persoon.Geboortedatum.Age();
                var lijstKortingen = _context.Kortingen.OrderBy(k => k.LeeftijdVan).ToList();

                var kortingCoefficientPP = lijstKortingen.FirstOrDefault(k => leeftijd >= k.LeeftijdVan && leeftijd <= k.LeeftijdTot).Coefficient;

                kortingsCoefficient += kortingCoefficientPP;
            }

            return kortingsCoefficient;

        }
        #endregion
    }
}
