using Application.CQRS.Categories.Queries.Responses;
using Common.GlobalResponse;
using MediatR;

namespace Application.CQRS.Categories.Queries.Requests;

public sealed class GetAllCategoryRequest:IRequest<ResponseModelPagination<GetAllCategoryResponse>>
{
    public int Limit { get; set; } = 10;
    public int Page { get; set; } = 1;
}
