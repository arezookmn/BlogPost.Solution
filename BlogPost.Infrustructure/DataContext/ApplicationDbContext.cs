using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;
using BlogPost.Core.Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Infrustructure.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<UserLike> UserLikes { get; set; }

        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options
        ) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Article>().HasQueryFilter(p => !p.IsDeleted);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Comment>().HasQueryFilter(c => !c.IsDeleted);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Article)
                .WithMany(p => p.Comments)  
                .HasForeignKey(c => c.ArticleID) 
                .IsRequired(false); 

            modelBuilder.Entity<Article>()
                .HasOne(p => p.Category)     
                .WithMany(c => c.Articles)     
                .HasForeignKey(p => p.CategoryID);


            modelBuilder.Entity<Category>()
                .Property(c => c.CategoryID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Article>()
                .HasOne(p => p.ApplicationUser)
                .WithMany(u => u.Articles)
                .HasForeignKey(p => p.AuthorId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ApplicationUser)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.ApplicationUserId);

            // Configure the UserLike entity relationships
            modelBuilder.Entity<UserLike>()
                .HasOne(ul => ul.User)
                .WithMany(u => u.LikedArticles)
                .HasForeignKey(ul => ul.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete when a user is deleted

            modelBuilder.Entity<UserLike>()
                .HasOne(ul => ul.Article)
                .WithMany(a => a.LikedArticles)
                .HasForeignKey(ul => ul.ArticleId)
                .OnDelete(DeleteBehavior.Restrict); // No cascading delete when an article is deleted


        }
    }
}
