using System.Collections.Generic;

namespace PublicApi.Api
{
    // Base Result class without value but error info
    public class Result
    {
        // Store errors as dictionary with key and message
        public Dictionary<string, string> Errors { get; } = new Dictionary<string, string>();

        public bool HasErrors => Errors.Count > 0;

        // Add or update error by key
        public void AddError(object key, object value)
        {
            var keyStr = key?.ToString() ?? "";
            var valueStr = value?.ToString() ?? "";

            if (Errors.ContainsKey(keyStr))
                Errors[keyStr] = valueStr;
            else
                Errors.Add(keyStr, valueStr);
        }
    }
}

