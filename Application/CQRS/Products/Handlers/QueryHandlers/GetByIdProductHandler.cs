using Application.CQRS.Products.Queries.Requests;
using Application.CQRS.Products.Queries.Responses;
using AutoMapper;
using Common.Exceptions;
using Common.GlobalResponse.Generics;
using Domain.Entities;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Products.Handlers.QueryHandlers;

public class GetByIdProductHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetByIdProductRequest, ResponseModel<GetByIdProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseModel<GetByIdProductResponse>> Handle(GetByIdProductRequest request, CancellationToken cancellationToken)
    {
        var currentProduct = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);

        if (currentProduct == null) throw new NotFoundException(typeof(Product), request.Id);

        var response = _mapper.Map<GetByIdProductResponse>(currentProduct);

        return new ResponseModel<GetByIdProductResponse>
        {
            Data = response,
            Errors = [],
            isSuccess = true
        };

    }
}
