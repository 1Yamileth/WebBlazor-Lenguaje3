using Datos.Interfaces;
using Datos.Repositorios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using System.Security.Claims;

namespace Blazor_Vista.Controllers
{
    public class LoginController : Controller
    {
        private readonly Config _config;//Llamamos a la cadena de conexion
        //LLAMAMOS A LOS REPOSITORIOS
        private ILoginRepositorio _loginRepositorio;
        private IUsuarioRepositorio _usuarioRepositorio; 
        //Se lo pasamos al contructor de esta clase
        public LoginController(Config config)
        {
            _config = config;
            _loginRepositorio = new LoginRepositorio(config.CadenaConexion);
            _usuarioRepositorio = new UsuarioRepositorio(config.CadenaConexion);
        }

        //anclar nuestro endpoint asi lo llamamos
        [HttpPost("/autenticar/validar")]//tapamos las rutas en el navegador con este
        public async Task<IActionResult> Validacion(LoginM login)//El IactioRESULT CAPTURA LO QUE RETORNA UN ENDPOINT DE APIREST
        {
            string rol = string.Empty;//Validamos que este vacia
            try
            {//VALIDAMOPS QUE INGRESO LOS DATOS CORRECTAMENTE
                bool usuarioValido = await _loginRepositorio.ValidarUsuarioAsync(login);/* Cuando se hagael metodo "validar usuario async" nos devolvera tru or false
                                                                                            true siesta correcto el usuario y false si no*/
                //VALIDAMOS SI ESTA ACTIVO O NO
                if (usuarioValido)
                {
                    UsuariosM user = await _usuarioRepositorio.GetPorCodigoAsync(login.CodigoUsuario);
                    /*Una vez validado en la linea 29, aca se conulta todo el objeto con el metodo "Get por codigo async"
                     y vemos si si esta activo*/
                    if (user.EstadoActivo)
                    {
                        rol = user.Rol;//SI el usuario esta activo le pasamos que rol tiene

                        //Creamos nuestra sesion,trayendo los valores de usuario
                        var claims = new[]
                        {
                            new Claim(ClaimTypes.Name, user.CodigoUsuario),
                            new Claim(ClaimTypes.Role, user.Rol)
                        };
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);//Le pasamos el claims que acabamos de crear y el tipo de autentificacion
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);//MANDA O EJECUTA LA SESION Y EL IDENTITY LE PASAMOS LOS VALORES

                        //INICIO DE SESION
                        /*El HttpContext sirve para poder acceder al contexto del sitio web, con el SignInAsync abrimos la sesion y le pasamos el esquema de autentificacion y los claims, 
                         asi ya iniciamos sesion y le pasaremos parametros ara decirle cuanto dure abierto la sesion:  ExpiresUtc = DateTime.UtcNow.AddMinutes(20).*/

                         await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.UtcNow.AddMinutes(20) });


                    }
                    else
                    {
                        /*Si no esta registrado lo redireciona a la pagina principal.
                         El metodo LocalRedirec sirve en blazor para poder mandar hacia otra ruta */
                        return LocalRedirect("/login/USUARIO NO ENCONTRADO ACTIVO");
                    }
                }
                else
                {//Si los datos son erroneos nos dira el mensjae
                    return LocalRedirect("/login/USUARIO NO ENCONTRADO, DATOS INVALIDOS");
                }
            }
            catch (Exception)
            {
            }
            return LocalRedirect("/");//Inciai sesion si todo esta correcto.
        }

        [HttpGet("/salir")]
        public async Task<ActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/login");
        }


    }
}
