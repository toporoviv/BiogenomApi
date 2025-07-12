using BiogenomApi.Domain.Enums;
using BiogenomApi.Services.Models;
using BiogenomApi.Services.Models.Enums;

namespace BiogenomApi.Services.Dtos;

public readonly record struct GetDailyIntakeVitaminDto(Identifier UserId);

public readonly record struct DailyIntakeVitaminDto(
    string VitaminTitle,
    double CurrentIntake,
    MeasurementUnit MeasurementUnit,
    VitaminState State,
    double LowerLimit,
    double? UpperLimit); 
    
public readonly record struct GetPersonalizedDietarySupplementsDto(Identifier UserId);

public readonly record struct PersonalizedDietarySupplementsDto(
    string Title,
    byte[] Image,
    int AlternativesCount);
    
public readonly record struct GetPersonalizedDietarySupplementsRecommendationsDto(Identifier UserId);

public readonly record struct PersonalizedDietarySupplementsRecommendationsDto(
    MeasurementUnit MeasurementUnit,
    double CurrentIntake,
    double LowerLimit,
    double? UpperLimit = null,
    double? DietarySupplementIntake = null,
    double? FoodIntake = null);