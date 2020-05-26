namespace DAL_OPDRACHT_PR3.Resources
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Attributen
        private UitgavenRepo _uitgavenRepo;
        private GezinRepo _gezinRepo;
        private KortingRepo _kortingRepo;
        #endregion

        #region Constructor
        private readonly ProjectContext _context;

        public UnitOfWork()
        {
            this._context = new ProjectContext();
        }
        #endregion

        #region Properties
        public UitgavenRepo UitgavenRepo
        {
            get
            {
                if (_uitgavenRepo is null)
                    _uitgavenRepo = new UitgavenRepo(_context);
                return _uitgavenRepo;
            }
        }

        public GezinRepo GezinRepo
        {
            get
            {
                if (_gezinRepo is null)
                    _gezinRepo = new GezinRepo(_context);
                return _gezinRepo;
            }
        }

        public KortingRepo KortingRepo
        {
            get
            {
                if (_kortingRepo is null)
                    _kortingRepo = new KortingRepo(_context);
                return _kortingRepo;
            }
        }

        #endregion

        #region Methodes
        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
        #endregion
    }
}
