namespace Application.CQRS.Cars.DTOs;

public class GetPriceDTO
{
    public int Id { get; set; }
    public string Model { get; set; }
    public string Vendor { get; set; }
    public DateTime Year { get; set; }
    public decimal Price { get; set; }
}
