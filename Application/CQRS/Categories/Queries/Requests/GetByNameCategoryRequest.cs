using Application.CQRS.Categories.Queries.Responses;
using Common.GlobalResponse;
using Common.GlobalResponse.Generics;
using MediatR;

namespace Application.CQRS.Categories.Queries.Requests;

public class GetByNameCategoryRequest:IRequest<ResponseModel<GetByNameCategoryResponse>>
{
    public string Name { get; set; }    
}

