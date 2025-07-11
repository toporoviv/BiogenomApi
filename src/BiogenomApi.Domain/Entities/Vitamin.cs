using BiogenomApi.Domain.Enums;

namespace BiogenomApi.Domain.Entities;

/// <summary>
/// Витамин
/// </summary>
public abstract class Vitamin
{
    public int Id { get; init; }
    
    /// <summary>
    /// Название
    /// </summary>
    public required string Title { get; init; }
    
    /// <summary>
    /// Единицы измерения (мкг, мг, г)
    /// </summary>
    public MeasurementUnit MeasurementUnit { get; init; }
    
    /// <summary>
    /// Нижняя граница
    /// </summary>
    /// <remarks>
    /// Если целевое значение меньше, то это дефицит витамина
    /// </remarks>
    public double LowerLimit { get; init; }
    
    /// <summary>
    /// Верхняя граница
    /// </summary>
    /// <remarks>
    /// Если UpperLimit != null, то целевое значение должно находиться между LowerValue и UpperValue,
    /// иначе это не считается нормой
    /// </remarks>
    public double? UpperLimit { get; init; }
    
    /// <summary>
    /// Описание важности данного витамина для здоровья
    /// </summary>
    public required string ImportanceForHealth { get; init; }
    
    /// <summary>
    /// Описание проявления дефицита
    /// </summary>
    public string? ScarcityManifestation { get; init; }
    
    /// <summary>
    /// Описание профилактики
    /// </summary>
    public string? Prevention { get; init; }

    /// <summary>
    /// Информация по БАДам, которые могут восполнить дефицит текущего витамина
    /// </summary>
    public List<VitaminDietarySupplement> RelatedSupplements { get; set; } = [];
}