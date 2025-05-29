using System;
using System.Collections.Generic;

namespace PublicApi.Api
{
    public class Result<T>
    {
        public T Value { get; set; }
        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();

        public void AddError(string key, string message)
        {
            Errors[key] = message;
        }
    }

}