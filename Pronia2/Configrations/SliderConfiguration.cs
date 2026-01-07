using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pronia2.Configrations
{
    public class SliderConfiguration: IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ImageUrl).IsRequired().HasMaxLength(512);
            builder.Property(x => x.DiscountPercentage).IsRequired().HasPrecision(10, 2);
        }
    }
}
