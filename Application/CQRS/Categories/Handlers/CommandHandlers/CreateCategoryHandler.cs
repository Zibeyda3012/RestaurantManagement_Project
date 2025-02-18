using Application.CQRS.Categories.Command.Requests;
using Application.CQRS.Categories.Command.Responses;
using Common.GlobalResponse.Generics;
using Domain.Entities;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Categories.Handlers.CommandHandlers;

public class CreateCategoryHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCategoryRequest, ResponseModel<CreateCategoryResponse>>
{

    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ResponseModel<CreateCategoryResponse>> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        Category newCategory = new()
        {
            Name = request.Name
        };

        if (string.IsNullOrEmpty(newCategory.Name))
        {
            return new ResponseModel<CreateCategoryResponse>
            {
                Data = null,
                Errors = ["Gonderilen melumat bosh ve ya null ola bilmez"],
                isSuccess = false
            };

        }

        await _unitOfWork.CategoryRepository.AddAsync(newCategory);

        CreateCategoryResponse response = new CreateCategoryResponse()
        {
            Id = newCategory.Id,
            Name = newCategory.Name
        };

        return new ResponseModel<CreateCategoryResponse>
        {
            Data = response,
            Errors = [],
            isSuccess = true

        };

    }
}
