using Microsoft.JSInterop;

namespace CaptainsLog.Js;

public class Alerts : JSInterop {
    public Alerts(IJSRuntime js) : base(js) {}

    public async Task Alert(string message) {
        await JavaScript.InvokeVoidAsync("alert", message);
    }

    public async Task<bool> Confirm(string message) {
        return await JavaScript.InvokeAsync<bool>("confirm", message);
    }
}