
using BlogPost.WebApi.StartupExtension;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((HostBuilderContext context,
    IServiceProvider services,
    LoggerConfiguration loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(context.Configuration);
    loggerConfiguration.Enrich.FromLogContext();
    loggerConfiguration.ReadFrom.Services(services);
}); 

builder.Services.ConfigureServices(builder.Configuration);
 var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policyName: "AllowAnyOrigin");


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
