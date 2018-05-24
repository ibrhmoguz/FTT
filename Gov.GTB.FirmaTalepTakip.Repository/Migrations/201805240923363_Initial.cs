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
                        CevapBaslik = c.String(maxLength: 500),
                        CevapAciklama = c.String(maxLength: 1000),
                        CevapTarih = c.DateTime(),
                        TalepReferansNumarasi = c.String(maxLength: 100),
                        TalepReferansNo_TalepReferansNo = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TalepDetayFirma", t => t.TalepReferansNo_TalepReferansNo)
                .Index(t => t.TalepReferansNo_TalepReferansNo);
            
            CreateTable(
                "dbo.TalepDetayFirma",
                c => new
                    {
                        TalepReferansNo = c.String(nullable: false, maxLength: 100),
                        VergiNo = c.Long(nullable: false),
                        TcNoFirmaKullanici = c.String(),
                        KonuTalepBaslik = c.String(maxLength: 500),
                        KonuTalepAciklama = c.String(maxLength: 1000),
                        TalepTarih = c.DateTime(),
                        BolgeKodu = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.TalepReferansNo);
            
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
                "dbo.RefTalepKonu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TKonu = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kullanici", "RolId", "dbo.Rol");
            DropForeignKey("dbo.Firma", "BolgeKodu", "dbo.GumrukKod");
            DropForeignKey("dbo.CevapDetayGumruk", "TalepReferansNo_TalepReferansNo", "dbo.TalepDetayFirma");
            DropIndex("dbo.Firma", new[] { "BolgeKodu" });
            DropIndex("dbo.Kullanici", new[] { "RolId" });
            DropIndex("dbo.CevapDetayGumruk", new[] { "TalepReferansNo_TalepReferansNo" });
            DropTable("dbo.RefTalepKonu");
            DropTable("dbo.GumrukKod");
            DropTable("dbo.Firma");
            DropTable("dbo.Rol");
            DropTable("dbo.Kullanici");
            DropTable("dbo.RefTalepCevap");
            DropTable("dbo.TalepDetayFirma");
            DropTable("dbo.CevapDetayGumruk");
        }
    }
}
