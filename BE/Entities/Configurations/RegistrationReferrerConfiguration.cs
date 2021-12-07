using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApi.Entities.Configurations
{
	public class RegistrationReferrerConfiguration : IEntityTypeConfiguration<RegistrationReferrer>
    {
        public void Configure(EntityTypeBuilder<RegistrationReferrer> builder)
        {
            builder.HasKey(x=>x.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(450);
        }
    }
}