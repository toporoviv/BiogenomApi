using BiogenomApi.Domain.Entities;
using BiogenomApi.Domain.Enums;
using BiogenomApi.Services.Models.Enums;

namespace BiogenomApi.Services.Extensions;

internal static class VitaminExtensions
{
    public static VitaminState GetState(this Vitamin vitamin, NutrientAmount value) =>
        vitamin.IsValueInNormalRange(value) switch
        {
            false => VitaminState.Bad,
            _ => VitaminState.Good
        };

    public static bool IsValueInNormalRange(this Vitamin vitamin, double value)
    {
        return vitamin.UpperLimit is null
            ? value >= vitamin.LowerLimit
            : vitamin.LowerLimit <= value && value <= vitamin.UpperLimit;
    }
    
    public static bool IsValueInNormalRange(this Vitamin vitamin, NutrientAmount value)
    {
        // Единицы измерения могут отличаться, поэтому приводим их к общей единице - мкг
        var nutrientAmountValue = GetNormalizedNutrientAmount(value);
        var vitaminNutrientAmountLowerValue = GetNormalizedNutrientAmount(new NutrientAmount
        {
            Unit = vitamin.MeasurementUnit,
            Value = vitamin.LowerLimit
        });

        if (vitamin.UpperLimit is null)
        {
            return vitaminNutrientAmountLowerValue <= nutrientAmountValue;
        }

        var vitaminNutrientAmountUpperValue = GetNormalizedNutrientAmount(new NutrientAmount
        {   
            Unit = vitamin.MeasurementUnit,
            Value = (double)vitamin.UpperLimit
        });

        return vitaminNutrientAmountLowerValue <= nutrientAmountValue &&
               nutrientAmountValue <= vitaminNutrientAmountUpperValue;
    }

    private static double GetNormalizedNutrientAmount(NutrientAmount nutrientAmount)
    {
        // Выбрасываем исключение если натыкаемся не на единицу массы
        var power = nutrientAmount.Unit switch
        {
            MeasurementUnit.Kilocalories => throw new ArgumentOutOfRangeException(nameof(nutrientAmount.Unit)),
            _ => (int)nutrientAmount.Unit + 1
        };
        
        return nutrientAmount.Value * Math.Pow(10, power);
    }
    
    public static VitaminDietarySupplement? GetFirstVitaminDietarySupplementForVitaminDeficiency(this Vitamin vitamin)
    {
        return vitamin.RelatedSupplements
            .FirstOrDefault(rs =>
                rs.Amount.Unit == vitamin.MeasurementUnit);
    }
}