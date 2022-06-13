using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBRProject.Domain;

namespace TBRProject.DataAccess.Configurations
{
    public class GenreConfiguration : EntityConfiguration<Genre>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Genre> builder)
        {
            builder.HasIndex(x => x.Name);
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

            builder.HasMany(x => x.BookGenre).WithOne(x => x.Genre).HasForeignKey(x => x.GenreId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
