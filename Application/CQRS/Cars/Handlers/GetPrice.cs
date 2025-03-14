using Application.CQRS.Cars.DTOs;
using Application.StrategyPattern;
using AutoMapper;
using Common.Exceptions;
using Common.GlobalResponse.Generics;
using Domain.Enums;
using MediatR;
using Repository.Common;
using Repository.Repositories;

namespace Application.CQRS.Cars.Handlers;

public class GetPrice
{
    public class GetPriceQueryRequest : IRequest<ResponseModel<GetPriceDTO>>
    {
        public int Id { get; set; }
        public PriceType PriceType { get; set; }
    }

    public sealed class Handler(IUnitOfWork unitOfWork, ICarContext carContext) : IRequestHandler<GetPriceQueryRequest, ResponseModel<GetPriceDTO>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICarContext _carContext = carContext;

        public async Task<ResponseModel<GetPriceDTO>> Handle(GetPriceQueryRequest request, CancellationToken cancellationToken)
        {
            var currentCar = await _unitOfWork.CarRepository.GetByIdAsync(request.Id);
            if (currentCar == null) { throw new BadRequestException("car with provided id doesn't exist"); }

            var response = new GetPriceDTO()
            {
                Id = currentCar.Id,
                Model = currentCar.Model,
                Vendor = currentCar.Vendor,
                Year = currentCar.Year,
                Price = await _carContext.Calculate(currentCar.Id, request.PriceType)
            };

            return new ResponseModel<GetPriceDTO>
            {
                Data = response,
                Errors = [],
                isSuccess = true
            };

        }
    }
}
