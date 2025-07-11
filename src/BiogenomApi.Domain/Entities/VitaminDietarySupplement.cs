using BiogenomApi.Domain.Enums;

namespace BiogenomApi.Domain.Entities;

/// <summary>
/// Связующая таблица между витаминами и БАДами
/// </summary>
public class VitaminDietarySupplement
{
    public int VitaminId { get; set; }
    public Vitamin Vitamin { get; set; } = null!;

    public int DietarySupplementId { get; set; }
    public DietarySupplement DietarySupplement { get; set; } = null!;
    
    /// <summary>
    /// Содержание нутриентов текущего БАДа для конкретного витамина
    /// </summary>
    public required NutrientAmount Amount { get; init; }
}