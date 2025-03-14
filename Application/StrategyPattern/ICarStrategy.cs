using Domain.Enums;

namespace Application.StrategyPattern;

public interface ICarStrategy
{
    Task<decimal> CalculatePrice(int CarId,PriceType priceType);
}
