namespace BioMetrixCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stringdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Logs", "DateOnlyRecord", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Logs", "DateOnlyRecord", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
