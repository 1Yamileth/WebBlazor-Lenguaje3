 using Blazor_Vista;
using Blazor_Vista.Interfaces;
using Blazor_Vista.Servicios;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authentication.Cookies;
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
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();//Inyectaremos o pasaremos el servicio de tipo de autentificacion
builder.Services.AddHttpContextAccessor();//Nos permite acceder a los datos del usuario que esta con la sesion activa, que es lo que hicimos con el HttpContext en loginController
builder.Services.AddSweetAlert2();
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
//Lo usamos para autentificar y en dado caso autorizar a los usuarios
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();//Le pasamos este middlelwer, signiica que nuestra app de blazor usara controladores, sino dara error el logincontroller
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
