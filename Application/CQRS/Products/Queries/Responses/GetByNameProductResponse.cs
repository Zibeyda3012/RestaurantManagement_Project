namespace Application.CQRS.Products.Queries.Responses;

public class GetByNameProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedDate { get; set; }
}
