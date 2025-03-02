using Application.CQRS.Products.Queries.Responses;
using Common.GlobalResponse.Generics;
using MediatR;

namespace Application.CQRS.Products.Queries.Requests;

public class GetByIdProductRequest:IRequest<ResponseModel<GetByIdProductResponse>>
{
    public int Id { get; set; }
}
