using Apoint_pro.cofig;
using Apoint_pro.Data;
using Apoint_pro.Data.Helpers;
using Apoint_pro.Extentions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomJwtToken(builder.Configuration);
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
//builder.Services.AddSingleton<TokenService>();
//builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseMySql(builder.Configuration.GetConnectionString("myCon"),
//         new MySqlServerVersion(new Version(10, 4, 32))
//         ));
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseMySql(builder.Configuration.GetConnectionString("myCon"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("myCon")))
                .UseLazyLoadingProxies());
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin() // «·”„«Õ ·√Ì œÊ„Ì‰
              .AllowAnyHeader() // «·”„«Õ »√Ì ÂÌœ—“
              .AllowAnyMethod(); // «·”„«Õ »√Ì „ÌÀÊœ (GET, POST, ≈·Œ)
    });
});


//builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
