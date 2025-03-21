﻿using Application.CQRS.Categories.Command.Responses;
using Common.GlobalResponse.Generics;
using MediatR;

namespace Application.CQRS.Categories.Command.Requests;

public class CreateCategoryRequest:IRequest<ResponseModel<CreateCategoryResponse>>
{
    public string Name { get; set; }    
}
