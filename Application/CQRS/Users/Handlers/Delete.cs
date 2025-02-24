using Common.Exceptions;
using Common.GlobalResponse.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Users.Handlers;

public class Delete
{
    public record struct Command:IRequest<ResponseModel<Unit>>
    {
        public int Id { get; set; }

    }

    public sealed class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Command, ResponseModel<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ResponseModel<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var currentUser = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
            if (currentUser is null)
                throw new BadRequestException("User with provided id does not exist");

            _unitOfWork.UserRepository.Remove(request.Id);
            await _unitOfWork.SaveChanges();

            return new ResponseModel<Unit>
            {
                Data = Unit.Value,
                Errors = [],
                isSuccess = true
            };
        }
    }
}
