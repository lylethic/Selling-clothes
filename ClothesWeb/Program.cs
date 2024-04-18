using ClothesWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ClothesWeb.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ClothesWeb.Models.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("MyLocal");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddIdentity<IdentityUser, IdentityRole>()
  .AddEntityFrameworkStores<ApplicationDbContext>()
  .AddDefaultUI()
  .AddDefaultTokenProviders();

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
//  .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

//builder.Services.AddAuthentication(options =>
//{
//  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//  options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//  options.SaveToken = true;
//  options.RequireHttpsMetadata = false;
//  options.TokenValidationParameters = new TokenValidationParameters
//  {
//    ValidateIssuer = true,
//    ValidateAudience = true,
//    ValidAudience = "",
//    ValidIssuer = "",
//    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(""))
//  };

//});

builder.Services.AddSession();

builder.Services.AddControllersWithViews();

// Dang ky Interface
builder.Services.AddSingleton<IVnPayService, VnPayService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
//pattern: "{area=Owner}/{controller=Admin}/{action=Dashboard}/{id?}");


app.MapRazorPages();

app.Run();
