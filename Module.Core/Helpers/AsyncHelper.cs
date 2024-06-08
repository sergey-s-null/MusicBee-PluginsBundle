namespace Module.Core.Helpers;

public static class AsyncHelper
{
    public static T Synchronize<T>(Func<Task<T>> asyncFunc)
    {
        return Task.Run(async () => await asyncFunc()).Result;
    }
}