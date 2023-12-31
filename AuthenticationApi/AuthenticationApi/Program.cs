using AuthenticationApi.Data;
using AuthenticationApi.Services.Authenticate;
using AuthenticationApi.Services.Permission;
using AuthenticationApi.Services.User;
using AuthenticationApi.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

#region Version
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
});
#endregion

#region DB
builder.Services.AddDbContext<AuthenticationApiDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStrEf"));
});
#endregion

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(config =>
{
    config.ResolveConflictingActions(api => api.First());
    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Reference = new OpenApiReference()
        {
            Id = "Bearer",
            Type = ReferenceType.SecurityScheme
        },
        Scheme = "Bearer",
        Name = "Authorization",
        In = ParameterLocation.Header
    });
    config.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                 Reference=new OpenApiReference()
                 {
                     Type=ReferenceType.SecurityScheme,
                     Id="Bearer"
                 }
            },
            new string[] {}
        }
    });

    config.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "1",
        Title = "AuthenticationApi v1",
        Description = "Authorization with Token And Refresh Token",
        Contact = new OpenApiContact
        {
            Name = "Hessam Mousavi",
            Email = "Hessam.mousavi99@gmail.com"
        }
    });
    config.OperationFilter<RemoveVersionFromParameter>();
    config.DocumentFilter<ReplaceVersionWithExactValueInPath>();
});

#region Ioc
builder.Services.AddSingleton<EncryptionUtility>();
builder.Services.AddScoped<DapperUtility>();
builder.Services.AddScoped<TokenUtility>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();

#endregion

#region Token Auth
var secretKey = builder.Configuration.GetValue<string>("TokenKey");
var tokenTimeOut = builder.Configuration.GetValue<int>("TokenTimeOut");
var key = Encoding.UTF8.GetBytes(secretKey);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ClockSkew = TimeSpan.FromMinutes(tokenTimeOut),
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});
#endregion

#region Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicyKeys.Policy_A, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
        config.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthenticationApi V1");
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(CorsPolicyKeys.Policy_A);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
