using Application.CQRS.Users.DTOs;
using AutoMapper;
using Common.GlobalResponse.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Users.Handlers;

//Handler ve Request eyni yerde (bir class daxilinde olur)
public class GetById
{
    public class Query : IRequest<ResponseModel<GetByIdDTO>>
    {
        public int Id { get; set; }

     
    }

    public sealed class Handler : IRequestHandler<Query, ResponseModel<GetByIdDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel<GetByIdDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var currentUser = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);

            if (currentUser == null)
            {
                return new ResponseModel<GetByIdDTO>
                {
                    Data = null,
                    Errors = ["User doesn't exist"],
                    isSuccess = false,
                };
            }

            ////method 1 (ozumuz manual maplayirik)

            //var response = new GetByIdDTO()
            //{
            //    Id = currentUser.Id,
            //    Email = currentUser.Email,
            //    Name = currentUser.Name,
            //    Surname = currentUser.Surname,
            //    Phone = currentUser.Phone, 
            //};



            //method 2(Auto mapper)

            var response = _mapper.Map<GetByIdDTO>(currentUser);

            return new ResponseModel<GetByIdDTO>
            {
                Data = response,
                Errors = [],
                isSuccess = true,
            };
        
        

        }
    }
}
