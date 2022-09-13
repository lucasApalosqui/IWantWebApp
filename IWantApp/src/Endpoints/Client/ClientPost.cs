using IWantApp.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IWantApp.Endpoints.Employees;

public record ClientRequest(string Email, string Password, string Name, string cpf);


public class ClientPost
{
    public static string Template => "/clients";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public  static async Task<IResult> Action(ClientRequest clientRequest, HttpContext http, UserCreator userCreator)
    {
        var userClaims = new List<Claim>
        {
            new Claim("cpf", clientRequest.cpf),
            new Claim("Name", clientRequest.Name)

        };

        (IdentityResult identity, string userId) result =
            await userCreator.Create(clientRequest.Email, clientRequest.Password, userClaims);

        if (!result.identity.Succeeded)
        {
            return Results.ValidationProblem(result.identity.Errors.ConvertToProblemDetails());

        }

        return Results.Created($"/clients/{result.userId}", result.userId);
    }
}
