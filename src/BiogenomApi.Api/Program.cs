using BiogenomApi.Api.Gql.Dtos;
using BiogenomApi.Api.Gql.Queries;
using BiogenomApi.Api.Middlewares;
using BiogenomApi.Domain.Enums;
using BiogenomApi.Infrastructure;
using BiogenomApi.Infrastructure.Extensions;
using BiogenomApi.Services.Extensions;
using BiogenomApi.Services.Models.Enums;
using HotChocolate.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace BiogenomApi.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddAuthorization();
        builder.Services.AddSwaggerGen();
        builder.Services.AddOpenApi();
        builder.Services.AddBiogenomInfrastructure();
        builder.Services.AddBiogenomServices();
        builder.Services.AddControllers();

        builder.Services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAny", policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        
        builder.Services
            .AddGraphQLServer()
            .AddQueryType<DbLoggerCategory.Query>()
            .AddTypeExtension<PersonalReportQuery>()
            .AddType<DailyIntakeVitaminGqlDto>()
            .AddType<DietarySupplementGqlDto>()
            .AddType<DietarySupplementRecommendationGqlDto>()
            .AddType<VitaminState>()
            .AddType<MeasurementUnit>();
        
        var app = builder.Build();
        
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            context.Database.Migrate();
            DbInitializer.SeedData(context);
        }
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapOpenApi();
        }

        app.UseMiddleware<ExceptionMiddleware>();
        
        app.UseHttpsRedirection();
        app.UseCors("AllowAny");

        app.UseAuthorization();
        app.MapControllers();
        app.MapGraphQL();
        app.UsePlayground("/ui/graphql");

        app.Run();
    }
}