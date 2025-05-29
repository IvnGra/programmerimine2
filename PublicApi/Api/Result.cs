using System;
using System.Collections.Generic;

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

        public static explicit operator Result(Result<User> v)
        {
            throw new NotImplementedException();
        }

        public static explicit operator Result(Result<List<User>> v)
        {
            throw new NotImplementedException();
        }
    }
}
