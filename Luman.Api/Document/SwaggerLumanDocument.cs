using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Luman.Api.Document
{
    public class SwaggerLumanDocument : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;


        public SwaggerLumanDocument(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {

            foreach (var item in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(item.GroupName,
                     new OpenApiInfo()
                     {
                         
                         Title = $"Luman Api Version {item.ApiVersion}",
                         Version = item.ApiVersion.ToString(),
                         Contact = new OpenApiContact()
                         {
                             Name = "Ahmadreza golmakani nia",
                             Email = "argolmakani@gmail.com",

                         }
                         
                     });
            }


            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "این کادر برای احراز هویت (jwt) هست \r\n\r\n" +
                "برای تست بعد از 'Bearer' \r\n\r\n" +
                " و یک فاصله توکن خود را وارد کنید .\r\n\r\n" +
                "example : Bearer dl;fgkd;fgldfgjdlfkgjiogjighuieredghsdklfghsjkdfhsdkljfl",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"

            });


            options.AddSecurityRequirement(document => new()
            {
                [new OpenApiSecuritySchemeReference("Bearer", document)] = new List<string>()
            });

            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "comment.xml"));

        }
    }
}
