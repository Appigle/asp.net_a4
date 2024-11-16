using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LEI_CHEN_Practice_Asst_4.Data;
using Asst4.Utilities;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LEI_CHEN_Practice_Asst_4_DbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("LEI_CHEN_Practice_Asst_4_DbContext") ?? throw new InvalidOperationException("Connection string 'LEI_CHEN_Practice_Asst_4_DbContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

CRUDUtilities.Initialize(app.Services);

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

app.Run();
