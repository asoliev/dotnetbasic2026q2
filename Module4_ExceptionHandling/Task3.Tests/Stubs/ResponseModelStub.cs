using System.Collections.Generic;
using Task3.DoNotChange;

namespace Task3.Tests.Stubs;

internal class ResponseModelStub : IResponseModel
{
    private readonly IDictionary<string, string> _data = new Dictionary<string, string>();

    public void AddAttribute(string key, string value) => _data.Add(key, value);

    public string GetAttribute(string key) => _data[key];

    public string GetActionResult() => _data.TryGetValue("action_result", out string result) ? result : null;
}
