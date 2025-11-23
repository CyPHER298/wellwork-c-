using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using WellworkGS.Data;
using WellworkGS.Infra.Persistence.Repository;
using WellworkGS.Service;
using WellworkGS.Utils;

var builder = WebApplication.CreateBuilder(args);

var swaggerConfig = builder.Configuration
    .GetSection("Swagger")
    .Get<SwaggerConfig>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = swaggerConfig.Title,
        Version = "v1",
        Description = swaggerConfig.Description,
        Contact = swaggerConfig.Contact
    });

    swagger.EnableAnnotations();

    foreach (var server in swaggerConfig.Servers)
    {
        swagger.AddServer(new OpenApiServer
        {
            Url = server.Url,
            Description = server.Name
        });
    }
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("Wellwork")));


builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IGestorRepository, GestorRepository>();
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddScoped<ITemporizadorRepository, TemporizadorRepository>();
builder.Services.AddScoped<IMetaRepository, MetaRepository>();
builder.Services.AddScoped<ILembreteRepository, LembreteRepository>();
builder.Services.AddScoped<IAlertaCriseRepository, AlertaCriseRepository>();

builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<GestorService>();
builder.Services.AddScoped<TarefaService>();
builder.Services.AddScoped<TemporizadorService>();
builder.Services.AddScoped<MetaService>();
builder.Services.AddScoped<LembreteService>();
builder.Services.AddScoped<AlertaCriseService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(ui =>
    {
        ui.SwaggerEndpoint("/swagger/v1/swagger.json", "WellworkGS API v1");
        ui.RoutePrefix = string.Empty;
    });

    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();