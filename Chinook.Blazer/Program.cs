using Chinook.Blazer.Components;
using Chinook.DAL;
using Chinook.Models;
using SQLitePCL;

var builder = WebApplication.CreateBuilder(args);

Batteries_V2.Init();
// Add services to the container.
// W sekcji builder.Services
builder.Services.AddDbContext<ArtistContext>();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();
builder.Services.AddScoped<IRepository>();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error", createScopeForErrors: true);
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
