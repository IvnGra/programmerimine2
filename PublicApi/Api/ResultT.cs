using PublicApi.Api;

namespace PublicApi.Api
{
    public class Result<T> : Result
    {
        public T Value { get; set; } = default!;
    }
}
