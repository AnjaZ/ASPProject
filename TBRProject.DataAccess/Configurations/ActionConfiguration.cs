using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBRProject.Domain;


namespace TBRProject.DataAccess.Configurations
{
    public class ActionConfiguration : EntityConfiguration<ReaderAction>
    {
        protected override void ConfigureRules(EntityTypeBuilder<ReaderAction> builder)
        {
            builder.HasIndex(x => x.Name);
            builder.Property(x => x.Name).HasMaxLength(150).IsRequired();

            builder.HasMany(x => x.Reader).WithOne(x => x.BookAction).HasForeignKey(x => x.Action).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
