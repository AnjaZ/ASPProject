using Microsoft.EntityFrameworkCore;
using System;
using TBRProject.Domain;

namespace TBRProject.DataAccess
{
    public class TBRContext : DbContext
    {
        public TBRContext(DbContextOptions options) : base(options)
        {

        }
        public IApplicationUser User { get; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            modelBuilder.Entity<AuthorBook>().HasKey(x => new { x.BookId, x.UserId });
            modelBuilder.Entity<BookGenre>().HasKey(x => new { x.BookId, x.GenreId });
            modelBuilder.Entity<UserBook>().HasKey(x => new { x.BookId, x.UserId, x.Action});
            modelBuilder.Entity<Like>().HasKey(x => new { x.ReviewId, x.UserId });
            modelBuilder.Entity<Like>().Property(x => x.LikedAt).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<UserUseCase>().HasKey(x => new { x.UserId, x.UseCaseId });
            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data Source=ms-ns-07;Initial Catalog=tbrprojectasp;Integrated Security=True");
        //}

        public override int SaveChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries())
            {
                if (entry.Entity is Entity e)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            e.CreatedAt = DateTime.UtcNow;
                            break;
                        case EntityState.Modified:
                            e.UpdatedAt = DateTime.UtcNow;
                            e.UpdatedBy = User?.Identity;
                            break;
                    }
                }
            }

            return base.SaveChanges();
        }

        public DbSet<Image> Images { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<UserBook> ReadersList { get; set; }
        public DbSet<ReaderAction> ReaderActions { get; set; }
        public DbSet<UserUseCase> UserUseCases { get; set; }
    }
}
