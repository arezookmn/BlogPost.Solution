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
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options
        ) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Article>().HasQueryFilter(p => !p.IsDeleted);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Comment>().HasQueryFilter(c => !c.IsDeleted);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)  
                .HasForeignKey(c => c.PostID) 
                .IsRequired(false); 

            modelBuilder.Entity<Article>()
                .HasOne(p => p.Category)     
                .WithMany(c => c.Posts)     
                .HasForeignKey(p => p.CategoryID);


            modelBuilder.Entity<Category>()
                .Property(c => c.CategoryID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Article>()
                .HasOne(p => p.ApplicationUser)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.ApplicationUserId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ApplicationUser)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.ApplicationUserId);
        }
    }
}
