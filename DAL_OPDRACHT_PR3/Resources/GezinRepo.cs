using DAL_OPDRACHT_PR3.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL_OPDRACHT_PR3.Resources
{
    public class GezinRepo
    {
        #region Constructor
        private readonly ProjectContext _context;

        public GezinRepo(ProjectContext context)
        {
            this._context = context;
        }
        #endregion

        #region CRUD methodes (enkel read)
        public ICollection<Gezin> GetAllGezinnen()
        {
            return _context.Gezinnen.ToList();
        }

        public ICollection<Gezin> GetAllGezinnenUitgebreid()
        {
            return _context.Gezinnen
                        .Include(g => g.Gemeente)
                        .Include(g => g.Personen)
                        .ToList();
        }
        #endregion
    }
}
