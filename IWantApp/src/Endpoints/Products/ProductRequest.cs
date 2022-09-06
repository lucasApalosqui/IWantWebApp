namespace IWantApp.Endpoints.Products;

public record ProductRequest(Guid CategoryId, string Name, string Description, bool HasStock, bool Active, decimal Price);

