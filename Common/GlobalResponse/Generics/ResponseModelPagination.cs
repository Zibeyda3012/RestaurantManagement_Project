using Common.GlobalResponse.Generics;

namespace Common.GlobalResponse;

public class ResponseModelPagination<T> : ResponseModel<T>
{
    public Pagination<T> Data { get; set; }

    public ResponseModelPagination(List<string> messages) : base(messages)
    {
    }
    public ResponseModelPagination()
    {
    }
}
