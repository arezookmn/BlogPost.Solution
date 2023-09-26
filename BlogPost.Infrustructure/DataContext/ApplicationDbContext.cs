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
        public virtual DbSet<Author> Authors { get; set; }


        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options
        ) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Article>().HasQueryFilter(p => !p.IsDeleted);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comment>().HasQueryFilter(c => !c.IsDeleted);

            modelBuilder.Entity<Category>()
                .Property(c => c.CategoryID)
                .ValueGeneratedOnAdd();


            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.Author)
                .WithOne(u => u.ApplicationUser)
                .HasForeignKey<Author>(a => a.ApplicationUserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Article)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.ArticleID)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
