using Domain.Enums;

namespace Application.StrategyPattern;

public class CarContext(ICarStrategy carStrategy) : ICarContext
{
    private readonly ICarStrategy _carStrategy = carStrategy;

    public Task<decimal> Calculate(int CarId, PriceType priceType)
    {
        return _carStrategy.CalculatePrice(CarId, priceType);
    }
}
