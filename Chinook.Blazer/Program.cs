using Chinook.Blazer.Components;
using Chinook.DAL;
using Chinook.Models;
using SQLitePCL;

var builder = WebApplication.CreateBuilder(args);
Batteries_V2.Init();
builder.Services.AddDbContext<ArtistContext>();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<IRepository, Repository>();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error", createScopeForErrors: true);
 
  app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.Run();
