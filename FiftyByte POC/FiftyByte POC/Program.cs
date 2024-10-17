using FiftyByte_POC.Data;
using FiftyByte_POC.Repositories;
using FiftyByte_POC.Repositories.Concrete;
using FiftyByte_POC.Repositories.Interface;
using FiftyByte_POC.Repositories.Interfaces;
using FiftyByte_POC.Services;
using FiftyByte_POC.Services.Concrete;
using FiftyByte_POC.Services.Interface;
using FiftyByte_POC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add DbContext configuration using the connection string from appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories and services for dependency injection.
builder.Services.AddScoped<ICompanyLogoRepository, CompanyLogoRepository>();
builder.Services.AddScoped<ICompanyLogoService, CompanyLogoService>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ITranslationRepository, TranslationRepository>();
builder.Services.AddScoped<ITranslationService, TranslationService>();
builder.Services.AddScoped<ILeaveApplicationRepository, LeaveApplicationRepository>();
builder.Services.AddScoped<ILeaveApplicationService, LeaveApplicationService>();





// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<SwaggerFileOperationFilter>();
});


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
