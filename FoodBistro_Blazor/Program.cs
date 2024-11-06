using FoodBistro_Blazor.Components;
using FoodBistro_Blazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Register services in the DI container
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(); // Only if using interactive components, otherwise you can skip this.

builder.Services.AddTransient<IUserService>(_ => new UserService("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BistroManagement;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));
builder.Services.AddSingleton<ICartService, CartService>(); // Cart service is registered as Singleton
builder.Services.AddSingleton<ProductService>();

// Add support for static files (e.g., images, CSS, JS)
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Configure the app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts(); // Only needed in production
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery(); // Ensure this is configured if needed

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode(); // Interactive components mode if needed

app.MapControllers(); // Make sure controllers are mapped if you have any
app.MapRazorPages(); // For Razor pages and components

app.Run();
