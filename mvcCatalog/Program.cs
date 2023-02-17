using Microsoft.EntityFrameworkCore;
using mvcCatalog.Data;
using mvcCatalog.Models;
using mvcCatalog.Repositories;
using mvcCatalog.Repositories.CategoryRepo;
using mvcCatalog.Repositories.ProductFromSupplierRepo;
using mvcCatalog.Repositories.ProductRepo;
using mvcCatalog.Repositories.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductFromSupplierRepository, ProductFromSupplierRepository>();
builder.Services.AddSingleton<IBucketService, BucketService>();


builder.Services.AddScoped<AppRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

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
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();