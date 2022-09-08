using Dapper;
using IWantApp.Endpoints.Employees;
using IWantApp.Endpoints.Products;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace IWantApp.Infra.Data;

public class QueryAllProducts
{
	private readonly IConfiguration configuration;
	public QueryAllProducts(IConfiguration configuration)
	{
		this.configuration = configuration;
	}

	public async Task<IEnumerable<ProductResponse>> Execute(int page, int row)
	{
        var acount = (page - 1) * row;
        using var db = new MySqlConnection(configuration.GetConnectionString("IWantDb"));
        var query = @"Select products.Name, categories.Name as CategoryName, Description, HasStock, products.Active, Price 
            From products  INNER JOIN categories
            on products.CategoryId = categories.Id
            WHERE products.HasStock = 1 and products.Active = 1
            LIMIT @acount,@row";
            return await db.QueryAsync<ProductResponse>(
            query,
            new { acount, row }
            );
    }
}
