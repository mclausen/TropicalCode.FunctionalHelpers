using System;

namespace TropicalCode.FunctionalHelpers.Options;

public readonly struct ValueOption<T> where T : struct
{
    private readonly T? _value;

    private ValueOption(T? value) => _value = value;
        
    public bool IsSome => _value is not null;
        
    public static ValueOption<T> Some(T value) => new(value);
    public static ValueOption<T> None() => new(default);
        
    public TOut Match<TOut>(Func<T, TOut> some, Func<TOut> none) => _value.HasValue
        ? some(_value.Value)
        : none();
}