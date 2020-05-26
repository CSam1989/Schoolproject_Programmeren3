namespace DAL_OPDRACHT_PR3.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdatingKortingTableLeeftijdVanTot : DbMigration
    {
        public override void Up()
        {
            AddColumn("PR3_Project.Kortingen", "LeeftijdVan", c => c.Int());
            AddColumn("PR3_Project.Kortingen", "LeeftijdTot", c => c.Int());
            AlterColumn("PR3_Project.Kortingen", "Leeftijd", c => c.Int());
        }

        public override void Down()
        {
            AlterColumn("PR3_Project.Kortingen", "Leeftijd", c => c.Int(nullable: false));
            DropColumn("PR3_Project.Kortingen", "LeeftijdTot");
            DropColumn("PR3_Project.Kortingen", "LeeftijdVan");
        }
    }
}
