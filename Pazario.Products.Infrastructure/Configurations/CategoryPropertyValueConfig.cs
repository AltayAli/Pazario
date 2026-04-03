using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pazario.Products.Domain.CategoryPropertyValues;

namespace Pazario.Products.Infrastructure.Configurations
{
    public class CategoryPropertyValueConfig : IEntityTypeConfiguration<CategoryPropertyValue>
    {
        public void Configure(EntityTypeBuilder<CategoryPropertyValue> builder)
        {
            builder.ToTable("CategoryPropertyValues");
            builder.HasKey(x => x.Id);

        }
    }
}
