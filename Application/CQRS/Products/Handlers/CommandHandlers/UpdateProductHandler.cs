using Application.CQRS.Products.Commands.Requests;
using Application.CQRS.Products.Commands.Responses;
using AutoMapper;
using Common.GlobalResponse.Generics;
using Domain.Entities;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Products.Handlers.CommandHandlers;

public class UpdateProductHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateProductRequest, ResponseModel<UpdateProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseModel<UpdateProductResponse>> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var mappedProduct = _mapper.Map<Product>(request);

        if (string.IsNullOrEmpty(mappedProduct.Name))
        {
            return new ResponseModel<UpdateProductResponse>
            {
                Data = null,
                Errors = ["Data can not be null or empty"],
                isSuccess = false
            };
        }

        await _unitOfWork.ProductRepository.Update(mappedProduct);
        var response = _mapper.Map<UpdateProductResponse>(mappedProduct);

        return new ResponseModel<UpdateProductResponse>
        {
            Data = response,
            Errors = [],
            isSuccess = true
        };
    }
}
