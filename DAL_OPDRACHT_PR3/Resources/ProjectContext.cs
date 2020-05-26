using DAL_OPDRACHT_PR3.Models;
using System.Data.Entity;

namespace DAL_OPDRACHT_PR3
{
    public partial class ProjectContext : DbContext
    {
        public ProjectContext()
            : base("name=ProjectContext")
        {
        }
        public DbSet<Gemeente> Gemeentes { get; set; }
        public DbSet<Korting> Kortingen { get; set; }
        public DbSet<Persoon> Personen { get; set; }
        public DbSet<Gezin> Gezinnen { get; set; }
        public DbSet<Uitgave> Uitgaven { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*
               Fluent API (geen data annotations)
               Om de conventies van EF te overschrijven
           */
            #region Gemeente
            modelBuilder.Entity<Gemeente>().ToTable("Gemeentes", "PR3_Project");

            modelBuilder.Entity<Gemeente>()
                .Property(g => g.Postcode)
                .IsRequired()
                .HasMaxLength(4);

            modelBuilder.Entity<Gemeente>()
                .Property(g => g.GemeenteNaam)
                .IsRequired()
                .HasMaxLength(50);
            #endregion

            #region Korting
            modelBuilder.Entity<Korting>().ToTable("Kortingen", "PR3_Project");
            #endregion

            #region Persoon
            modelBuilder.Entity<Persoon>().ToTable("Personen", "PR3_Project");

            modelBuilder.Entity<Persoon>()
                .Property(p => p.Naam)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Persoon>()
                .Property(p => p.Voornaam)
                .HasMaxLength(50);

            modelBuilder.Entity<Persoon>()
                .Property(p => p.Geboortedatum)
                .IsRequired();

            modelBuilder.Entity<Persoon>()
                .HasRequired<Gezin>(p => p.Gezin)
                .WithMany(g => g.Personen)
                .HasForeignKey(p => p.GezinID)
                .WillCascadeOnDelete(false);

            #endregion

            #region Gezin
            modelBuilder.Entity<Gezin>().ToTable("Gezinnen", "PR3_Project");

            modelBuilder.Entity<Gezin>()
                .Property(g => g.Gezinsnaam)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Gezin>()
                .Property(g => g.Straat)
                .HasMaxLength(255);

            modelBuilder.Entity<Gezin>()
                .Property(g => g.Huisnummer)
                .HasMaxLength(10);

            modelBuilder.Entity<Gezin>()
                .HasOptional<Gemeente>(g => g.Gemeente)
                .WithMany(g => g.Gezinnen)
                .HasForeignKey(g => g.GemeenteID)
                .WillCascadeOnDelete(false);


            #endregion

            #region Uitgave
            modelBuilder.Entity<Uitgave>().ToTable("Uitgaven", "PR3_Project");

            modelBuilder.Entity<Uitgave>()
                .Property(u => u.Plaats)
                .HasMaxLength(50);

            modelBuilder.Entity<Uitgave>()
                .Property(u => u.Opmerking)
                .HasMaxLength(255);

            modelBuilder.Entity<Uitgave>()
                .HasRequired<Gezin>(u => u.Gezin)
                .WithMany(g => g.Uitgaven)
                .HasForeignKey(u => u.GezinID)
                .WillCascadeOnDelete(false);
            #endregion
        }
    }
}
