using Application.CQRS.Products.Commands.Responses;
using Common.GlobalResponse.Generics;
using MediatR;

namespace Application.CQRS.Products.Commands.Requests;

public class CreateProductRequest:IRequest<ResponseModel<CreateProductResponse>>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}
