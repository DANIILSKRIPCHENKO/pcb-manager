using PcbManager.Main.App.Image;
using PcbManager.Main.App.PcbDefect;
using PcbManager.Main.App.Report;
using PcbManager.Main.App.User;
using PcbManager.Main.DAL;
using PcbManager.Main.DAL.Image;
using PcbManager.Main.DAL.PcbDefect;
using PcbManager.Main.DAL.Report;
using PcbManager.Main.DAL.User;
using PcbManager.Main.FileSystemNS;
using Microsoft.IdentityModel.Tokens;
using PcbManager.Main.WebHost.Security;
using Microsoft.OpenApi.Models;
using PcbManager.Main.WebHost.OpenApi;
using PcbManager.Main.WebApi.Security;
using static System.Net.WebRequestMethods;
using PcbManager.Main.App;
using System.Transactions;
using PcbManager.Main.App.Abstractions;
using PcbManager.Main.DAL.Configuration;

namespace PcbManager.Main.WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            // Add services to the container.
            builder.Services
                .AddControllers()
                .AddNewtonsoftJson(
                    opt =>
                        opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft
                            .Json
                            .ReferenceLoopHandling
                            .Ignore
                );

            builder.Services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });

            builder.Services.Configure<DalConfiguration>(builder.Configuration.GetSection("Dal"));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    "v1",
                    new OpenApiInfo { Title = "PcbManager.Main", Version = "v1" }
                );
                options.AddSecurityDefinition(
                    "ApiKey",
                    new OpenApiSecurityScheme
                    {
                        Description = "ApiKey must appear in header",
                        Type = SecuritySchemeType.ApiKey,
                        Name = "X-API-KEY",
                        In = ParameterLocation.Header,
                        Scheme = "ApiKey"
                    }
                );
                options.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Description =
                            "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    }
                );

                options.AddSecurityDefinition(
                    "OAuth2",
                    new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.OAuth2,
                        Scheme = "OAuth2",
                        Flows = new OpenApiOAuthFlows()
                        {
                            ClientCredentials = new OpenApiOAuthFlow()
                            {
                                TokenUrl = new Uri("https://localhost:7048/connect/token"),
                                Scopes =
                                {
                                    { Scopes.ImageRead, Scopes.ImageRead },
                                    { Scopes.ImageWrite, Scopes.ImageWrite },
                                    { Scopes.ReportRead, Scopes.ReportRead },
                                    { Scopes.ReportWrite, Scopes.ReportWrite },
                                    { Scopes.UserRead, Scopes.UserRead },
                                    { Scopes.UserWrite, Scopes.UserWrite },
                                },
                            }
                        }
                    }
                );

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            builder.Services.AddTransient<IUserAppService, UserAppService>();
            builder.Services.AddTransient<IImageAppService, ImageAppService>();
            builder.Services.AddTransient<IReportAppService, ReportAppService>();
            builder.Services.AddTransient<IPcbDefectAppService, PcbDefectAppService>();

            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IImageRepository, ImageRepository>();
            builder.Services.AddTransient<IReportRepository, ReportRepository>();
            builder.Services.AddTransient<IPcbDefectRepository, PcbDefectRepository>();

            builder.Services.AddTransient<ITransactionManager, DAL.TransactionManager>();

            builder.Services.AddTransient<IFileSystem, FileSystem>();
            builder.Services.AddTransient<IReportGeneratorAdapter, ReportGeneratorAdapter>();

            builder.Services.AddDbContext<PcbManagerDbContext>();

            const string solutionName = $"{nameof(PcbManager)}";

            builder.Services.AddAutoMapper(x => x.AddMaps($"{solutionName}.{nameof(WebApi)}"));

            builder.Services
                .AddAuthentication("Bearer")
                .AddJwtBearer(
                    "Bearer",
                    options =>
                    {
                        options.Authority = "https://localhost:7048";
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateAudience = false
                        };
                    }
                );

            builder.Services
                .AddAuthentication("ApiKey")
                .AddScheme<ApiKeyAuthenticationSchemeOptions, ApiKeyAuthenticationSchemeHandler>(
                    "ApiKey",
                    opts => opts.ApiKey = "ApiKey"
                );

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    Policies.ImageRead,
                    policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.RequireClaim("scope", Scopes.ImageRead);
                    }
                );
                options.AddPolicy(
                    Policies.ImageWrite,
                    policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.RequireClaim("scope", Scopes.ImageWrite);
                    }
                );
                options.AddPolicy(
                    Policies.ReportRead,
                    policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.RequireClaim("scope", Scopes.ReportRead);
                    }
                );
                options.AddPolicy(
                    Policies.ReportWrite,
                    policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.RequireClaim("scope", Scopes.ReportWrite);
                    }
                );
                options.AddPolicy(
                    Policies.UserRead,
                    policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.RequireClaim("scope", Scopes.UserRead);
                    }
                );
                options.AddPolicy(
                    Policies.UserWrite,
                    policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.RequireClaim("scope", Scopes.UserWrite);
                    }
                );
                options.AddPolicy(
                    Policies.AiWrite,
                    policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.RequireClaim("scope", Scopes.AiWrite);
                    }
                );
            });

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
