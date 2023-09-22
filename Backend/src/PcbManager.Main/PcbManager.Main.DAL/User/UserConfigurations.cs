using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcbManager.Main.Domain.Common;
using PcbManager.Main.Domain.UserNS.ValueObjects;

namespace PcbManager.Main.DAL.User
{
    public class UserConfigurations : IEntityTypeConfiguration<Domain.UserNS.User>
    {
        public void Configure(EntityTypeBuilder<Domain.UserNS.User> builder)
        {
            ConfigureUserTable(builder);
        }

        private void ConfigureUserTable(EntityTypeBuilder<Domain.UserNS.User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.HasMany<Domain.ImageNS.Image>().WithOne().HasForeignKey(x => x.UserId);

            builder
                .Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value, value => UserId.Create(value));

            builder
                .Property(x => x.Name)
                .HasConversion(name => name.Value, value => UserName.Create(value).Value);

            builder
                .Property(x => x.Surname)
                .HasConversion(surname => surname.Value, value => UserSurname.Create(value).Value);

            builder
                .Property(x => x.Email)
                .HasConversion(email => email.Value, value => UserEmail.Create(value).Value);

            builder
                .Property(x => x.Password)
                .HasConversion(
                    password => password.Value,
                    value => UserPassword.Create(value).Value
                );

            builder
                .Property(x => x.CreatedAt)
                .HasConversion(
                    createdAt => createdAt.Value,
                    value => CreatedAt.Create(value).Value
                );

            builder
                .Property(x => x.UpdatedAt)
                .HasConversion(
                    updatedAt => updatedAt.Value,
                    value => UpdatedAt.Create(value).Value
                );
        }
    }
}
