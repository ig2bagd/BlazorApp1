var JSInteropWithTypeScript;
(function (JSInteropWithTypeScript) {
    class ExampleJsFunctions {
        showPrompt(message) {
            return prompt(message, 'Type anything here');
        }
    }
    function Load() {
        window['exampleJsFunctions'] = new ExampleJsFunctions();
    }
    JSInteropWithTypeScript.Load = Load;
})(JSInteropWithTypeScript || (JSInteropWithTypeScript = {}));
JSInteropWithTypeScript.Load();
//# sourceMappingURL=exampleJsInterop.js.map