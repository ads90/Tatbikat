using System.Threading.Tasks;

namespace Tatbikat.UI.Interfaces
{
    /// <summary>
    /// Implement in screens where if you want to make it Modal Page, and will 'await it' to return a result of type <see cref="TResult"/>
    /// </summary>
    /// <typeparam name="TResult">The type the screen will evantually return.</typeparam>
    public interface ICallbackEnabledScreen<TResult>
    {
        Task<TResult> Wait();
    }
}
