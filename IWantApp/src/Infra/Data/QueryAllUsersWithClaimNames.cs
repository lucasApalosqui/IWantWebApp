using Dapper;
using IWantApp.Endpoints.Employees;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace IWantApp.Infra.Data;

public class QueryAllUsersWithClaimNames
{
	private readonly IConfiguration configuration;
	public QueryAllUsersWithClaimNames(IConfiguration configuration)
	{
		this.configuration = configuration;
	}

	public IEnumerable<EmployeeResponse> Execute(int page, int rows)
	{
        var acount = (page - 1) * rows;
        using var db = new MySqlConnection(configuration.GetConnectionString("IWantDb"));
        var query = @"Select Email, ClaimValue as Name
            From aspnetusers u INNER JOIN aspnetuserclaims c
            on u.id = c.UserId and ClaimType = 'Name'
            order by Name
            LIMIT @acount,@rows";
            return db.Query<EmployeeResponse>(
            query,
            new { acount, rows }
            );
    }
}
