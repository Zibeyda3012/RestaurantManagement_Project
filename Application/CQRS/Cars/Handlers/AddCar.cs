using Application.CQRS.Cars.DTOs;
using AutoMapper;
using Common.GlobalResponse;
using Common.GlobalResponse.Generics;
using Domain.Entities;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Cars.Handlers;

public class AddCar
{
    public class AddCarCommandRequest : IRequest<ResponseModel<AddCarDTO>>
    {
        public string Model { get; set; }
        public string Vendor { get; set; }
        public DateTime Year { get; set; }
        public decimal Price { get; set; }

        public AddCarCommandRequest(string model, string vendor, DateTime year, decimal price)
        {
            Model = model;
            Vendor = vendor;
            Year = year;
            Price = price;
        }
    }


    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<AddCarCommandRequest, ResponseModel<AddCarDTO>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseModel<AddCarDTO>> Handle(AddCarCommandRequest request, CancellationToken cancellationToken)
        {
            var newCar = _mapper.Map<Car>(request);
            await _unitOfWork.CarRepository.AddAsync(newCar);

            var response = _mapper.Map<AddCarDTO>(newCar);

            return new ResponseModel<AddCarDTO>
            {
                Data = response,
                Errors = [],
                isSuccess = true
            };
        }

    }
}

