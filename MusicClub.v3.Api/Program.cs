using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Microsoft.EntityFrameworkCore;
using MusicClub.v3.Api.Middleware;
using MusicClub.v3.DbCore.Services;
using MusicClub.v3.DbCore;
using MusicClub.v3.DbServices.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var credentialPath = Path.Combine(Directory.GetCurrentDirectory(), "secrets", "google-calendar-service-account-key.json");

var googleCredential = GoogleCredential.FromFile(credentialPath).CreateScoped(CalendarService.Scope.Calendar);

var calendarService = new CalendarService(new BaseClientService.Initializer()
{
    HttpClientInitializer = googleCredential,
    ApplicationName = "MusicClub"
});

builder.Services.AddSingleton(provider =>
{
    return calendarService;
});


builder.Services.AddDbContext<MusicClubDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddDbServices();

builder.Services.AddScoped<TenantService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
