using Microsoft.AspNetCore.Mvc.Routing;

namespace PcbManager.Main.WebApi.Customization;

public class StandardRouteAttribute : Attribute, IRouteTemplateProvider
{
    public string? Template => $"api/pcb-manager/{Version ?? "v1"}/{Controller ?? "[controller]"}";

    public int? Order => 1;

    public string? Name { get; set; }
    public string? Version { get; set; }
    public string? Controller { get; set; }
}