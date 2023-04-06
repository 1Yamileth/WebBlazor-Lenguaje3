using Blazor_Vista;
using Blazor_Vista.Interfaces;
using Blazor_Vista.Servicios;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

Config cadena = new Config(builder.Configuration.GetConnectionString("MySQL"));//Pasando la cdena de conexion, mediante el nombre de esta.
builder.Services.AddSingleton(cadena);//Configurajmos el servicio para usarlo 
//CONFIGURAMOS LOS SERVICIOS
builder.Services.AddScoped<ILoginServicio, LoginServicio>();//CLASE 
builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();//CLASE 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
