using GeneCareDemo.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;// Ensure this namespace is included for extension methods like UseSqlServer  

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.  
builder.Services.AddControllersWithViews();

// Service for DB Connection  
builder.Services.AddDbContext<ApplicationDbContext>(contextOptions =>
{
    contextOptions.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); // Corrected capitalization of 'UseSqlServer'  
});

var app = builder.Build();

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
