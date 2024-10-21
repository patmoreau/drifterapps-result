namespace DrifterApps.Seeds.FluentResult;

public sealed partial record Result<T>
{
    /// <summary>
    ///     Executes the next function if the result is successful and returns a result of type <typeparamref name="TOut" />.
    /// </summary>
    /// <typeparam name="TOut">The type of the result.</typeparam>
    /// <param name="next">The function to execute if the result is successful.</param>
    /// <returns>The result of the next function or a failure result.</returns>
    public Result<TOut> OnSuccess<TOut>(Func<T, Result<TOut>> next) =>
        IsSuccess ? next(Value) : Result<TOut>.Failure(Error);

    /// <summary>
    ///     Executes the next function if the result is a failure.
    /// </summary>
    /// <param name="next">The function to execute if the result is a failure.</param>
    /// <returns>The result of the next function or the initial result.</returns>
    public Result<T> OnFailure(Func<ResultError, Result<T>> next) => IsFailure ? next(Error) : this;

    /// <summary>
    ///     Matches the result to the appropriate function based on success or failure and returns a result of type
    ///     <typeparamref name="TOut" />.
    /// </summary>
    /// <typeparam name="TOut">The type of the result.</typeparam>
    /// <param name="onSuccess">The function to execute if the result is successful.</param>
    /// <param name="onFailure">The function to execute if the result is a failure.</param>
    /// <returns>The result of the appropriate function.</returns>
    public Result<TOut> Switch<TOut>(Func<T, Result<TOut>> onSuccess, Func<ResultError, Result<TOut>> onFailure) =>
        IsSuccess ? onSuccess(Value) : onFailure(Error);
}