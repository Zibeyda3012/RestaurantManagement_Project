using Application.CQRS.Products.Queries.Responses;
using Common.GlobalResponse;
using MediatR;

namespace Application.CQRS.Products.Queries.Requests;

public class GetAllProductRequest:IRequest<ResponseModelPagination<GetAllProductResponse>>
{
    public int Limit { get; set; } = 10;
    public int Page { get; set; } = 1;
}
