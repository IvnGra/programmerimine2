using System;

namespace PublicApi.Api
{
    public class Result
    {
        public string Error { get; set; }

        public bool HasError
        {
            get
            {
                return !string.IsNullOrEmpty(Error);
            }
        }

        internal void AddError(object key, object value)
        {
            throw new NotImplementedException();
        }
    }
}
