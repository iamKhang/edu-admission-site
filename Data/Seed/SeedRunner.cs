using EduAdmissionSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EduAdmissionSite.Data.Seed;

/// <summary>
/// Utility class để chạy seeding độc lập (có thể dùng trong console app hoặc testing)
/// </summary>
public static class SeedRunner
{
    public static async Task RunAsync(string connectionString)
    {
        var services = new ServiceCollection();

        // Cấu hình DbContext
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var seeder = new DatabaseSeeder(context);

        // Migrate và seed
        await context.Database.MigrateAsync();
        await seeder.SeedAsync();

        Console.WriteLine("Database seeding completed!");
    }

    /// <summary>
    /// Chạy seeding từ command line (tùy chọn)
    /// Usage: dotnet run --seed
    /// </summary>
    public static async Task RunFromCommandLineAsync(string[] args)
    {
        if (args.Length == 0 || args[0] != "--seed")
            return;

        // Load connection string từ appsettings.json
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = config.GetConnectionString("Default");

        if (string.IsNullOrEmpty(connectionString))
        {
            Console.WriteLine("Connection string not found in appsettings.json");
            return;
        }

        await RunAsync(connectionString);
    }
}
