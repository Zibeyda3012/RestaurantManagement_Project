using Application.CQRS.Cars.DTOs;
using AutoMapper;
using Common.Exceptions;
using Common.GlobalResponse.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Cars.Handlers;

public class GetById
{
    public class GetByIdQuery : IRequest<ResponseModel<GetByIdDTO>>
    {
        public int Id { get; set; }
    }

    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetByIdQuery, ResponseModel<GetByIdDTO>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseModel<GetByIdDTO>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var currentCar = await _unitOfWork.CarRepository.GetByIdAsync(request.Id);
            if (currentCar == null) { throw new BadRequestException("car with provided id doesn't exist"); }

            var response = _mapper.Map<GetByIdDTO>(currentCar);

            return new ResponseModel<GetByIdDTO> { Data = response, Errors = [], isSuccess = true };

        }
    }

}
