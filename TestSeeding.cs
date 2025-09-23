using EduAdmissionSite.Data.Seed;
using EduAdmissionSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

class TestSeeding
{
    static async Task Main(string[] args)
    {
        // Load connection string
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = config.GetConnectionString("Default");

        // Setup services
        var services = new ServiceCollection();
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Migrate
        await context.Database.MigrateAsync();

        // Seed
        var seeder = new DatabaseSeeder(context);
        await seeder.SeedAsync();

        // Check results
        var count = await context.Articles.CountAsync();
        Console.WriteLine($"Total articles seeded: {count}");

        var articles = await context.Articles.ToListAsync();
        foreach (var article in articles)
        {
            Console.WriteLine($"- {article.Title} ({article.Category})");
        }
    }
}
