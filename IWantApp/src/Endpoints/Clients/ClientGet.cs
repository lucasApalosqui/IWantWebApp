using Microsoft.AspNetCore.Authorization;
using System.Net.NetworkInformation;
using System.Security.Claims;

namespace IWantApp.Endpoints.Clients;

public class ClientGet
{
    public static string Template => "/Clients";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(HttpContext http)
    {
        var user = http.User;
        var result = new
        {
            Email = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value,
            cpf = user.Claims.First(c => c.Type == "Cpf").Value,

        };

        return Results.Ok(result);

    }
}
