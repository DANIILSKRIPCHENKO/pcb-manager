namespace PcbManager.Security.WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddIdentityServer()
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients);

            var app = builder.Build();

            app.UseIdentityServer();
            app.UseHttpsRedirection();
            app.Run();
        }
    }
}