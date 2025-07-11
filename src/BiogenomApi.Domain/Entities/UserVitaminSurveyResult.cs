namespace BiogenomApi.Domain.Entities;

/// <summary>
/// Результат опроса
/// </summary>
public class UserVitaminSurveyResult
{
    public int VitaminId { get; set; }
    public Vitamin Vitamin { get; set; } = null!;

    public int UserVitaminSurveyId { get; set; }
    public UserVitaminSurvey UserVitaminSurvey { get; set; } = null!;

    /// <summary>
    /// Количество нутриентов для текущего витамина 
    /// </summary>
    public required NutrientAmount Amount { get; set; }
}