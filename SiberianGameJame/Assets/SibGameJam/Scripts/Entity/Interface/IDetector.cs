using System.Collections.Generic;
using System.Linq;

public interface IDetector<T>
{
    Option<T> Detected { get; set; }
    Option<IEnumerable<T>> DetectedEnumerable { get; set; }

    void Handle();

    bool Contains(T value) => DetectedEnumerable.IsSome(out var some) && some.Contains(value);
    
    void Reset()
    {
        Detected = Option<T>.None;
        DetectedEnumerable = Option<IEnumerable<T>>.None;
    }
}