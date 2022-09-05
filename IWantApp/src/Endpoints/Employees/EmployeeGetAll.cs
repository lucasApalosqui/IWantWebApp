using Dapper;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Security.Claims;

namespace IWantApp.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(int? page, int? rows, QueryAllUsersWithClaimNames query)
    {
        if (page == null || rows == null)
        {
            return Results.BadRequest("Rows and Pages must be defined!!");
        }
        if (rows > 10)
        {
            return Results.BadRequest("Rows doesnt be more than 10");
        }
        var result = await query.Execute(page.Value, rows.Value);
        return Results.Ok(result);
    }
}
