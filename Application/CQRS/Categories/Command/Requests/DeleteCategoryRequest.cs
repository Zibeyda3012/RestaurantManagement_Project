using Application.CQRS.Categories.Command.Responses;
using Common.GlobalResponse.Generics;
using MediatR;

namespace Application.CQRS.Categories.Command.Requests;

public class DeleteCategoryRequest : IRequest<ResponseModel<DeleteCategoryResponse>>
{
    public int Id { get; set; }
}
