using System.Text;
using System.Text.Unicode;
using BlogPost.Core.Domain.Entities.IdentityEntities;
using BlogPost.Core.Domain.RepositoryContracts;
using BlogPost.Core.Service.CategoryServices;
using BlogPost.Core.Service.CommentService;
using BlogPost.Core.Service.IdentityService;
using BlogPost.Core.Service.PostService;
using BlogPost.Core.ServiceContracts.CategoryServiceInterface;
using BlogPost.Core.ServiceContracts.CommentServicesInterface;
using BlogPost.Core.ServiceContracts.IdentityServiceContracts;
using BlogPost.Core.ServiceContracts.PostServicesInterface;
using BlogPost.Infrustructure.DbContext;
using BlogPost.Infrustructure.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
            services.AddScoped<ICategoryItemGetterServiceInterface, CategoryItemGetterService>();
            services.AddScoped<ICategoryAdminService, CategoryAdminService>();
            services.AddTransient<IJwtService, JwtService>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();


            
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<ApplicationDbContext>(options =>
                { options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")); });


            services.AddIdentity<ApplicationUser, ApplicationRole>(opt =>
                {
                    opt.Password.RequiredLength = 6;
                    opt.Password.RequireDigit = true;
                    opt.Password.RequireLowercase = true;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireNonAlphanumeric = false;
                })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
                .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidAudience = configuration["Jwt:Audience"],
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),


                    };
                });

            services.AddAuthorization(options =>
            {

            });

            return services;
        }

    }
}
