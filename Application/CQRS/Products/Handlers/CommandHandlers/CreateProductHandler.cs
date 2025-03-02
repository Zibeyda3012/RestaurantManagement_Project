using Application.CQRS.Products.Commands.Requests;
using Application.CQRS.Products.Commands.Responses;
using AutoMapper;
using Common.GlobalResponse.Generics;
using Domain.Entities;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Products.Handlers.CommandHandlers;

public class CreateProductHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateProductRequest, ResponseModel<CreateProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseModel<CreateProductResponse>> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var mappedProduct = _mapper.Map<Product>(request);
        await _unitOfWork.ProductRepository.AddAsync(mappedProduct);

        var response = _mapper.Map<CreateProductResponse>(mappedProduct);

        return new ResponseModel<CreateProductResponse>
        {
            Data = response,
            Errors = [],
            isSuccess = true
        };
    }
}
