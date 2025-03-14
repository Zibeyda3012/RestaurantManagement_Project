using Domain.Enums;

namespace Application.StrategyPattern;

public interface ICarContext
{
    Task<decimal> Calculate(int CarId, PriceType priceType);
}
