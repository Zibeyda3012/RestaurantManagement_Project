using Application.CQRS.Categories.Command.Requests;
using MediatR;

namespace Application.CQRS.Categories.Handlers.CommandHandlers;

public class CreateCategoryHandler:IRequestHandler<CreateCategoryRequest,ResponseModel>
{
}
