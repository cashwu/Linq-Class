using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF01.Migrations
{
    public partial class AddTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Areas",
                    c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
        }

        public override void Down()
        {
            DropTable("dbo.Areas");
        }
    }
}
