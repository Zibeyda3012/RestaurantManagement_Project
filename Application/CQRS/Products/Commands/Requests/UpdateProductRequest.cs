using Application.CQRS.Products.Commands.Responses;
using Common.GlobalResponse.Generics;
using MediatR;

namespace Application.CQRS.Products.Commands.Requests;

public class UpdateProductRequest:IRequest<ResponseModel<UpdateProductResponse>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
