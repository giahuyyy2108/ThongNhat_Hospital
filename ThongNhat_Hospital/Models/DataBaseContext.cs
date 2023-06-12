using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using ThongNhat_Hospital.Models.ViewModel;

namespace ThongNhat_Hospital.Models
{
    public class DataBaseContext : IdentityDbContext<User>
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            foreach(var entityType in modelbuilder.Model.GetEntityTypes())
            {
                var tablbename = entityType.GetTableName();
                if (tablbename.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tablbename.Substring(6));
                }

            }

            modelbuilder.Entity<ThongkeViewModel>(entity => entity.HasNoKey().ToView(null));
        }

        public DbSet<User> user { get; set; }

        public DbSet<PhieuGiaoHang> PhieuGiaoHang { set; get; }

        public DbSet<CTDH> ChiTietDonHang { set; get; }

        public DbSet<HinhThuc> HinhThuc { set; get; }

        public DbSet<LoaiHang> LoaiHang { set; get; }

        public virtual DbSet<ThongkeViewModel> ThongkeViewModel { get; set;}

        public DbSet<ChiTietHang> ChiTietHang { set; get; }

    }
}
