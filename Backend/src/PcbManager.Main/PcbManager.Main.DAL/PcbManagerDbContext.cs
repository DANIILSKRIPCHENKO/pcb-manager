using Microsoft.EntityFrameworkCore;

namespace PcbManager.Main.DAL
{
    public class PcbManagerDbContext : DbContext
    {
        public DbSet<Domain.UserNS.User> Users { get; set; }

        public DbSet<Domain.ImageNS.Image> Images { get; set; }

        public DbSet<Domain.ReportNS.Report> Reports { get; set; }

        public DbSet<Domain.PcbDefectNS.PcbDefect> PcbDefects { get; set; }

        public PcbManagerDbContext(DbContextOptions<PcbManagerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.ApplyConfigurationsFromAssembly(typeof(PcbManagerDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=660003");
        }
    }
}