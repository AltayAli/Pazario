using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pazario.Products.Domain.Markas;

namespace Pazario.Products.Infrastructure.Configurations
{
    public class MarkaConfig : IEntityTypeConfiguration<Marka>
    {
        public void Configure(EntityTypeBuilder<Marka> builder)
        {
            builder.ToTable("Markas");
            builder.HasKey(x => x.Id);
            builder.OwnsOne(x => x.Name, name =>
            {
                name.Property(n => n.Value).HasColumnName("Name").IsRequired();
            });

        }
    }
}
