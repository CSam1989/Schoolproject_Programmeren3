namespace DAL_OPDRACHT_PR3.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddingFluentAPIToTables : DbMigration
    {
        public override void Up()
        {
            AlterColumn("PR3_Project.Gemeentes", "Postcode", c => c.String(nullable: false, maxLength: 4));
            AlterColumn("PR3_Project.Gemeentes", "GemeenteNaam", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("PR3_Project.Gezinnen", "Gezinsnaam", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("PR3_Project.Gezinnen", "Straat", c => c.String(maxLength: 255));
            AlterColumn("PR3_Project.Gezinnen", "Huisnummer", c => c.String(maxLength: 10));
            AlterColumn("PR3_Project.Personen", "Voornaam", c => c.String(maxLength: 50));
            AlterColumn("PR3_Project.Personen", "Naam", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("PR3_Project.Uitgaven", "Plaats", c => c.String(maxLength: 50));
            AlterColumn("PR3_Project.Uitgaven", "Opmerking", c => c.String(maxLength: 255));
        }

        public override void Down()
        {
            AlterColumn("PR3_Project.Uitgaven", "Opmerking", c => c.String());
            AlterColumn("PR3_Project.Uitgaven", "Plaats", c => c.String());
            AlterColumn("PR3_Project.Personen", "Naam", c => c.String());
            AlterColumn("PR3_Project.Personen", "Voornaam", c => c.String());
            AlterColumn("PR3_Project.Gezinnen", "Huisnummer", c => c.String());
            AlterColumn("PR3_Project.Gezinnen", "Straat", c => c.String());
            AlterColumn("PR3_Project.Gezinnen", "Gezinsnaam", c => c.String());
            AlterColumn("PR3_Project.Gemeentes", "GemeenteNaam", c => c.String());
            AlterColumn("PR3_Project.Gemeentes", "Postcode", c => c.String());
        }
    }
}
