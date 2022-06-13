using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBRProject.Domain;

namespace TBRProject.DataAccess.Configurations
{
    public class ReviewConfiguration : EntityConfiguration<Review>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Review> builder)
        {
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.Stars).IsRequired();

            builder.HasMany(x => x.Likes).WithOne(x=> x.Reviews).HasForeignKey(x => x.ReviewId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);

        }
    }
}
