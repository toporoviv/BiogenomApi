using BiogenomApi.Domain.Enums;

namespace BiogenomApi.Domain.Entities;

public class User
{
    public int Id { get; init; }
    public required string FirstName { get; init; }
    public required string Email { get; init; }
    public DateTime Birthday { get; init; }
    public Gender Gender { get; init; } 
    
    /// <summary>
    /// Информация по пройденным опросам
    /// </summary>
    public List<UserVitaminSurvey> VitaminSurveys { get; set; } = [];
}