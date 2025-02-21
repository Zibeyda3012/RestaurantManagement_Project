using Application.CQRS.Users.DTOs;
using AutoMapper;
using Common.Exceptions;
using Common.GlobalResponse.Generics;
using Common.Security;
using Domain.Entities;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Users.Handlers;

public class Register
{
    public record struct Command : IRequest<ResponseModel<RegisterDTO>>
    {
        public Command(string name, string surname, string email, string phone, string password)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Phone = phone;
            Password = password;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }

    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<Command, ResponseModel<RegisterDTO>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseModel<RegisterDTO>> Handle(Command request, CancellationToken cancellationToken)
        {
            var currentUser = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
            if (currentUser != null) throw new BadRequestException("User with provided email already exists");

            var user = _mapper.Map<User>(request);

            var hashPassword = PasswordHasher.ComputePasswordToSha256Has(request.Password); 
            user.PasswordHash = hashPassword;
            user.CreatedBy = 1;

            await _unitOfWork.UserRepository.RegisterAsync(user);
            await _unitOfWork.SaveChanges();


            var response = _mapper.Map<RegisterDTO>(user);

            return new ResponseModel<RegisterDTO>
            {
                Data = response,
                Errors = [],
                isSuccess = true
            };
        }
    }
}
