using Application.CQRS.Products.Queries.Requests;
using Application.CQRS.Products.Queries.Responses;
using AutoMapper;
using Common.Exceptions;
using Common.GlobalResponse.Generics;
using Domain.Entities;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Products.Handlers.QueryHandlers;

public class GetByNameProductHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetByNameProductRequest, ResponseModel<GetByNameProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseModel<GetByNameProductResponse>> Handle(GetByNameProductRequest request, CancellationToken cancellationToken)
    {
        var currentProduct = await _unitOfWork.ProductRepository.GetByName(request.Name);

        if (currentProduct == null)
        {
            return new ResponseModel<GetByNameProductResponse>
            {
                Data = null,
                Errors = ["Product with provided name doesn't exist"],
                isSuccess = false
            };
        }

        var response = _mapper.Map<GetByNameProductResponse>(currentProduct);

        return new ResponseModel<GetByNameProductResponse>
        {
            Data = response,
            Errors = [],
            isSuccess = true
        };
    }
}
