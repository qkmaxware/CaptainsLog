using Microsoft.JSInterop;

namespace CaptainsLog.Js;

public class LocalStorage : JSInterop {
    public LocalStorage(IJSRuntime js) : base(js) {}

    public async Task SaveAs<T>(string name, T obj) {
        await JavaScript.InvokeVoidAsync("window.localStorage.setItem", name, System.Text.Json.JsonSerializer.Serialize(obj));
    }

    public async Task<T> Load<T>(string name) {
        var obj = await JavaScript.InvokeAsync<string>("window.localStorage.getItem", name);
        if (!string.IsNullOrEmpty(obj)) {
            try {
                var parsed = System.Text.Json.JsonSerializer.Deserialize<T>(obj);
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