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

        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options
        ) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
