namespace Gov.GTB.FirmaTalepTakip.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GumrukKod : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GumrukKod",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BolgeKodu = c.String(nullable: false, maxLength: 50),
                        Adi = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GumrukKod");
        }
    }
}
