using Microsoft.JSInterop;

namespace CaptainsLog.Js;

public class LocalStorage : JSInterop {
    public LocalStorage(IJSRuntime js) : base(js) {}

    System.Text.Json.JsonSerializerOptions jsonOptions = new System.Text.Json.JsonSerializerOptions{
        IncludeFields = true
    };

    public async Task SaveAs<T>(string name, T obj) {
        await JavaScript.InvokeVoidAsync("window.localStorage.setItem", name, System.Text.Json.JsonSerializer.Serialize(obj, options: jsonOptions));
    }

    public async Task<T> Load<T>(string name) {
        var obj = await JavaScript.InvokeAsync<string>("window.localStorage.getItem", name);
        if (!string.IsNullOrEmpty(obj)) {
            try {
                var parsed = System.Text.Json.JsonSerializer.Deserialize<T>(obj, options: jsonOptions);
                if (!ReferenceEquals(parsed, null)) {
                    return parsed;
                } else {
                    throw new Exception("Parsed value was null");
                }
            } catch {
                throw;
            }
        } else {
            throw new Exception($"No item with name '{name}' in local storage");
        }
    }
} 