using Application.CQRS.Products.Queries.Responses;
using Common.GlobalResponse.Generics;
using MediatR;

namespace Application.CQRS.Products.Queries.Requests;

public class GetByNameProductRequest:IRequest<ResponseModel<GetByNameProductResponse>>
{
    public string Name { get; set; }
}
