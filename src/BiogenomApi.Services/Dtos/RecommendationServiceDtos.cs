using BiogenomApi.Domain.Entities;
using BiogenomApi.Domain.Enums;

namespace BiogenomApi.Services.Dtos;

public readonly record struct ThresholdRecommendationDto(
    Vitamin Vitamin,
    MeasurementUnit MeasurementUnit,
    double CurrentIntake,
    double LowerLimit,
    double? UpperLimit = null);