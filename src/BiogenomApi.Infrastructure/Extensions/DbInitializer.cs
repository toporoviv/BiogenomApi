using BiogenomApi.Domain.Entities;
using BiogenomApi.Domain.Enums;

namespace BiogenomApi.Infrastructure.Extensions;

public static class DbInitializer
{
    public static void SeedData(DataContext context)
    {
        if (context.Users.Any()) return;
        
        var vitaminA = new Vitamin
        {
            Title = "Vitamin A",
            MeasurementUnit = MeasurementUnit.Micrograms,
            LowerLimit = 700,
            UpperLimit = 900,
            ImportanceForHealth = "Важен для зрения и иммунитета.",
            ScarcityManifestation = "Сухость глаз, ухудшение зрения.",
            Prevention = "Употребление моркови, печени."
        };

        var vitaminC = new Vitamin
        {
            Title = "Vitamin C",
            MeasurementUnit = MeasurementUnit.Milligrams,
            LowerLimit = 60,
            UpperLimit = null,
            ImportanceForHealth = "Поддерживает иммунитет и антиоксидантную защиту.",
            ScarcityManifestation = "Цинга, слабость.",
            Prevention = "Употребление цитрусовых, шиповника."
        };

        context.Vitamins.AddRange(vitaminA, vitaminC);
        
        var supplement1 = new DietarySupplement
        {
            Title = "MultiVitamin Complex",
            Description = "Комплексный БАД с витаминами группы B, A, C.",
            Application = "По 1 таблетке в день после еды.",
            Images = new List<DietarySupplementImage>(),
            RelatedVitamins = new List<VitaminDietarySupplement>
            {
                new VitaminDietarySupplement
                {
                    Vitamin = vitaminA,
                    Amount = new NutrientAmount { Value = 500, Unit = MeasurementUnit.Micrograms }
                },
                new VitaminDietarySupplement
                {
                    Vitamin = vitaminC,
                    Amount = new NutrientAmount { Value = 100, Unit = MeasurementUnit.Milligrams }
                }
            }
        };

        var supplement2 = new DietarySupplement
        {
            Title = "Vitamin C Booster",
            Description = "Высокая концентрация витамина C.",
            Application = "По 1 капсуле в день.",
            Images = new List<DietarySupplementImage>(),
            RelatedVitamins = new List<VitaminDietarySupplement>
            {
                new VitaminDietarySupplement
                {
                    Vitamin = vitaminC,
                    Amount = new NutrientAmount { Value = 500, Unit = MeasurementUnit.Milligrams }
                }
            }
        };

        context.DietarySupplements.AddRange(supplement1, supplement2);
        var carrot = new Food
        {
            Name = "Carrot",
            Description = "Сырая морковь.",
            Vitamins = new List<FoodVitamin>
            {
                new FoodVitamin
                {
                    Vitamin = vitaminA,
                    Amount = new NutrientAmount { Value = 800, Unit = MeasurementUnit.Micrograms }
                }
            }
        };

        var orange = new Food
        {
            Name = "Orange",
            Description = "Свежий апельсин.",
            Vitamins = new List<FoodVitamin>
            {
                new FoodVitamin
                {
                    Vitamin = vitaminC,
                    Amount = new NutrientAmount { Value = 70, Unit = MeasurementUnit.Milligrams }
                }
            }
        };

        context.Foods.AddRange(carrot, orange);
        var user = new User
        {
            FirstName = "John",
            Email = "john.doe@example.com",
            Birthday = DateTime.UtcNow,
            Gender = Gender.Male,
            VitaminSurveys = new List<UserVitaminSurvey>
            {
                new UserVitaminSurvey
                {
                    SurveyAtUtc = DateTime.UtcNow,
                    Results = new List<UserVitaminSurveyResult>
                    {
                        new UserVitaminSurveyResult
                        {
                            Vitamin = vitaminA,
                            Amount = new NutrientAmount { Value = 500, Unit = MeasurementUnit.Micrograms }
                        },
                        new UserVitaminSurveyResult
                        {
                            Vitamin = vitaminC,
                            Amount = new NutrientAmount { Value = 40, Unit = MeasurementUnit.Milligrams }
                        }
                    }
                }
            }
        };

        context.Users.Add(user);
        context.SaveChanges();
    }
}