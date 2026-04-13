using Asp.Versioning;
using Pazario.API.OpenApi;
using Pazario.Products.Infrastructure;
using Pazario.Products.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProductModule(builder.Configuration);

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();

        foreach (var description in descriptions)
        {
            var url = "/swagger/" + description.GroupName + "/swagger.json";
            options.SwaggerEndpoint(url, description.GroupName.ToUpperInvariant());
        }
    });
}


app.ApplyProductsMigrations();
app.UseHttpsRedirection();

app.MapProductModuleEndpoints();

app.Run();
