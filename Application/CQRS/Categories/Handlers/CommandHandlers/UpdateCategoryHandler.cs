using Application.CQRS.Categories.Command.Requests;
using Application.CQRS.Categories.Command.Responses;
using Application.Security;
using Common.GlobalResponse.Generics;
using Domain.Entities;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Categories.Handlers.CommandHandlers;

public class UpdateCategoryHandler(IUnitOfWork unitOfWork, IUserContext userContext) : IRequestHandler<UpdateCategoryRequest, ResponseModel<UpdateCategoryResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserContext _userContext = userContext;

    public async Task<ResponseModel<UpdateCategoryResponse>> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {

        Category updatedCategory = new()
        {
            Id = request.Id,
            Name = request.Name,

        };

        if (string.IsNullOrEmpty(updatedCategory.Name))
        {
            return new ResponseModel<UpdateCategoryResponse>()
            {
                Data = null,
                Errors = ["Data can't be null or empty"],
                isSuccess = false
            };
        }
        updatedCategory.UpdatedBy = _userContext.MustGetUserId();

        await _unitOfWork.CategoryRepository.Update(updatedCategory);

        var response = new UpdateCategoryResponse()
        {
            Id = updatedCategory.Id,
            Name = updatedCategory.Name
        };

        return new ResponseModel<UpdateCategoryResponse>
        {
            Data = response,
            Errors = [],
            isSuccess = true
        };


    }
}
