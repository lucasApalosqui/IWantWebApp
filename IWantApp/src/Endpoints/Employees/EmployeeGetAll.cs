using Dapper;
using IWantApp.Infra.Data;
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

    public static IResult Action(int? page, int? rows, IConfiguration configuration)
    {
        if(page == null || rows == null)
        {
            return Results.BadRequest("Rows and Pages must be defined!!");
        }
        if(rows > 10)
        {
            return Results.BadRequest("Rows doesnt be more than 10");
        }
        var acount = (page - 1) * rows;
        using var db = new MySqlConnection(configuration.GetConnectionString("IWantDb"));
        var query = @"Select Email, ClaimValue as Name
            From aspnetusers u INNER JOIN aspnetuserclaims c
            on u.id = c.UserId and ClaimType = 'Name'
            order by Name
            LIMIT @acount,@rows";
        var employees = db.Query<EmployeeResponse>(
            query,
            new { acount, rows }
            );
        return Results.Ok(employees);
    }
}
