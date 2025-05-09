using Airlines.Data;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Domains.Data;
using Domains.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositories;
using Repositories.Interfaces;
using Services;
using Services.Interfaces;
using System.Text;
using Util.Mail;
using Util.Mail.interfaces;
using Util.PDF;
using Util.PDF.interfaces;

var builder = WebApplication.CreateBuilder(args);


// Key Vault settings
string? vaultUrl = builder.Configuration["KeyVault:VaultUrl"];
string? secretName = builder.Configuration["KeyVault:SecretName"];
string? secretNamePassword = builder.Configuration["KeyVault:SecretNamePassword"];

// Maak een client met default credentials (werkt lokaal met ingelogde gebruiker, en in Azure met managed identity)
var client = new SecretClient(new Uri(vaultUrl), new DefaultAzureCredential());

KeyVaultSecret secret = client.GetSecret(secretName);
KeyVaultSecret mailPassword = client.GetSecret(secretName);
string connectionString = secret.Value;

string mailPasswordString = mailPassword.Value;

// Add services to the container.
builder.Configuration["ConnectionStrings:DefaultConnection"] = connectionString;
builder.Configuration["ConnectionStrings:MailPassword"] = mailPasswordString;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<AirlineDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
//Localization - recource map 
builder.Services.AddLocalization(
    options => options.ResourcesPath = "Resources");




builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.SubFolder) // vertaling op View
    .AddDataAnnotationsLocalization(); // vertaling op ViewModel
// we need to decide which cultures we support, and which is the default culture.
var supportedCultures = new[] { "nl", "en", "fr", "es" };

//Localization - Dit configureert de standaard instellingen voor RequestLocalizationOptions en slaat ze op in de dependency injection-container (DI).*
builder.Services.Configure<RequestLocalizationOptions>(options => {
    options.SetDefaultCulture(supportedCultures[0])
      .AddSupportedCultures(supportedCultures)  //Culture is used when formatting or parsing culture dependent data like dates, numbers, currencies, etc
      .AddSupportedUICultures(supportedCultures);  //UICulture is used when localizing strings, for example when using resource files.
});


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
builder.Services.AddTransient<IHotelService, HotelService>();
builder.Services.AddTransient<ITicketDAO, TicketDAO>();
builder.Services.AddTransient<ITicketService, TicketService>();
builder.Services.AddTransient<IBoekingDAO, BoekingDAO>();
builder.Services.AddTransient<IBoekingService, BoekingService>();
//builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddSingleton<IEmailSend, EmailSend>();
builder.Services.AddTransient<IDAO<AspNetUser>, UserDAO>();
builder.Services.AddTransient<IService<AspNetUser>, UserService>();
builder.Services.AddTransient<ICreatePDF, CreatePDF>();




var app = builder.Build();

//Localization - Culture from the HttpRequest
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

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
