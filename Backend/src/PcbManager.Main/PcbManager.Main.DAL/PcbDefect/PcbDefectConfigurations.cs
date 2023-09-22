using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcbManager.Main.Domain.PcbDefectNS.ValueObjects;

namespace PcbManager.Main.DAL.PcbDefect
{
    public class PcbDefectsConfigurations : IEntityTypeConfiguration<Domain.PcbDefectNS.PcbDefect>
    {
        public void Configure(EntityTypeBuilder<Domain.PcbDefectNS.PcbDefect> builder)
        {
            ConfigurePcbDefectsTable(builder);
        }

        private void ConfigurePcbDefectsTable(
            EntityTypeBuilder<Domain.PcbDefectNS.PcbDefect> builder
        )
        {
            builder.ToTable("PcbDefects");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value, value => PcbDefectId.Create(value).Value);

            builder
                .Property(x => x.PcbDefectType)
                .HasConversion(
                    pcbDefectType => (int)pcbDefectType.Value,
                    value => PcbDefectType.Create((PcbDefectTypeEnum)value).Value
                );
        }
    }
}
