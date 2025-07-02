using Microsoft.EntityFrameworkCore;
using LoyaltyProgram.Infrastructure;
using LoyaltyProgram.Application;
using System.Text.Json.Serialization;
using System.Text.Json;
using LoyaltyProgram.Api.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LoyaltyDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("LoyaltyProgram.Api")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, false));

});

builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<LoyaltyProgramService>();
builder.Services.AddScoped<ShopService>();
builder.Services.AddScoped<LoyaltyCardService>();
builder.Services.AddScoped<RewardService>();
builder.Services.AddScoped<HistoryRewardService>();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.DocumentFilter<SnakeCaseDocumentFilter>();
});
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
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
