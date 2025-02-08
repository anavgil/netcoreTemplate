using FluentResults;

namespace Api.Extensions;

/// <summary>
/// 
/// </summary>
public static class ResultExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOut"></typeparam>
    /// <param name="result"></param>
    /// <param name="onSuccess"></param>
    /// <param name="onFailure"></param>
    /// <returns></returns>
    public static TOut Match<TOut>(
        this Result result,
        Func<TOut> onSuccess,
        Func<Result, TOut> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <param name="result"></param>
    /// <param name="onSuccess"></param>
    /// <param name="onFailure"></param>
    /// <returns></returns>
    public static TOut Match<TIn, TOut>(
        this IResult<TIn> result,
        Func<TIn, TOut> onSuccess,
        Func<IResult<TIn>, TOut> onFailure)
    {
        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result);
    }
}
