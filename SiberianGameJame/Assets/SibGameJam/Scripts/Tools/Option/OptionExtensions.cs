using System;

namespace Tools
{
    public static class OptionExtensions
    {
        public static T2 Match<T1, T2>(this Option<T1> option,
            Func<T1, T2> onIsSome, Func<T2> onIsNone) =>
            option.IsSome(out var value) ? onIsSome(value) : onIsNone();
    
        public static Option<T2> Bind<T1, T2>(this Option<T1> option, 
            Func<T1, Option<T2>> binder) =>
            option.Match(binder, () => Option<T2>.None);

        public static Option<T2> Map<T1, T2>(this Option<T1> option, 
            Func<T1, T2> mapper) =>
            option.Bind(value => Option<T2>.Some(mapper(value)));
    }
}