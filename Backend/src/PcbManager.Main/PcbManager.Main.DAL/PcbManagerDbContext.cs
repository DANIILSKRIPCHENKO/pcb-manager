using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PcbManager.Main.DAL.Configuration;

namespace PcbManager.Main.DAL
{
    public class PcbManagerDbContext : DbContext
    {
        private readonly DalConfiguration _dalConfiguration;

        public PcbManagerDbContext(IOptionsSnapshot<DalConfiguration> dalConfiguration)
        {
            _dalConfiguration = dalConfiguration.Value;
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.ApplyConfigurationsFromAssembly(typeof(PcbManagerDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_dalConfiguration.ConnectionString);
        }

        public DbSet<Domain.UserNS.User> Users { get; set; }

        public DbSet<Domain.ImageNS.Image> Images { get; set; }

        public DbSet<Domain.ReportNS.Report> Reports { get; set; }

        public DbSet<Domain.PcbDefectNS.PcbDefect> PcbDefects { get; set; }
    }
}
