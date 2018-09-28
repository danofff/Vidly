namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDataToGenreTable : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT into Genres (Name) VALUES('Comedy')");
            Sql(@"INSERT into Genres (Name) VALUES('Action')");
            Sql(@"INSERT into Genres (Name) VALUES('Family')");
            Sql(@"INSERT into Genres (Name) VALUES('Romance')");
        }
        
        public override void Down()
        {
        }
    }
}
