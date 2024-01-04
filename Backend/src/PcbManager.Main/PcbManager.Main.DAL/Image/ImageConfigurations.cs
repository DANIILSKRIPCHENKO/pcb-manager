using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcbManager.Main.Domain.ImageNS.ValueObjects;

namespace PcbManager.Main.DAL.Image
{
    public class ImageConfigurations : IEntityTypeConfiguration<Domain.ImageNS.Image>
    {
        public void Configure(EntityTypeBuilder<Domain.ImageNS.Image> builder)
        {
            ConfigureImageTable(builder);
        }

        private void ConfigureImageTable(EntityTypeBuilder<Domain.ImageNS.Image> builder)
        {
            builder.ToTable("Images");

            builder.HasKey(x => x.Id);

            builder
                .HasOne<Domain.ReportNS.Report>()
                .WithOne()
                .HasForeignKey<Domain.ReportNS.Report>(x => x.ImageId);

            builder
                .Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value, value => ImageId.Create(value));

            builder
                .Property(x => x.ImageName)
                .HasConversion(
                    imageName => imageName.Value,
                    value => ImageName.Create(value).Value
                );

            builder
                .Property(x => x.ImagePath)
                .HasConversion(
                    imagePath => imagePath.Value,
                    value => ImagePath.Create(value).Value
                );
        }
    }
}
