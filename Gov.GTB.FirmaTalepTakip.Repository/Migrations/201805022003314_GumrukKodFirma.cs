namespace Gov.GTB.FirmaTalepTakip.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GumrukKodFirma : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.GumrukKod");
            AlterColumn("dbo.Firma", "BolgeKodu", c => c.String(nullable: false, maxLength: 50));
            AddPrimaryKey("dbo.GumrukKod", "BolgeKodu");
            CreateIndex("dbo.Firma", "BolgeKodu");
            AddForeignKey("dbo.Firma", "BolgeKodu", "dbo.GumrukKod", "BolgeKodu", cascadeDelete: true);
            DropColumn("dbo.GumrukKod", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GumrukKod", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Firma", "BolgeKodu", "dbo.GumrukKod");
            DropIndex("dbo.Firma", new[] { "BolgeKodu" });
            DropPrimaryKey("dbo.GumrukKod");
            AlterColumn("dbo.Firma", "BolgeKodu", c => c.String(nullable: false, maxLength: 11));
            AddPrimaryKey("dbo.GumrukKod", "Id");
        }
    }
}
