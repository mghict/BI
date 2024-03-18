using API.Common.ExceptionHandling;
using CaptchaConfigurations.ExtensionMethod;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Moneyon.Common.Web.IOC;
using Moneyon.PowerBi.API.Common.JWT;
using Moneyon.PowerBi.Domain.Model.Modeling;
using Moneyon.PowerBi.Domain.Service.IServices;
using Ocelot.Administration;
using Ocelot.DependencyInjection;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;



static void AddJWTAuthenticationUserToken(IServiceCollection services, IConfiguration Configuration)
{
    var bindJwtSettings = new JwtSettings();
    Configuration.Bind("JsonWebTokenKeys", bindJwtSettings);
    services.AddSingleton(bindJwtSettings);

    JwtExtensions.AddJwtAuthentication(services, bindJwtSettings);
}
static void AutoRegisterServices(IServiceCollection services, string environmentName)
{
    var executingAssm = Assembly.GetExecutingAssembly();
    var dir = Path.GetDirectoryName(executingAssm.Location);
    Assembly.LoadFrom(Path.Combine(dir, "Moneyon.PowerBi.Domain.Model.dll"));
    Assembly.LoadFrom(Path.Combine(dir, "Moneyon.PowerBi.Domain.Service.dll"));
    Assembly.LoadFrom(Path.Combine(dir, "Moneyon.PowerBi.Infrastructure.dll"));
    //Assembly.LoadFrom(Path.Combine(dir, "Moneyon.PowerBi.External.Service.dll"));
    Assembly.LoadFrom(Path.Combine(dir, "Moneyon.PowerBi.API.dll"));

    new ServiceRegistrar(services, environmentName, "Moneyon.PowerBi").Register();
}



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JsonWebTokenKeys"));

builder.Services.AddDbContext<IPowerBiContext, PowerBiContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("AppConnectionString");

    options.UseSqlServer(connectionString, p =>
    {
        p.CommandTimeout(30000);
        p.EnableRetryOnFailure(3);
    });
});


AutoRegisterServices(builder.Services, builder.Environment.EnvironmentName);


// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
}).AddJsonOptions(options =>
{
    var enumConverter = new JsonStringEnumConverter();
    options.JsonSerializerOptions.Converters.Add(enumConverter);
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "توکن الزامی است"
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                    }
                },
                new string[] {}
        }
    });
});


#region sqlServer Configs
builder.Services.AddDbContext<PowerBiContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:AppConnectionString"]);
});

#endregion

#region mapper
builder.Services.AddAutoMapper(typeof(UserMappingProfile));

#endregion

#region IOC


//builder.Services.AddScoped<IUserService, UserService>();
////builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

#endregion

#region Auth Configs
builder.Services.AddTransient<JwtTokenService>();
#endregion

#region Captcha

builder.Services.UseCaptcha(new CaptchaConfigurations.CaptchaOptionsDTO.CaptchaOptions()
{
    CaptchaType = CaptchaConfigurations.CaptchaOptionsDTO.CaptchaType.Numbers,
    CaptchaCharacter = 5,
    FontStyle = SixLabors.Fonts.FontStyle.Regular,
    CaptchaValueSendType = CaptchaConfigurations.CaptchaOptionsDTO.CaptchaValueSendType.InBody,
    Height = 40,
    Width = 100,
    NoiseRate = 200,
    DrawLines = 3,
    FontFamilies = new string[] { "Arail", "Verdana", "Hack" }

});

#endregion

var app = builder.Build();

app.UseMiddleware<JwtMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

//app.UseHsts();
//app.UseHttpsRedirection();
app.UseCors(c=>
{
    c.AllowAnyOrigin()
     .AllowAnyHeader()
     .AllowAnyMethod();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
