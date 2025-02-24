using Application.CQRS.Users.DTOs;
using AutoMapper;
using Common.Exceptions;
using Common.GlobalResponse.Generics;
using Domain.Entities;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Users.Handlers;

public class Update
{
    public record struct Command : IRequest<ResponseModel<UpdateDTO>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<Command, ResponseModel<UpdateDTO>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseModel<UpdateDTO>> Handle(Command request, CancellationToken cancellationToken)
        {
            var currentUser = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
            if (currentUser == null)
                throw new BadRequestException("User with provided id doesn't exist");

            currentUser.Name = request.Name;
            currentUser.Surname = request.Surname;
            currentUser.Phone = request.Phone;
            currentUser.Email = request.Email;

            _unitOfWork.UserRepository.Update(currentUser);
            _unitOfWork.SaveChanges();

            var response = _mapper.Map<UpdateDTO>(currentUser);

            return new ResponseModel<UpdateDTO>
            {
                Data = response,
                Errors = [],
                isSuccess = true
            };
        }
    }
}
