namespace DAL_OPDRACHT_PR3.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCommit_GeneratingAllBasicTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "PR3_Project.Gemeentes",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Postcode = c.String(),
                    GemeenteNaam = c.String(),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "PR3_Project.Kortingen",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Leeftijd = c.Int(nullable: false),
                    Coefficient = c.Double(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "PR3_Project.Personen",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Voornaam = c.String(),
                    Naam = c.String(),
                    Geboortedatum = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "PR3_Project.Gezinnen",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Gezinsnaam = c.String(),
                    Straat = c.String(),
                    Huisnummer = c.String(),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "PR3_Project.Uitgaven",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Voornaam = c.String(),
                    Naam = c.String(),
                    Geboortedatum = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.ID);

        }

        public override void Down()
        {
            DropTable("PR3_Project.Uitgaven");
            DropTable("PR3_Project.Gezinnen");
            DropTable("PR3_Project.Personen");
            DropTable("PR3_Project.Kortingen");
            DropTable("PR3_Project.Gemeentes");
        }
    }
}
