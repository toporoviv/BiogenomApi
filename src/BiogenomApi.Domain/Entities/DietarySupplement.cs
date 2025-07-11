namespace BiogenomApi.Domain.Entities;

/// <summary>
/// БАД
/// </summary>
public class DietarySupplement
{
    public int Id { get; init; }
    
    /// <summary>
    /// Название
    /// </summary>
    public required string Title { get; init; }
    
    /// <summary>
    /// Описание
    /// </summary>
    public required string Description { get; init; }
    
    /// <summary>
    /// Способ применения
    /// </summary>
    public required string Application { get; init; }
    
    /// <summary>
    /// Изображения БАДА
    /// </summary>
    public List<DietarySupplementImage> Images { get; set; } = [];
    
    /// <summary>
    /// Витамины, которые может восполнить этот БАД
    /// </summary>
    public List<VitaminDietarySupplement> RelatedVitamins { get; set; } = [];
}