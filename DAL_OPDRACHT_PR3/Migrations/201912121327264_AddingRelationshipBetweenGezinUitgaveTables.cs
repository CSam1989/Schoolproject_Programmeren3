namespace DAL_OPDRACHT_PR3.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddingRelationshipBetweenGezinUitgaveTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("PR3_Project.Uitgaven", "Prijs", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("PR3_Project.Uitgaven", "Plaats", c => c.String());
            AddColumn("PR3_Project.Uitgaven", "Opmerking", c => c.String());
            AddColumn("PR3_Project.Uitgaven", "UitgaveDatum", c => c.DateTime(nullable: false));
            AddColumn("PR3_Project.Uitgaven", "IsVerekend", c => c.Boolean(nullable: false));
            AddColumn("PR3_Project.Uitgaven", "GezinID", c => c.Int(nullable: false));
            CreateIndex("PR3_Project.Uitgaven", "GezinID");
            AddForeignKey("PR3_Project.Uitgaven", "GezinID", "PR3_Project.Gezinnen", "ID");
            DropColumn("PR3_Project.Uitgaven", "Voornaam");
            DropColumn("PR3_Project.Uitgaven", "Naam");
            DropColumn("PR3_Project.Uitgaven", "Geboortedatum");
        }

        public override void Down()
        {
            AddColumn("PR3_Project.Uitgaven", "Geboortedatum", c => c.DateTime(nullable: false));
            AddColumn("PR3_Project.Uitgaven", "Naam", c => c.String());
            AddColumn("PR3_Project.Uitgaven", "Voornaam", c => c.String());
            DropForeignKey("PR3_Project.Uitgaven", "GezinID", "PR3_Project.Gezinnen");
            DropIndex("PR3_Project.Uitgaven", new[] { "GezinID" });
            DropColumn("PR3_Project.Uitgaven", "GezinID");
            DropColumn("PR3_Project.Uitgaven", "IsVerekend");
            DropColumn("PR3_Project.Uitgaven", "UitgaveDatum");
            DropColumn("PR3_Project.Uitgaven", "Opmerking");
            DropColumn("PR3_Project.Uitgaven", "Plaats");
            DropColumn("PR3_Project.Uitgaven", "Prijs");
        }
    }
}
