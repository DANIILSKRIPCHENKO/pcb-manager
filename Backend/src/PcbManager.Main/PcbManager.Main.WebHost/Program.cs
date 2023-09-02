using PcbManager.Main.App.Image;
using PcbManager.Main.App.PcbDefect;
using PcbManager.Main.App.Report;
using PcbManager.Main.App.User;
using PcbManager.Main.DAL;
using PcbManager.Main.DAL.Image;
using PcbManager.Main.DAL.PcbDefect;
using PcbManager.Main.DAL.Report;
using PcbManager.Main.DAL.User;
using PcbManager.Main.FileSystem;
using Microsoft.IdentityModel.Tokens;
using static CSharpFunctionalExtensions.Result;
using PcbManager.Main.WebHost.Security;
using Microsoft.OpenApi.Models;

namespace PcbManager.Main.WebHost
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
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "PcbManager.Main", Version = "v1" });
                options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    Description = "ApiKey must appear in header",
                    Type = SecuritySchemeType.ApiKey,
                    Name = "X-API-KEY",
                    In = ParameterLocation.Header,
                    Scheme = "ApiKey"
                });
                var key = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "ApiKey"
                    },
                    In = ParameterLocation.Header
                };
                var requirement = new OpenApiSecurityRequirement
                {
                    { key, new List<string>() }
                };
                options.AddSecurityRequirement(requirement);
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme,
                            }
                        },
                        new List<string>()
                    }
                });
            });

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

            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:7048";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            builder.Services
                .AddAuthentication("ApiKey")
                .AddScheme<ApiKeyAuthenticationSchemeOptions, 
                ApiKeyAuthenticationSchemeHandler>("ApiKey", opts => opts.ApiKey = "ApiKey");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}