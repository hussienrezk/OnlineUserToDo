namespace DataBaseEntity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdateUseTOdotable : DbMigration
    {
        public override void Up()
        {

            AddColumn("dbo.UserTodoList", "IsActive", c => c.Boolean());
            
        }
        
        public override void Down()
        {
           DropColumn("dbo.UserTodoList", "IsActive");
        }
    }
}
