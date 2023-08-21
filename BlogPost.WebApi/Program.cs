using BlogPost.Core.Domain.RepositoryContracts;
using BlogPost.Core.Service.PostService;
using BlogPost.Core.ServiceContracts;
using BlogPost.Infrustructure.DbContext;
using BlogPost.Infrustructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IPostAdderService, PostAdderService>();
builder.Services.AddScoped<IPostGetterService, PostGetterService>();
builder.Services.AddScoped<IPostUpdaterService, PostUpdaterService>();
builder.Services.AddScoped<IPostRepository, PostRepository>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    { options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
