using Common.Exceptions;
using Domain.Enums;
using Repository.Common;

namespace Application.StrategyPattern;

public class CarStrategy(IUnitOfWork unitOfWork) : ICarStrategy
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<decimal> CalculatePrice(int CarId, PriceType priceType)
    {
        var currentCar = await _unitOfWork.CarRepository.GetByIdAsync(CarId);
        if (currentCar == null) { throw new BadRequestException("car with provided id doesn't exist"); }

        switch (priceType)
        {
            case PriceType.Standart:
                return currentCar.Price;

            case PriceType.Discounted:
                return currentCar.Price *(decimal) 0.8;

            case PriceType.Premium:
                return currentCar.Price *(decimal) 1.4;

            case PriceType.Vip:
                return currentCar.Price *(decimal) 1.6;

            default:
                return 0;
        }
    }
}
