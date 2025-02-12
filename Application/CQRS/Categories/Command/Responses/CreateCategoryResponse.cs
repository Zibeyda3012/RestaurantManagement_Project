using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Application.CQRS.Categories.Command.Responses;

public class CreateCategoryResponse
{
    public int Id { get; set; }

    public string Name { get; set; }    
}
