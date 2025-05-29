using System;

namespace PublicApi.Api
{
    public class Result<T> : Result
    {
        public T Value { get; set; }

        internal void AddError(object key, object msg)
        {
            throw new NotImplementedException();
        }

        internal void AddError(object key, object msg)
        {
            throw new NotImplementedException();
        }

        internal void AddError(object key, object msg)
        {
            throw new NotImplementedException();
        }
    }
}