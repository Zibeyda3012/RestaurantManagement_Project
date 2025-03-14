using Application.CQRS.Cars.DTOs;
using AutoMapper;
using Common.Exceptions;
using Common.GlobalResponse.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Cars.Handlers;

public class Delete
{
    public class DeleteCommandRequest : IRequest<ResponseModel<DeleteCarDTO>>
    {
        public int Id { get; set; }
    }

    public sealed class Handler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCommandRequest, ResponseModel<DeleteCarDTO>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ResponseModel<DeleteCarDTO>> Handle(DeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var currentCar = await _unitOfWork.CarRepository.GetByIdAsync(request.Id);
            if (currentCar == null) { throw new BadRequestException("car with provided id doesn't exist"); }

            _unitOfWork.CarRepository.Remove(request.Id);
            await _unitOfWork.SaveChanges();

            return new ResponseModel<DeleteCarDTO>
            {
                Data = new DeleteCarDTO { Message = "Deleted sucessfully" },
                Errors = [],
                isSuccess = true
            };

        }
    }

}
