using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CrudDbAccess.Configurations.Swagger
{
    public static class SwaggerConfig
    {

        public static void AddAppSwagger(this WebApplicationBuilder app)
        {
            app.Services.AddSwaggerGen();
        }

        public static void UseAppSwagger(this WebApplication web)
        {
            web.UseSwagger();
            web.UseSwaggerUI();
        }
    }
}
