using Westwind.AspNetCore.LiveReload;
using Microsoft.EntityFrameworkCore;
using EduAdmissionSite.Models;
using EduAdmissionSite.Data.Seed;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddLiveReload();

// EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Repositories
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();

var app = builder.Build();

app.UseLiveReload();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Seed database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    var seeder = new DatabaseSeeder(context);

    // Ensure database is created and migrated
    await context.Database.MigrateAsync();

    // Seed data
    await seeder.SeedAsync();

    // Debug: Count seeded articles
    var articleCount = await context.Articles.CountAsync();
    Console.WriteLine($"Seeded {articleCount} articles");
}

app.Run();