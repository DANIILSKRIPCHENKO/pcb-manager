using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcbManager.Main.Domain.ReportNS.ValueObjects;

namespace PcbManager.Main.DAL.Report;

public class ReportConfigurations : IEntityTypeConfiguration<Domain.ReportNS.Report>
{
    public void Configure(EntityTypeBuilder<Domain.ReportNS.Report> builder)
    {
        ConfigureReportsTable(builder);
    }

    private void ConfigureReportsTable(EntityTypeBuilder<Domain.ReportNS.Report> builder)
    {
        builder.ToTable("Reports");

        builder.HasMany<Domain.PcbDefectNS.PcbDefect>().WithOne().HasForeignKey(x => x.ReportId);

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => ReportId.Create(value).Value);
    }
}
