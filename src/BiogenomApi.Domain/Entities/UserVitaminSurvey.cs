namespace BiogenomApi.Domain.Entities;

/// <summary>
/// Связующая таблица между пользователем и результатом опроса
/// </summary>
public class UserVitaminSurvey
{
    public int Id { get; init; }
    
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    public DateTime SurveyAtUtc { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Результаты опросов пользователя
    /// </summary>
    public List<UserVitaminSurveyResult> Results { get; set; } = [];
}