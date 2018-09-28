using System.Data.Entity.Migrations.Model;

namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDataToBirthDate : DbMigration
    {
        public override void Up()
        {
            Sql(@"UPDATE Customers SET BirthDate='1990.12.09' WHERE Id=1");
        }
        
        public override void Down()
        {
        }
    }
}
