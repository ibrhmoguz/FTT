namespace Gov.GTB.FirmaTalepTakip.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TalepFirmaDetay : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CevapDetayGumruk", "TalepReferansNo_TalepReferansNo", "dbo.TalepDetayFirma");
            DropIndex("dbo.CevapDetayGumruk", new[] { "TalepReferansNo_TalepReferansNo" });
            RenameColumn(table: "dbo.CevapDetayGumruk", name: "TalepReferansNo_TalepReferansNo", newName: "TalepReferansNo_Id");
            DropPrimaryKey("dbo.TalepDetayFirma");
            CreateTable(
                "dbo.TalepDetayFirmaLog",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TalepReferansNo = c.String(maxLength: 100),
                        VergiNo = c.Long(nullable: false),
                        TcNoFirmaKullanici = c.Long(nullable: false),
                        KonuTalepBaslik = c.String(maxLength: 500),
                        KonuTalepAciklama = c.String(maxLength: 1000),
                        TalepTarih = c.DateTime(),
                        BolgeKodu = c.String(maxLength: 500),
                        CevapDurum = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.TalepDetayFirma", "Id", c => c.Long(nullable: false, identity: true));
            AddColumn("dbo.TalepDetayFirma", "CevapDurum", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CevapDetayGumruk", "TalepReferansNo_Id", c => c.Long());
            AlterColumn("dbo.TalepDetayFirma", "TalepReferansNo", c => c.String(maxLength: 100));
            AlterColumn("dbo.TalepDetayFirma", "TcNoFirmaKullanici", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.TalepDetayFirma", "Id");
            CreateIndex("dbo.CevapDetayGumruk", "TalepReferansNo_Id");
            AddForeignKey("dbo.CevapDetayGumruk", "TalepReferansNo_Id", "dbo.TalepDetayFirma", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CevapDetayGumruk", "TalepReferansNo_Id", "dbo.TalepDetayFirma");
            DropIndex("dbo.CevapDetayGumruk", new[] { "TalepReferansNo_Id" });
            DropPrimaryKey("dbo.TalepDetayFirma");
            AlterColumn("dbo.TalepDetayFirma", "TcNoFirmaKullanici", c => c.String());
            AlterColumn("dbo.TalepDetayFirma", "TalepReferansNo", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.CevapDetayGumruk", "TalepReferansNo_Id", c => c.String(maxLength: 100));
            DropColumn("dbo.TalepDetayFirma", "CevapDurum");
            DropColumn("dbo.TalepDetayFirma", "Id");
            DropTable("dbo.TalepDetayFirmaLog");
            AddPrimaryKey("dbo.TalepDetayFirma", "TalepReferansNo");
            RenameColumn(table: "dbo.CevapDetayGumruk", name: "TalepReferansNo_Id", newName: "TalepReferansNo_TalepReferansNo");
            CreateIndex("dbo.CevapDetayGumruk", "TalepReferansNo_TalepReferansNo");
            AddForeignKey("dbo.CevapDetayGumruk", "TalepReferansNo_TalepReferansNo", "dbo.TalepDetayFirma", "TalepReferansNo");
        }
    }
}
