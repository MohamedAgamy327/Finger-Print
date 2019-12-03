namespace BioMetrixCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MachineNumber = c.Int(nullable: false),
                        IndRegID = c.Int(nullable: false),
                        DateTimeRecord = c.DateTime(nullable: false),
                        DateOnlyRecord = c.DateTime(nullable: false, storeType: "date"),
                        TimeOnlyRecord = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Logs");
        }
    }
}
