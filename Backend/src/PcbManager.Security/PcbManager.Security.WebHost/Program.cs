namespace PcbManager.Security.WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddIdentityServer()
                .AddInMemoryApiResources(Config.ApiResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients);

            builder.Services.AddCors();

            var app = builder.Build();

            app.UseCors(
                x =>
                    x.AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(origin => true)
                        .AllowCredentials()
            );

            app.UseIdentityServer();
            app.UseHttpsRedirection();
            app.Run();
        }
    }
}
