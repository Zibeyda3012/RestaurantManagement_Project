using Application.CQRS.Categories.Queries.Requests;
using Application.CQRS.Categories.Queries.Responses;
using Common.GlobalResponse.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Categories.Handlers.QueryHandlers;

public class GetByNameCategoryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetByNameCategoryRequest, ResponseModel<GetByNameCategoryResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ResponseModel<GetByNameCategoryResponse>> Handle(GetByNameCategoryRequest request, CancellationToken cancellationToken)
    {
        var currentCategory = await _unitOfWork.CategoryRepository.GetByName(request.Name);

        if (currentCategory == null)
        {
            return new ResponseModel<GetByNameCategoryResponse>
            {
                Data = null,
                Errors = ["This category with provided name doesn't exist"],
                isSuccess = false
            };
        }


        var response = new GetByNameCategoryResponse()
        {
            Id = currentCategory.Id,
            Name = currentCategory.Name,
        };

        return new ResponseModel<GetByNameCategoryResponse>
        {
            Data = response,
            Errors = [],
            isSuccess = true

        };


    }
}
