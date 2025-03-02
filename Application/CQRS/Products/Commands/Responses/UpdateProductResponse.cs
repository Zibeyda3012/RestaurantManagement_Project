﻿namespace Application.CQRS.Products.Commands.Responses;

public class UpdateProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
