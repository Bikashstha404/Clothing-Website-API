using Clothing_Store.Mapper;
using ClothingStoreApplication.Interface;
using ClothingStoreInfrastrucutre.Data;
using ClothingStoreInfrastrucutre.Implementation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ClothDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ClothString")));

builder.Services.AddScoped<ICloth, ClothImplementation>();
builder.Services.AddScoped<IClothMapper, ClothMapper>();

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
