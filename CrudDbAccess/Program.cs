using CrudDbAccess.Configurations.Database;
using CrudDbAccess.Configurations.Swagger;
using CrudDbAccess.Mapper;
using CrudDbAccess.Repos.Impelimantiations;
using CrudDbAccess.Repos.RepoInterfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IServiceBase, ServiceBase>();

builder.Services.AddScoped<IAccessService, AccessService>();

builder.Services.AddScoped<IDbService, DbService>();

builder.AddDatabaseConfigurations();

builder.Services.AddAutoMapper(mapper => mapper.AddProfile<MapperBase>());

builder.Services.AddControllers();

builder.Services.AddControllers()
    .AddJsonOptions(option => {
        option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        option.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyHeader();
                          policy.AllowAnyOrigin();
                          policy.AllowAnyMethod();

                      });
});

builder.AddAppSwagger();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAppSwagger();

app.UseRouting();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors(MyAllowSpecificOrigins);
app.MapControllers();

app.UseRouting();

app.Run();

public partial class Program { }
