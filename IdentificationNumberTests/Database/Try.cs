using Polly;

namespace IdentificationNumberTests.Database;

internal static class Try
{
    public static async Task<T> ExecuteAsync<T>
        (Func<CancellationToken, Task<T>> action, Func<T, bool> isAcceptable, int retryCount = 3)
    {
        return await Policy.HandleResult<T>(result =>
        {
            return !isAcceptable(result);
        })
        .WaitAndRetryAsync(retryCount, retryAttempt =>
        {
            return TimeSpan.FromSeconds(Math.Pow(2, retryCount));
        })
        .ExecuteAsync(async ct =>
        {
            return await action(ct);
        }, CancellationToken.None);
    }
}
