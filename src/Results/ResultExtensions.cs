using System;
using TropicalCode.FunctionalHelpers.Options;

namespace TropicalCode.FunctionalHelpers.Results;

public static class ResultExtensions
{
    public static Result<TOut> Bind<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> bind)
    {
        return result.IsSuccess ? 
            bind(result.Value) : 
            Result<TOut>.Failed(result.Error);
    }
    
    public static Result<TOut> TryCatch<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> func, Error error)
    {
        try
        {
            return result.IsSuccess ? 
                Result<TOut>.Success(func(result.Value)) : 
                Result<TOut>.Failed(result.Error);
        }
        catch (Exception e)
        {
            return Result<TOut>.Failed(error with { InnerException = Options<Exception>.Some(e) });
        }
    }
    
    public static Result<TIn> Tap<TIn>(this Result<TIn> result, Action<TIn> action)
    {
        if (result.IsSuccess)
        {
            action(result.Value);
        }
        return result;
    }
    
    public static TOut Match<TIn, TOut>(this Result<TIn> result
        , Func<TIn, TOut> success
        , Func<Error, TOut> failure) => result.IsSuccess ? 
            success(result.Value) : 
            failure(result.Error);
}