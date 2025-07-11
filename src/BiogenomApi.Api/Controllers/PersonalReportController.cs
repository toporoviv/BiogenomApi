using BiogenomApi.Api.Extensions;
using BiogenomApi.Services.Dtos;
using BiogenomApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BiogenomApi.Api.Controllers;

[ApiController]
[Route("personal-report")]
public class PersonalReportController(IVitaminService vitaminService) : ControllerBase
{
    // Информация о пользователе, в качестве примера, будет передаваться в строке запроса
    
    [HttpGet("get-daily-intake-vitamins-stats")]
    public async Task<IActionResult> GetDailyIntakeVitaminsStats([FromQuery] int userId)
    {
        var result = await vitaminService
            .GetDailyIntakeVitaminsStatsAsync(new GetDailyIntakeVitaminDto(userId));

        return Ok(result.AsResponse());
    }

    [HttpGet("get-personalized-dietary-supplements")]
    public async Task<IActionResult> GetPersonalizedDietarySupplements([FromQuery] int userId)
    {
        var result =
            await vitaminService.GetPersonalizedDietarySupplementsAsync(
                new GetPersonalizedDietarySupplementsDto(userId));

        return Ok(result.AsResponse());
    }

    [HttpGet("get-personalized-dietary-supplements-recommendations")]
    public async Task<IActionResult> GetPersonalizedDietarySupplementRecommendations([FromQuery] int userId)
    {
        var result =
            await vitaminService.GetPersonalizedDietarySupplementRecommendationsAsync(
                new GetPersonalizedDietarySupplementsRecommendationsDto(userId));

        return Ok(result.AsResponse());
    }
}