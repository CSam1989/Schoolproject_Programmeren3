namespace DAL_OPDRACHT_PR3.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddingRelationshipBetweenGezinPersoonTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("PR3_Project.Personen", "GezinID", c => c.Int(nullable: false));
            CreateIndex("PR3_Project.Personen", "GezinID");
            AddForeignKey("PR3_Project.Personen", "GezinID", "PR3_Project.Gezinnen", "ID");
        }

        public override void Down()
        {
            DropForeignKey("PR3_Project.Personen", "GezinID", "PR3_Project.Gezinnen");
            DropIndex("PR3_Project.Personen", new[] { "GezinID" });
            DropColumn("PR3_Project.Personen", "GezinID");
        }
    }
}
