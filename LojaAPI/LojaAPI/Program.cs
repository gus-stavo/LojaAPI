using LojaAPI.Domain.Interfaces.DAL;
using LojaAPI.Domain.Interfaces.Services;
using LojaAPI.Infra.Context;
using LojaAPI.Infra.Data;
using LojaAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddDbContext<LojaDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("LojaMSSQL")));

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IClienteDAL, ClienteDAL>();
builder.Services.AddScoped<ITelefoneService, TelefoneService>();
builder.Services.AddScoped<ITelefoneDAL, TelefoneDAL>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => 
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "LojaAPI", Version = "v1" });

    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => 
    {
        options.SwaggerEndpoint("v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
