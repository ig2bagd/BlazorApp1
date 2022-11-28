namespace BlazorApp1.Services;

// https://www.youtube.com/watch?v=YWYUIjWKh7w
public interface ICircuitUserService
{
   ConcurrentDictionary<string, CircuitUser> Circuits { get; }
   event EventHandler CircuitsChanged;
   event UserRemovedEventHandler UserRemoved;

   void Connect(string CircuitId, string UserId);
   void Disconnect(string CircuitId);
}
