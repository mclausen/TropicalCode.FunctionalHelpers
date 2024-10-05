using System;

namespace TropicalCode.FunctionalHelpers.Options
{
    public class Options<T> where T : class
    {
        private readonly T? _value;

        private Options(T? value) => _value = value;
        
        public bool IsSome => _value is not null;
        
        public static Options<T> Some(T value) => new(value);
        public static Options<T> None() => new(default);

        public Options<TOut> Map<TOut>(Func<T, TOut> map) where TOut : class => _value is not null
            ? Options<TOut>.Some(map(_value))
            : Options<TOut>.None();
        
        public Options<TOut> Bind<TOut>(Func<T, Options<TOut>> bind) where TOut : class => _value is not null
            ? bind(_value)
            : Options<TOut>.None();
        
        public TOut Match<TOut>(Func<T, TOut> some, Func<TOut> none) => _value is not null
            ? some(_value)
            : none();
        
        public Options<T> Filter(Func<T, bool> predicate) => _value is not null && predicate(_value)
            ? this
            : None();
    }
}