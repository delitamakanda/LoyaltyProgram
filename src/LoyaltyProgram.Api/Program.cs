using Microsoft.EntityFrameworkCore;
using LoyaltyProgram.Infrastructure;
using LoyaltyProgram.Application;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using LoyaltyProgram.Api.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureOptions<LoyaltyProgram.Api.ConfigureSwaggerOptions>();

builder.Services.AddDbContext<LoyaltyDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("LoyaltyProgram.Api")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, false));

});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;

    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader(),
        new MediaTypeApiVersionReader("x-api-version")
    );
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty)),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero, // remove delay of token when expire
        ValidAudience = builder.Configuration["Jwt:Audience"] ?? "LoyaltyProgram",
        ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? "LoyaltyProgram",
        RequireExpirationTime = true,
        RequireSignedTokens = true,
        ValidateAudience = builder.Environment.IsProduction(),
        ValidateIssuer = builder.Environment.IsProduction()
    };
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("Authentication failed: " + context.Exception.Message);
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine("Token validated");
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            Console.WriteLine("Challenge called" + context.ErrorDescription);
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddSwaggerGen(c =>
{
    c.DocumentFilter<SnakeCaseDocumentFilter>();
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAny", builder => builder
       .AllowAnyOrigin()
       .AllowAnyMethod()
       .AllowAnyHeader());
});

builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<LoyaltyProgramService>();
builder.Services.AddScoped<ShopService>();
builder.Services.AddScoped<LoyaltyCardService>();
builder.Services.AddScoped<RewardService>();
builder.Services.AddScoped<HistoryRewardService>();
builder.Services.AddScoped<UserService>();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowAny");

app.Run();
