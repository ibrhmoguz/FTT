namespace Gov.GTB.FirmaTalepTakip.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IrtibatPersonel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Firma", "TcNoIrtibatPersoneli", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Firma", "TcNoIrtibatPersoneli", c => c.Long(nullable: false));
        }
    }
}
