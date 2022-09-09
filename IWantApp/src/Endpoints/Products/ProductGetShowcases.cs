using Google.Protobuf.WellKnownTypes;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Relational;

namespace IWantApp.Endpoints.Products;

public class ProductGetShowCases
{
    public static string Template => "/products/showcase";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(QueryAllProducts query, int page = 1, int row = 10, string orderby = "name")
    {
        if(row > 50)
        {
            return Results.Problem(title:"No more 50 rows are permited", statusCode: 400);
        }

        if(orderby != "name" && orderby != "price")
        {
            return Results.Problem(title: "Order by just only price or name", statusCode: 400);
        }


        var queryFilter = await query.Execute(page, row);

        if(orderby == "name")
        {
            queryFilter = queryFilter.OrderBy(p => p.Name);
        }
        else
        {
            queryFilter = queryFilter.OrderBy(p => p.price);
        }

        
        return Results.Ok(queryFilter);
    }
}
