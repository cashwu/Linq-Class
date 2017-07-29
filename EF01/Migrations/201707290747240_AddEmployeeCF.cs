namespace EF01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmployeeCF : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeCFs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EmployeeCFs");
        }
    }
}
