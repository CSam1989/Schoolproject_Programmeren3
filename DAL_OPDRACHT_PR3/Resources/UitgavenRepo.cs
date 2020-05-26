using DAL_OPDRACHT_PR3.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace DAL_OPDRACHT_PR3.Resources
{
    public class UitgavenRepo
    {
        /// <summary>
        ///     Mocht er nog tijd overzijn, eventueel dit generic maken
        /// </summary>
        #region Constructor
        private readonly ProjectContext _context;

        public UitgavenRepo(ProjectContext context)
        {
            this._context = context;
        }
        #endregion

        #region CRUD methodes
        public ICollection<Uitgave> GetAllUitgaven()
        {
            return _context.Uitgaven.Include(u => u.Gezin).ToList();
        }

        public ICollection<Uitgave> GetFilteredUitgaven(string filter)
        {
            if (string.IsNullOrEmpty(filter))
                return GetAllUitgaven();

            return _context.Uitgaven.Include(u => u.Gezin)
                .Where(u => u.ID.ToString().Contains(filter) ||
                            u.Gezin.Gezinsnaam.Contains(filter) ||
                            u.Plaats.Contains(filter) ||
                            u.Prijs.ToString().Contains(filter))
                .ToList();
        }

        public ICollection<Uitgave> GetUitgavenByIsVerekend(bool isVerekend)
        {
            return _context.Uitgaven.Include(u => u.Gezin).Where(u => u.IsVerekend == isVerekend).ToList();
        }

        public ICollection<Uitgave> GetUItgavenByIsVerekendAndByGezin(bool isVerekend, int gezinsID)
        {
            return _context.Uitgaven.Where(u => u.IsVerekend == isVerekend && u.GezinID == gezinsID).ToList();
        }

        public Uitgave GetUitgaveByID(int id)
        {
            return _context.Uitgaven.Include(u => u.Gezin).FirstOrDefault(u => u.ID == id);
        }

        public void Insert(Uitgave uitgave)
        {
            _context.Entry(uitgave).State = EntityState.Added;
        }

        public void Update(Uitgave uitgave)
        {
            /// <summary>
            ///     Bij Entry.State = EntityState.Modified krijg ik zelfde foutmelding als Delete methode
            ///     Ik denk dat dit iets te maken heeft met mijn Dto's enkel een FK ID (en geen nav prop) hebben
            ///     Door AutoMapper te gebruiken is die nav property NULL
            ///     Daar gaat denk ik de fout zitten, dat entity.state de nav property naar null wilt overzetten (en dat gaat niet)
            /// </summary>
            _context.Set<Uitgave>().AddOrUpdate(uitgave);
        }

        public void Delete(int uitgaveID)
        {
            /// <summary>
            ///     Ik werk hier met een "Disconnected State" (niet met Entry.State)
            ///     Entry.Delete kan geen Child object deleten
            ///     Stack Overflow => Remove will also remove the child objects, but using Deleted will not. You should really be using Remove for this very reason.
            ///     Bij onderstaande werkwijze kan ik de gewenste uitgaven zo verwijderen
            /// </summary>
            Uitgave uitgave = _context.Uitgaven.FirstOrDefault(u => u.ID == uitgaveID);

            _context.Uitgaven.Remove(uitgave);
        }
        #endregion
    }
}
