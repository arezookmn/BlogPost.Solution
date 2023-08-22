using BlogPost.Core.Domain.RepositoryContracts;
using BlogPost.Core.Service.CommentService;
using BlogPost.Core.Service.PostService;
using BlogPost.Core.ServiceContracts.CommentServicesInterface;
using BlogPost.Core.ServiceContracts.PostServicesInterface;
using BlogPost.Infrustructure.DbContext;
using BlogPost.Infrustructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.WebApi.StartupExtension
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddControllers();
            services.AddScoped<IPostAdderService, PostAdderService>();
            services.AddScoped<IPostGetterService, PostGetterService>();
            services.AddScoped<IPostUpdaterService, PostUpdaterService>();
            services.AddScoped<IPostDeleterService, PostDeleterService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<ApplicationDbContext>(options =>
                { options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")); });

            return services;
        }

    }
}
