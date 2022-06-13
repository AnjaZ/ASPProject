using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBRProject.Domain;

namespace TBRProject.DataAccess.Configurations
{
    public class UserConfiguration : EntityConfiguration<User>
    {
        protected override void ConfigureRules(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(x => x.FirstName);
            builder.HasIndex(x => x.LastName);
            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.FirstName).HasMaxLength(40).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Password).IsRequired();

            builder.HasMany(x => x.AuthorBooks).WithOne(x => x.Author).HasForeignKey(x => x.UserId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
            builder.HasMany(x => x.UserBooks).WithOne(x => x.Reader).HasForeignKey(x => x.UserId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
            builder.HasMany(x => x.Reviews).WithOne(x => x.Users).HasForeignKey(x => x.UserId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
            builder.HasMany(x => x.Likes).WithOne(x => x.Users).HasForeignKey(x => x.UserId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.HasMany(x => x.UseCases).WithOne(x => x.User).HasForeignKey(x => x.UserId);

        }
    }
}
