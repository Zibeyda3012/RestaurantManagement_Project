using Application.CQRS.Products.Queries.Requests;
using Application.CQRS.Products.Queries.Responses;
using AutoMapper;
using Common.GlobalResponse;
using Common.GlobalResponse.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Products.Handlers.QueryHandlers;

public class GetAllProductHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllProductRequest, ResponseModelPagination<GetAllProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseModelPagination<GetAllProductResponse>> Handle(GetAllProductRequest request, CancellationToken cancellationToken)
    {
        var products = _unitOfWork.ProductRepository.GetAll();
        if (!products.Any())
        {
            return new ResponseModelPagination<GetAllProductResponse>
            {
                Data = null,
                Errors = [],
                isSuccess = true
            };
        }

        var totalCount = products.Count();
        products = products.Skip((request.Page - 1) * request.Limit).Take(request.Limit);

        var mappedProducts = new List<GetAllProductResponse>();

        foreach (var product in products)
        {
            var mappedProduct = _mapper.Map<GetAllProductResponse>(product);
            mappedProducts.Add(mappedProduct);
        }

        var response = new Pagination<GetAllProductResponse>() { Data = mappedProducts, TotalDataCount = totalCount };

        return new ResponseModelPagination<GetAllProductResponse>
        {
            Data = response,
            Errors = []
        };
    }
}
