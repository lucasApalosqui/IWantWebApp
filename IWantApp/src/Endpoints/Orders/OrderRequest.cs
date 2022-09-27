namespace IWantApp.Endpoints.Orders;

public record OrderRequest(List<Guid> ProductsId, string DeliveryAdress);

