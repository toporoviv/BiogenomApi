namespace BiogenomApi.Domain.Entities;

/// <summary>
/// Продукт питания
/// </summary>
public class Food
{
    public int Id { get; init; }
    
    /// <summary>
    /// Название
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Описание
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Витамины, которые содержатся в данном продукте
    /// </summary>
    public List<FoodVitamin> Vitamins { get; set; } = [];
}