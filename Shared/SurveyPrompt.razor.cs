namespace BlazorApp1.Shared
{
   public partial class SurveyPrompt : IAsyncDisposable
   {
      // Demonstrates how a parent component can supply parameters
      [Parameter]
      public string? Title { get; set; }

      [Inject]
      public IJSRuntime JS { get; set; }

      // We can use this to call the methods defined in the .ts class
      IJSObjectReference? javascript;

      protected async Task ShowAlert()
      {
         string msg = $"Hello from JavaScript at {DateTime.Now.ToLongTimeString()}";
         await javascript!.InvokeVoidAsync("log", msg);

         string name = await javascript!.InvokeAsync<string>("displayPrompt", "What is your name?");
         await javascript!.InvokeVoidAsync("displayAlert", $"Hello {name}!");
      }

      protected override async Task OnAfterRenderAsync(bool firstRender)
      {
         // https://docs.microsoft.com/en-us/aspnet/core/blazor/javascript-interoperability
         if (firstRender)
         {
            var jsmodule = await JS.InvokeAsync<IJSObjectReference>("import", "./Shared/SurveyPrompt.razor.js");
            javascript = await jsmodule!.InvokeAsync<IJSObjectReference>("GetInstance");
            await jsmodule.DisposeAsync();
         }
      }

      public async ValueTask DisposeAsync()
      {
         if (javascript != null) 
            await javascript.DisposeAsync();
      }
   }
}
