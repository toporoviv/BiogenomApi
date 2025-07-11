namespace BiogenomApi.Domain.Entities;

/// <summary>
/// Изображение БАДа
/// </summary>
public class DietarySupplementImage
{
    public int Id { get; init; }
    
    /// <summary>
    /// Картинка
    /// </summary>
    public required byte[] Data { get; init; }
    
    public int DietarySupplementId { get; set; }
    public DietarySupplement DietarySupplement { get; set; } = null!;
}