namespace SAA2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration9 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Accounts", "EmailPassword");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "EmailPassword", c => c.String());
        }
    }
}
