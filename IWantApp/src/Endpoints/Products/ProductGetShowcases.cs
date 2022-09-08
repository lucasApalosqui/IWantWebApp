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
    public static async Task<IResult> Action(int? page, int? row, string? orderby ,QueryAllProducts query)
    {
        if (page == null)
        {
            page = 1;
        }
        if (row == null)
        {
            row = 1;
        }

        if (string.IsNullOrEmpty(orderby))
        {
            orderby = "name";
        }

        if(orderby != "name" && orderby != "price")
        {
            orderby = "name";
        }


        var queryFilter = await query.Execute(page.Value, row.Value);

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
