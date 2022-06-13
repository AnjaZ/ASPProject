using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBRProject.Domain;

namespace TBRProject.DataAccess.Configurations
{
    public class BookConfiguration : EntityConfiguration<Book>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Book> builder)
        {
            builder.HasIndex(x => x.Title);
            builder.Property(x => x.Title).HasMaxLength(150).IsRequired();

            builder.HasMany(x => x.Authors).WithOne(x => x.Book).HasForeignKey(x => x.BookId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.HasMany(x => x.Users).WithOne(x => x.Books).HasForeignKey(x => x.BookId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
            builder.HasMany(x => x.Reviews).WithOne(x => x.Books).HasForeignKey(x => x.BookId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
            builder.HasMany(x => x.BookGenre).WithOne(x => x.Book).HasForeignKey(x => x.BookId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);//da li se onda sa cascade brise i genre ako se obrise knjiga

        }
    }
}
