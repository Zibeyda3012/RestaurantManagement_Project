using Application.CQRS.Categories.Command.Requests;
using Application.CQRS.Categories.Command.Responses;
using Common.GlobalResponse.Generics;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Categories.Handlers.CommandHandlers;

// demeli biz handler icerisinde validatoru avtomatik isletmek ucun pipeline behaviour istifade ede bilerik.

public class CreateCategoryHandler(IUnitOfWork unitOfWork/*, IValidator<CreateCategoryRequest> validator*/) : IRequestHandler<CreateCategoryRequest, ResponseModel<CreateCategoryResponse>>
{

    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    //// ozumuz manual yazsaq 
    ///
    //private readonly IValidator<CreateCategoryRequest> _validator = validator;

    public async Task<ResponseModel<CreateCategoryResponse>> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        //var validationResult = await _validator.ValidateAsync(request);

        //if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);


        Category newCategory = new()
        {
            Name = request.Name
        };


        ////validation yoxlayir deye burda yoxlamaga ehtiyac olmadi(bu kohne koddu)

        //if (string.IsNullOrEmpty(newCategory.Name))
        //{
        //    return new ResponseModel<CreateCategoryResponse>
        //    {
        //        Data = null,
        //        Errors = ["Gonderilen melumat bosh ve ya null ola bilmez"],
        //        isSuccess = false
        //    };

        //}

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
