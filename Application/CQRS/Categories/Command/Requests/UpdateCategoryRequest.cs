using Application.CQRS.Categories.Command.Responses;
using Common.GlobalResponse.Generics;
using MediatR;

namespace Application.CQRS.Categories.Command.Requests;

public class UpdateCategoryRequest:IRequest<ResponseModel<UpdateCategoryResponse>>
{
    public int Id { get; set; } 
    public string Name { get; set; }
}
