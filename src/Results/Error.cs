using System;
using TropicalCode.FunctionalHelpers.Options;

namespace TropicalCode.FunctionalHelpers.Results;

public record Error(ErrorType ErrorType, string Description, Options<Exception> InnerException)
{
    public static Error Empty => new(ErrorType.Default, string.Empty, Options<Exception>.None());
    public static Error ValidationError(string description) => new(ErrorType.ValidationError, description, Options<Exception>.None());
    public static Error DomainLogicError(string description) => new(ErrorType.DomainError, description, Options<Exception>.None());
    public static Error NotFound(string description) => new(ErrorType.NotFound, description, Options<Exception>.None());
    public static Error Problem(string description, Exception exception)
    {
        if (exception == null) 
            throw new ArgumentNullException(nameof(exception));
        
        return new Error(ErrorType.Problem, description, Options<Exception>.Some(exception));
    }

    public static Error Conflict(string description) => new(ErrorType.Conflict, description, Options<Exception>.None());
}