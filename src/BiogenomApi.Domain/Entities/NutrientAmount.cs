using BiogenomApi.Domain.Enums;

namespace BiogenomApi.Domain.Entities;

/// <summary>
/// Количество нутриентов
/// </summary>
public class NutrientAmount
{
    /// <summary>
    /// Значение
    /// </summary>
    public double Value { get; init; }
    
    /// <summary>
    /// Единицы измерения (мкг, мг, г)
    /// </summary>
    public MeasurementUnit Unit { get; init; }
}