using System;
using UnityEngine;

[System.Serializable]
public struct Option<T>
{
    [SerializeField] private bool hasValue;
    [SerializeField] private T value;

    public bool HasValue => hasValue;
    public T Value => value;

    public Option(T value)
    {
        hasValue = value != null;
        this.value = value;
    }

    public bool IsSome(out T some)
    {
        some = Value;
        return HasValue;
    }
    
    public static Option<T> None => new(default);
    public static Option<T> Some(T value) => new(value);

    public override string ToString() => $"HasValue: {HasValue} Value: {Value}";
}