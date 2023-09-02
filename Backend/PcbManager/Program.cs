using PcbManager.App.Image;
using PcbManager.App.PcbDefect;
using PcbManager.App.Report;
using PcbManager.App.User;
using PcbManager.DAL;
using PcbManager.DAL.Image;
using PcbManager.DAL.PcbDefect;
using PcbManager.DAL.Report;
using PcbManager.DAL.User;
using PcbManager.FileSystem;

namespace PcbManager.WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

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
            builder.Services.AddTransient<IReportAppService, ReportAppService>();
            builder.Services.AddTransient<IPcbDefectAppService, PcbDefectAppService>();

            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IImageRepository, ImageRepository>();
            builder.Services.AddTransient<IReportRepository, ReportRepository>();
            builder.Services.AddTransient<IPcbDefectRepository, PcbDefectRepository>();

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