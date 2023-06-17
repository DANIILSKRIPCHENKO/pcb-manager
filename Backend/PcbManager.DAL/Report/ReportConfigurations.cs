using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcbManager.Domain.ReportNS.ValueObjects;

namespace PcbManager.DAL.Report;

public class ReportConfigurations : IEntityTypeConfiguration<Domain.ReportNS.Report>
{
    public void Configure(EntityTypeBuilder<Domain.ReportNS.Report> builder)
    {
        ConfigureReportsTable(builder);
    }

    private void ConfigureReportsTable(EntityTypeBuilder<Domain.ReportNS.Report> builder)
    {
        builder.ToTable("Reports");

        builder.HasOne(x => x.Image)
            .WithOne(x => x.Report)
            .HasForeignKey<Domain.ReportNS.Report>(x => x.ImageId);

        builder.HasMany(x => x.PcbDefects).WithOne(x => x.Report);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => ReportId.Create(value).Value);
    }
}