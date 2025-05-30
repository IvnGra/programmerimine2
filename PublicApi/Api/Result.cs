using System.Collections.Generic;

public class Result
{
    public Dictionary<string, string> Errors { get; private set; } = new Dictionary<string, string>();

    public bool HasErrors => Errors.Count > 0;

    public void AddError(string key, string message)
    {
        Errors[key] = message;
    }
}
