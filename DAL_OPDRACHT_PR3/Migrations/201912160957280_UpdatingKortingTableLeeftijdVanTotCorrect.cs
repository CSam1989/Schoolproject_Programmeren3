namespace DAL_OPDRACHT_PR3.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdatingKortingTableLeeftijdVanTotCorrect : DbMigration
    {
        public override void Up()
        {
            AlterColumn("PR3_Project.Kortingen", "LeeftijdVan", c => c.Int(nullable: false));
            AlterColumn("PR3_Project.Kortingen", "LeeftijdTot", c => c.Int(nullable: false));
            DropColumn("PR3_Project.Kortingen", "Leeftijd");
        }

        public override void Down()
        {
            AddColumn("PR3_Project.Kortingen", "Leeftijd", c => c.Int());
            AlterColumn("PR3_Project.Kortingen", "LeeftijdTot", c => c.Int());
            AlterColumn("PR3_Project.Kortingen", "LeeftijdVan", c => c.Int());
        }
    }
}
