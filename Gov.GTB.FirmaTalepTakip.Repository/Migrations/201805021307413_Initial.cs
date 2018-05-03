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
                        Id = c.Long(nullable: false, identity: true),
                        TCNo = c.Long(nullable: false),
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
                        TcNoIrtibatPersoneli = c.Long(nullable: false),
                        Adi = c.String(nullable: false, maxLength: 500),
                        BolgeKodu = c.String(nullable: false, maxLength: 11),
                    })
                .PrimaryKey(t => t.FirmaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kullanici", "RolId", "dbo.Rol");
            DropIndex("dbo.Kullanici", new[] { "RolId" });
            DropTable("dbo.Firma");
            DropTable("dbo.Rol");
            DropTable("dbo.Kullanici");
        }
    }
}
