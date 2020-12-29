namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterColumnUpdater : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CodeInfo", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.CodeInfo", "DeleteTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CodeInfo", "DeleteTime");
            DropColumn("dbo.CodeInfo", "Id");
        }
    }
}
