using ClothingStoreAPI.Mapper;
using ClothingStoreApplication.Interface;
using ClothingStoreDomain;
using ClothingStoreInfrastructure.Data;
using ClothingStoreInfrastructure.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ClothDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ClothString")));

builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ClothDbContext>();

builder.Services.AddScoped<IAuth, AuthImplementation>();
builder.Services.AddScoped<IAuthMapper, AuthMapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
