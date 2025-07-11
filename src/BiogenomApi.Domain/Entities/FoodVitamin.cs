using BiogenomApi.Domain.Enums;

namespace BiogenomApi.Domain.Entities;

/// <summary>
/// Связующая таблица между продуктами питания и витамина (отношение многие ко многим)
/// </summary>
public class FoodVitamin
{
    public int FoodId { get; set; }
    public Food Food { get; set; } = null!;

    public int VitaminId { get; set; }
    public Vitamin Vitamin { get; set; } = null!;
    
    /// <summary>
    /// Количество нутриентов в текущем продукте питания (Food) для Vitamin
    /// </summary>
    public required NutrientAmount Amount { get; set; }
}