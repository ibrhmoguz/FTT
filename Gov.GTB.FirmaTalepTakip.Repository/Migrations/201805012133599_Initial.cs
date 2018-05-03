namespace Gov.GTB.FirmaTalepTakip.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kullanici",
                c => new
                    {
                        TCNo = c.String(nullable: false, maxLength: 11),
                        Adi = c.String(nullable: false, maxLength: 50),
                        Soyadi = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Durum = c.Boolean(nullable: false),
                        RolId = c.Int(nullable: false),
                        Sifre = c.String(maxLength: 50),
                        VergiNo = c.String(maxLength: 50),
                        Telefon = c.String(maxLength: 50),
                        BolgeKodu = c.String(maxLength: 50),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.TCNo)
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
                        VergiNo = c.Int(nullable: false),
                        TCNo = c.Int(nullable: false),
                        Adi = c.String(nullable: false, maxLength: 11),
                        BolgeKodu = c.String(nullable: false, maxLength: 11),
                        IrtibatPersoneli_TCNo = c.String(maxLength: 11),
                    })
                .PrimaryKey(t => t.FirmaId)
                .ForeignKey("dbo.Kullanici", t => t.IrtibatPersoneli_TCNo)
                .Index(t => t.IrtibatPersoneli_TCNo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kullanici", "RolId", "dbo.Rol");
            DropForeignKey("dbo.Firma", "IrtibatPersoneli_TCNo", "dbo.Kullanici");
            DropIndex("dbo.Firma", new[] { "IrtibatPersoneli_TCNo" });
            DropIndex("dbo.Kullanici", new[] { "RolId" });
            DropTable("dbo.Firma");
            DropTable("dbo.Rol");
            DropTable("dbo.Kullanici");
        }
    }
}
