using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Gov.GTB.FirmaTalepTakip.Model.Entities;

namespace Gov.GTB.FirmaTalepTakip.Repository.DataContext
{
    public class FirmaDbContext : DbContext
    {
        public FirmaDbContext()
            : base("FirmaDbConnectionString")
        {
        }

        public virtual DbSet<Rol> Roller { get; set; }
        public virtual DbSet<Kullanici> Kullanici { get; set; }
        public virtual DbSet<GumrukKullanici> GumrukKullanicilar { get; set; }
        public virtual DbSet<FirmaKullanici> FirmaKullanicilar { get; set; }
        public virtual DbSet<Firma> Firmalar { get; set; }
        public virtual DbSet<GumrukKod> GumrukKodlari { get; set; }
        public virtual DbSet<RefTalepCevap> CevapKonulari { get; set; }
        public virtual DbSet<RefTalepKonu> TalepKonulari { get; set; }
        public virtual DbSet<CevapDetayGumruk> CevapDetayi { get; set; }
        public virtual DbSet<TalepDetayFirma> TalepDetayi { get; set; }
        public virtual DbSet<TalepDetayFirmaLog> TalepDetayiLog { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
