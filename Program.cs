using Microsoft.EntityFrameworkCore;
using CafeMVC.Data;
using CafeMVC.Repositories;
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICafeChainRepository, CafeChainRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var retries = 10;
    while (retries > 0)
    {
        try
        {
            db.Database.Migrate();
            break;
        }
        catch (Exception)
        {
            retries--;
            Console.WriteLine($"DB not ready, retrying... ({retries} left)");
            Thread.Sleep(3000);
        }
    }
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=CafeChains}/{action=Index}/{id?}");
app.Run();
