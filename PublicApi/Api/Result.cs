using System.Collections.Generic;

namespace PublicApi.Api
{
    public class Result
    {
        public Dictionary<string, List<string>> Errors { get; }

        public bool HasErrors => Errors.Count > 0;

        public Result()
        {
            Errors = new Dictionary<string, List<string>>();
        }

        public void AddError(string key, string message)
        {
            if (!Errors.ContainsKey(key))
                Errors[key] = new List<string>();

            Errors[key].Add(message);
        }
    }
}
