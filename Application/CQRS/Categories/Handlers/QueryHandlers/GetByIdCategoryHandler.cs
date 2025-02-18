using Application.CQRS.Categories.Queries.Requests;
using Application.CQRS.Categories.Queries.Responses;
using Common.GlobalResponse.Generics;
using Domain.Entities;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Categories.Handlers.QueryHandlers;

public class GetByIdCategoryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetByIdCategoryRequest, ResponseModel<GetByIdCategoryResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<ResponseModel<GetByIdCategoryResponse>> Handle(GetByIdCategoryRequest request, CancellationToken cancellationToken)
    {
        Category currentCategory = await _unitOfWork.CategoryRepository.GetByIdAsyns(request.Id);

        if (currentCategory is null)
        {
            return new ResponseModel<GetByIdCategoryResponse>
            {
                Data = null,
                Errors = ["This category with provided id doesn't exist"],
                isSuccess = false
            };

        }

        GetByIdCategoryResponse response = new()
        {
            Id = currentCategory.Id,
            CreatedDate = currentCategory.CreatedDate,
            Name = currentCategory.Name
        };

        return new ResponseModel<GetByIdCategoryResponse>
        {
            Data = response,
            Errors = [],
            isSuccess = true
        };

    }

}
