using Application.CQRS.Customers.Commands.Requests;
using Application.CQRS.Customers.Commands.Responses;
using AutoMapper;
using Common.GlobalResponse.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Customers.Handlers.CommandHandlers;

//public class RegisterCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<RegisterCustomerRequest, ResponseModel<RegisterCustomerResponse>>
//{
//    private readonly IUnitOfWork _unitOfWork = unitOfWork;
//    private readonly IMapper _mapper = mapper;

//    //public Task<ResponseModel<RegisterCustomerResponse>> Handle(RegisterCustomerRequest request, CancellationToken cancellationToken)
//    //{
        
//    //}
//}
