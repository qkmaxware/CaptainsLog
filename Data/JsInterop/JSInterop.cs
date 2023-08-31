using Microsoft.JSInterop;

namespace CaptainsLog.Js;

public abstract class JSInterop {
    protected IJSRuntime JavaScript {get; private set;}

    public JSInterop(IJSRuntime js) {
        this.JavaScript = js;
    }
} 