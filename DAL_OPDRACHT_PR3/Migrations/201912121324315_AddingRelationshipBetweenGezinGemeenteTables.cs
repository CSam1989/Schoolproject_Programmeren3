namespace DAL_OPDRACHT_PR3.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddingRelationshipBetweenGezinGemeenteTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("PR3_Project.Gezinnen", "GemeenteID", c => c.Int());
            CreateIndex("PR3_Project.Gezinnen", "GemeenteID");
            AddForeignKey("PR3_Project.Gezinnen", "GemeenteID", "PR3_Project.Gemeentes", "ID");
        }

        public override void Down()
        {
            DropForeignKey("PR3_Project.Gezinnen", "GemeenteID", "PR3_Project.Gemeentes");
            DropIndex("PR3_Project.Gezinnen", new[] { "GemeenteID" });
            DropColumn("PR3_Project.Gezinnen", "GemeenteID");
        }
    }
}
