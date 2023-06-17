using Microsoft.AspNetCore.Mvc;
using PcbManager.App;
using PcbManager.DAL;
using PcbManager.DAL.Image;
using PcbManager.DAL.User;
using PcbManager.FileSystem;

namespace PcbManager.WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers().AddNewtonsoftJson(opt =>
            opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            builder.Services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddTransient<IUserAppService, UserAppService>();
            builder.Services.AddTransient<IImageAppService, ImageAppService>();

            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IImageRepository, ImageRepository>();

            builder.Services.AddTransient<IImageFileSystem, ImageFileSystem>();

            builder.Services.AddDbContext<PcbManagerDbContext>();

            const string solutionName = $"{nameof(PcbManager)}";

            builder.Services.AddAutoMapper(x => x.AddMaps(
                $"{solutionName}.{nameof(WebApi)}"
            ));

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
        }
    }
}