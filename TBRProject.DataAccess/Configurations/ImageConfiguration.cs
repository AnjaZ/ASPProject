using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBRProject.Domain;

namespace TBRProject.DataAccess.Configurations
{
    public class ImageConfiguration : EntityConfiguration<Image>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Image> builder)
        {
            builder.Property(x => x.Path).HasMaxLength(100).IsRequired();

            builder.HasMany(x => x.Users)
                   .WithOne(x => x.Image)
                   .HasForeignKey(x => x.ImageId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(x => x.Books)
                .WithOne(x => x.Image)
                .HasForeignKey(x => x.ImageId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
