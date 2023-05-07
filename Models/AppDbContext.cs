using DoAnLapTrinhWebNC.Models.Contacts;
using DoAnLapTrinhWebNC.Models.Categories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

//DoAnLapTrinhWebNC.Models.AppDbContext
namespace DoAnLapTrinhWebNC.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        //phuonng thuc khoi tao
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)//phuong thuc khoi tao
        {
            base.OnConfiguring(optionsBuilder);


        }

        protected override void OnModelCreating(ModelBuilder modelBuider)
        {
            base.OnModelCreating(modelBuider);


            foreach (var etityType in modelBuider.Model.GetEntityTypes())
            {
                var tableName = etityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    etityType.SetTableName(tableName.Substring(6));
                }
            }

            modelBuider.Entity<Category>(entity =>
            {
                entity.HasIndex(c => c.Slug); // Danh chi muc
            });

        }
        public DbSet<Contact> Contacts { set; get; }
        public DbSet<Category> Categories { set; get; }
    }
}