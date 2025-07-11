namespace BiogenomApi.Services.Models;

public readonly record struct Identifier
{
    public int Value { get; init; }
    
    public Identifier(int value)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value);

        Value = value;
    }
    
    public static implicit operator Identifier(int id) => new(id);
}