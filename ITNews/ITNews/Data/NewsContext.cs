using ITNews.Models;
using Microsoft.EntityFrameworkCore;

namespace ITNews.Data
{
    public class NewsContext : DbContext
    {
        public NewsContext(DbContextOptions<NewsContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var news = modelBuilder.Entity<News>();
            news.ToTable("News");
            news.Property(c => c.Name).IsRequired().IsUnicode().HasMaxLength(100);
            news.Property(c => c.Created).IsRequired().HasDefaultValueSql("getdate()");
            news.Property(c => c.ShortDescription).IsRequired().HasMaxLength(500);
            news.Property(c => c.Description).IsRequired().HasMaxLength(5000);
            news.HasOne(s => s.Category).WithMany(s => s.News).IsRequired();
            news.HasMany(s => s.Comments).WithOne(s => s.News);

            var category = modelBuilder.Entity<Category>();
            category.ToTable("Categories");
            category.Property(c => c.Name).IsRequired().IsUnicode().HasMaxLength(200);
            category.HasData(
                new Category { Id = 1, Name = "C#" },
                new Category { Id = 2, Name = "JavaScript" },
                new Category { Id = 3, Name = "Java" },
                new Category { Id = 4, Name = "C++" },
                new Category { Id = 5, Name = "Algorithms" },
                new Category { Id = 6, Name = "Machine Learning" },
                new Category { Id = 7, Name = "Other" }
            );

            var tag = modelBuilder.Entity<Tag>();
            tag.ToTable("Tags");
            tag.Property(c => c.Name).IsRequired().IsUnicode().HasMaxLength(200);
            tag.HasData(
                new Tag { Id = 1, Name = "Programming" },
                new Tag { Id = 2, Name = "Science" },
                new Tag { Id = 3, Name = "Success" }
            );

            var newsTags = modelBuilder.Entity<NewsTags>();
            newsTags.HasKey(p => new { p.NewsId, p.TagId });
            newsTags
                 .HasOne(p => p.News)
                 .WithMany(p => p.Tags)
                 .HasForeignKey(p => p.NewsId);
            newsTags
                 .HasOne(p => p.Tag)
                 .WithMany(p => p.News)
                 .HasForeignKey(p => p.TagId);

            var comment = modelBuilder.Entity<Comment>();
            comment.ToTable("Comments");
            comment.Property(c => c.Body).IsRequired().IsUnicode().HasMaxLength(300);
            comment.Property(c => c.UserId).IsRequired();
            comment.HasOne(s => s.News).WithMany(s => s.Comments);
        }
    }
}
