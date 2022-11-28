using BlazorApp1.Services;

namespace BlazorApp1.Pages
{
   public partial class Index : ComponentBase, IDisposable
   {
      public string MyCircuitMessage = "";
      public string UserRemovedMessage = "";
      public string MyUserId = "";

      CircuitHandlerService handler;

      protected override void OnInitialized()
      {
         handler = (CircuitHandlerService)BlazorCircuitHandler;
         MyCircuitMessage = $"My Circuit ID = {handler.CircuitId}";

         if(UserService.Circuits.Count == 0 )
            MyUserId = "Carl" ;
         else if(UserService.Circuits.Count == 1)
            MyUserId = "Kelly";
         else if (UserService.Circuits.Count == 2)
            MyUserId = "Doofus";

         UserService.Connect(handler.CircuitId, MyUserId);
         UserService.CircuitsChanged += UserService_CircuitsChanged;
         UserService.UserRemoved += UserService_UserRemoved;
      }

      private void UserService_UserRemoved(object sender, UserRemovedEventArgs e) 
      {
         UserRemovedMessage = $"{e.UserId} diconnected";
      }

      private void UserService_CircuitsChanged(object? sender, EventArgs e) 
      {
         InvokeAsync(StateHasChanged);
      }

      public void Dispose()
      {
         UserService.CircuitsChanged -= UserService_CircuitsChanged;
         UserService.UserRemoved -= UserService_UserRemoved;
      }
   }
}
