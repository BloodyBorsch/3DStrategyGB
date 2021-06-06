using System.Runtime.CompilerServices;


namespace Utils
{
    public interface IAwaiter<TResult> : INotifyCompletion
    {
        bool IsCompleted { get; }
        TResult GetResult();
    }
}