using Luman.Api.Document;
using Luman.Busines.Mapping;
using Luman.Busines.Services.OrderService;
using Luman.Busines.Services.PermissionService;
using Luman.Busines.Services.ProductService;
using Luman.Busines.Services.UserService;
using Luman.Busines.Utility;
using Luman.DataLayer.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Configuration;
using System.Net;
using System.Text;

using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();


builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); builder.Services.AddEndpointsApiExplorer();



#region SQL
builder.Services.AddDbContext<LumanContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("luman"));
});





#endregion

builder.Services.AddCors();

#region IOC

builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderServices, OrderServices>();

#endregion

#region Versioning

builder.Services.AddApiVersioning(option =>
{
    option.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.ReportApiVersions = true;
    option.ReportApiVersions = true;
    //option.ApiVersionReader = new HeaderApiVersionReader("X-ApiVersion");

});


builder.Services.AddVersionedApiExplorer(x =>
{
    x.GroupNameFormat = "'v'VVVV";
});

#endregion

#region Swagger
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerLumanDocument>();
builder.Services.AddSwaggerGen();


#endregion

#region JWT

var JwtSettingSection = builder.Configuration.GetSection("JwtSetting");

builder.Services.Configure<JwtSettings>(JwtSettingSection);

var Jwtsetting = JwtSettingSection.Get<JwtSettings>();


var key = Encoding.ASCII.GetBytes(Jwtsetting.Secret);



builder.Services.AddAuthentication(x =>
{
    x.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuerSigningKey = true,
        ValidIssuer = Jwtsetting.Issure,
        ValidateIssuer = true,
        ValidAudience = Jwtsetting.Audience,
        ValidateAudience = true,
        ValidateLifetime = true

    };
});


#endregion

#region Mapper
builder.Services.AddAutoMapper(typeof(MapperDTO));


#endregion











var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(x =>
//    {
//        var provider = app.Services.CreateScope().ServiceProvider
//                .GetRequiredService<IApiVersionDescriptionProvider>();

//        foreach (var item in provider.ApiVersionDescriptions)
//        {
//            x.SwaggerEndpoint($"swagger/{item.GroupName}/swagger.json", item.GroupName.ToString());

//        }
//        //x.SwaggerEndpoint("/swagger/VilaOpenApi/swagger.json" , "Vila");
//        x.RoutePrefix = "";
//    });
//}
app.UseSwagger();
app.UseSwaggerUI(x =>
{
    var provider = app.Services.CreateScope().ServiceProvider
            .GetRequiredService<IApiVersionDescriptionProvider>();

    foreach (var item in provider.ApiVersionDescriptions)
    {
        x.SwaggerEndpoint($"swagger/{item.GroupName}/swagger.json", item.GroupName.ToString());

    }
    //x.SwaggerEndpoint("/swagger/VilaOpenApi/swagger.json" , "Vila");
    x.RoutePrefix = "";
});
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


app.Run();
