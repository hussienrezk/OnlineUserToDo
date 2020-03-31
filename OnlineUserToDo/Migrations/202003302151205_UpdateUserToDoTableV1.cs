namespace OnlineUserToDo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserToDoTableV1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserToDo", "IsActive", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserToDo", "IsActive");
        }
    }
}
