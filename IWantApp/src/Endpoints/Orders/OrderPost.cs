using IWantApp.Domain.Orders;
using IWantApp.Domain.Products;
using IWantApp.Endpoints.Clients;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace IWantApp.Endpoints.Orders;

public class OrderPost
{
    public static string Template => "/orders";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "CpfPolicy")]
    public static async Task<IResult> Action(OrderRequest orderRequest, HttpContext http, ApplicationDbContext context)
    {
        var clientId = http.User.Claims
            .First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var clientName = http.User.Claims
            .First(c => c.Type == "Name").Value;

        List<Product> productsFound = null;
        if(orderRequest.ProductsId != null || orderRequest.ProductsId.Any())
        {
            productsFound = context.Products.Where(p => orderRequest.ProductsId.Contains(p.Id)).ToList();
        }


        var order = new Order(clientId, clientName, productsFound, orderRequest.DeliveryAdress);
        if (!order.IsValid)
        {
            return Results.ValidationProblem(order.Notifications.ConvertToProblemDetails());
        }
        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();

        return Results.Created($"/orders/{order.Id}", order.Id);

    }
}
