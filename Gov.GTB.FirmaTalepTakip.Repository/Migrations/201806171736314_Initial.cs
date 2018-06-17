namespace Gov.GTB.FirmaTalepTakip.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CevapDetayGumruk",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TcNoIrtibatPersoneli = c.String(),
                        RefTalepCevapId = c.Int(nullable: false),
                        CevapAciklama = c.String(maxLength: 1000),
                        CevapTarih = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RefTalepCevap", t => t.RefTalepCevapId, cascadeDelete: true)
                .Index(t => t.RefTalepCevapId);
            
            CreateTable(
                "dbo.RefTalepCevap",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CKonu = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Kullanici",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TcNo = c.Long(nullable: false),
                        Adi = c.String(nullable: false, maxLength: 50),
                        Soyadi = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Durum = c.Boolean(nullable: false),
                        RolId = c.Int(nullable: false),
                        Sifre = c.String(maxLength: 50),
                        VergiNo = c.Long(),
                        Telefon = c.String(maxLength: 50),
                        BolgeKodu = c.String(maxLength: 50),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rol", t => t.RolId, cascadeDelete: true)
                .Index(t => t.RolId);
            
            CreateTable(
                "dbo.Rol",
                c => new
                    {
                        RolId = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 5),
                        Adi = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.RolId);
            
            CreateTable(
                "dbo.Firma",
                c => new
                    {
                        FirmaId = c.Int(nullable: false, identity: true),
                        VergiNo = c.Long(nullable: false),
                        TcNoIrtibatPersoneli = c.String(),
                        Adi = c.String(nullable: false, maxLength: 500),
                        BolgeKodu = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.FirmaId)
                .ForeignKey("dbo.GumrukKod", t => t.BolgeKodu, cascadeDelete: true)
                .Index(t => t.BolgeKodu);
            
            CreateTable(
                "dbo.GumrukKod",
                c => new
                    {
                        BolgeKodu = c.String(nullable: false, maxLength: 50),
                        Adi = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.BolgeKodu);
            
            CreateTable(
                "dbo.TalepDetayFirma",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TalepReferansNo = c.Long(nullable: false),
                        VergiNo = c.Long(nullable: false),
                        FirmaKullaniciId = c.Int(nullable: false),
                        KonuTalepAciklama = c.String(maxLength: 1000),
                        TalepTarih = c.DateTime(),
                        BolgeKodu = c.String(maxLength: 500),
                        CevapDurum = c.Boolean(nullable: false),
                        RefTalepKonuId = c.Int(nullable: false),
                        CevapDetayGumrukId = c.Int(),
                        FirmaKullanici_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CevapDetayGumruk", t => t.CevapDetayGumrukId)
                .ForeignKey("dbo.Kullanici", t => t.FirmaKullanici_Id)
                .ForeignKey("dbo.RefTalepKonu", t => t.RefTalepKonuId, cascadeDelete: true)
                .Index(t => t.RefTalepKonuId)
                .Index(t => t.CevapDetayGumrukId)
                .Index(t => t.FirmaKullanici_Id);
            
            CreateTable(
                "dbo.RefTalepKonu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TKonu = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TalepDetayFirmaLog",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TalepReferansNo = c.Long(nullable: false),
                        VergiNo = c.Long(nullable: false),
                        FirmaKullanici = c.String(),
                        KonuTalepBaslik = c.String(maxLength: 500),
                        KonuTalepAciklama = c.String(maxLength: 1000),
                        TalepTarih = c.DateTime(),
                        BolgeKodu = c.String(maxLength: 500),
                        CevapDurum = c.Boolean(nullable: false),
                        IslemTarih = c.DateTime(nullable: false),
                        CevapDetayGumrukId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CevapDetayGumruk", t => t.CevapDetayGumrukId)
                .Index(t => t.CevapDetayGumrukId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TalepDetayFirmaLog", "CevapDetayGumrukId", "dbo.CevapDetayGumruk");
            DropForeignKey("dbo.TalepDetayFirma", "RefTalepKonuId", "dbo.RefTalepKonu");
            DropForeignKey("dbo.TalepDetayFirma", "FirmaKullanici_Id", "dbo.Kullanici");
            DropForeignKey("dbo.TalepDetayFirma", "CevapDetayGumrukId", "dbo.CevapDetayGumruk");
            DropForeignKey("dbo.Kullanici", "RolId", "dbo.Rol");
            DropForeignKey("dbo.Firma", "BolgeKodu", "dbo.GumrukKod");
            DropForeignKey("dbo.CevapDetayGumruk", "RefTalepCevapId", "dbo.RefTalepCevap");
            DropIndex("dbo.TalepDetayFirmaLog", new[] { "CevapDetayGumrukId" });
            DropIndex("dbo.TalepDetayFirma", new[] { "FirmaKullanici_Id" });
            DropIndex("dbo.TalepDetayFirma", new[] { "CevapDetayGumrukId" });
            DropIndex("dbo.TalepDetayFirma", new[] { "RefTalepKonuId" });
            DropIndex("dbo.Firma", new[] { "BolgeKodu" });
            DropIndex("dbo.Kullanici", new[] { "RolId" });
            DropIndex("dbo.CevapDetayGumruk", new[] { "RefTalepCevapId" });
            DropTable("dbo.TalepDetayFirmaLog");
            DropTable("dbo.RefTalepKonu");
            DropTable("dbo.TalepDetayFirma");
            DropTable("dbo.GumrukKod");
            DropTable("dbo.Firma");
            DropTable("dbo.Rol");
            DropTable("dbo.Kullanici");
            DropTable("dbo.RefTalepCevap");
            DropTable("dbo.CevapDetayGumruk");
        }
    }
}
