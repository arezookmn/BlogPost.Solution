using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Infrustructure.DbContext
{
    public class ApplicationDbContext :Microsoft.EntityFrameworkCore.DbContext
    {
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<Category> Categories { get; set; } 
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options
        ) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>().HasQueryFilter(p => !p.IsDeleted);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Comment>().HasQueryFilter(c => !c.IsDeleted);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)           // Comment has one Post
                .WithMany(p => p.Comments)     // Post has many Comments
                .HasForeignKey(c => c.PostID) // Foreign key relationship
                .IsRequired(false); // Mark the relationship as optional

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Category)     // Post has one Category
                .WithMany(c => c.Posts)      // Category has many Posts
                .HasForeignKey(p => p.CategoryID); // Foreign key relationship


            modelBuilder.Entity<Category>()
                .Property(c => c.CategoryID)
                .ValueGeneratedOnAdd();

        }
    }
}
