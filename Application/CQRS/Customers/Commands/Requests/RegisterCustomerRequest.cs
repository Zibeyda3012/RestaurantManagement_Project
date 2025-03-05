using Application.CQRS.Customers.Commands.Responses;
using Common.GlobalResponse.Generics;
using MediatR;

namespace Application.CQRS.Customers.Commands.Requests;

public class RegisterCustomerRequest : IRequest<ResponseModel<RegisterCustomerResponse>>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
}
