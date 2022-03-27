using Microsoft.EntityFrameworkCore;
using ClientApp.Data;
using ClientApp.Services.Ibge;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ClientAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClientAppContext")));

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IIbgeService, IbgeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/Index");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Client}/{action=Index}/{id?}");

app.Run();
