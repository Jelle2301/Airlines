using Airlines.Data;
using Airlines.EmailSender;
using Domains.Data;
using Domains.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositories;
using Repositories.Interfaces;
using Services;
using Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<AirlineDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();



builder.Services.AddControllersWithViews();

// SwaggerGen produces JSON schema documents that power Swagger UI.By default, these are served up under / swagger
//{ documentName}/ swagger.json, where { documentName} is usually the API version.
//provides the functionality to generate JSON Swagger documents that describe the objects, methods, return types, etc.
//eerste paramter, is de naam van het swagger document
//
// Register the Swagger generator, defining 1 or more Swagger documents
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API Flights",
        Version = "version 1",
        Description = "An API to see flight info",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "JGQB",
            Email = "quinten.bernard1@gmail.com",
            Url = new Uri("https://vives.be"),
        },
        License = new OpenApiLicense
        {
            Name = "Flight API",
            Url = new Uri("https://example.com/license"),
        }
    });
});
// add Automapper
builder.Services.AddAutoMapper(typeof(Program));
//session
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "GilweAirlines.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(10);//moet minimum 1 seconde zijn
});
//DI
builder.Services.AddTransient<IPlaatsDAO, PlaatsDAO>();
builder.Services.AddTransient<IPlaatsService, PlaatsService>();
builder.Services.AddTransient<IVluchtDAO, VluchtDAO>();
builder.Services.AddTransient<IVluchtService, VluchtService>();
builder.Services.AddTransient<IMaaltijdDAO, MaaltijdDAO>();
builder.Services.AddTransient<IMaaltijdService, MaaltijdService>();
builder.Services.AddTransient<IReisklasseDAO, ReisklasseDAO>();
builder.Services.AddTransient<IReisklasseService, ReisklasseService>();
builder.Services.AddTransient<ISeizoenDAO, SeizoenDAO>();
builder.Services.AddTransient<ISeizoenService, SeizoenService>();
builder.Services.AddTransient<IZitplaatsDAO, ZitplaatsDAO>();
builder.Services.AddTransient<IZitplaatsService, ZitplaatsService>();

//builder.Services
// .AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
// })
// .AddJwtBearer(cfg =>
// {
//     cfg.SaveToken = true;
//     cfg.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidIssuer = builder.Configuration["JwtConfig:JwtIssuer"],
//         ValidAudience = builder.Configuration["JwtConfig:JwtIssuer"],
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:JwtKey"])),
//         ClockSkew = TimeSpan.Zero // remove delay of token when expire
//     };
// });

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddSingleton<IEmailSender, EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
var swaggerOptions = new Airlines.Options.OptionsSwagger();
builder.Configuration.GetSection(nameof(Airlines.Options.OptionsSwagger)).Bind(swaggerOptions);
// Enable middleware to serve generated Swagger as a JSON endpoint.
//RouteTemplate legt het path vast waar de JSON-file wordt aangemaakt
app.UseSwagger(option => { option.RouteTemplate = swaggerOptions.JsonRoute; });
// By default, your Swagger UI loads up under / swagger /.If you want to change this, it's thankfully very straight-forward.
//Simply set the RoutePrefix option in your call to app.UseSwaggerUI in Program.cs:
app.UseSwaggerUI(option =>
{
    option.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
});
app.UseSwagger();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//add session
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
