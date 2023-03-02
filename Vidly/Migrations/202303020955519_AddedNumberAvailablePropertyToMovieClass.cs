namespace Vidly.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedNumberAvailablePropertyToMovieClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "NumberAvailable", c => c.Int(nullable: false));

            Sql("UPDATE Movies " +
                "SET NumberAvailable = StockNumber");
        }

        public override void Down()
        {
            DropColumn("dbo.Movies", "NumberAvailable");
        }
    }
}
