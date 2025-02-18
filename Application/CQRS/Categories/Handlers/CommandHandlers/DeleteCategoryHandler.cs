using Application.CQRS.Categories.Command.Requests;
using Application.CQRS.Categories.Command.Responses;
using Common.GlobalResponse.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Categories.Handlers.CommandHandlers;

class DeleteCategoryHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCategoryRequest, ResponseModel<DeleteCategoryResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ResponseModel<DeleteCategoryResponse>> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
    {
        await _unitOfWork.CategoryRepository.Remove(request.Id, 0);

        return new ResponseModel<DeleteCategoryResponse>
        {
            Data = new DeleteCategoryResponse { Message = "Deleted Successfully!" },
            Errors = [],
            isSuccess = true
        };
    }
}
