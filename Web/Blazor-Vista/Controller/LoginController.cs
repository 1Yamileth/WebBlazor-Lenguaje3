using Datos.Interfaces;
using Datos.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Modelos;

namespace Blazor_Vista.Controller
{
    public class LoginController : Controller
    {
        private readonly Config _config;//Llamamos a la cadena de conexion
        //LLAMAMOS A LOS REPOSITORIOS
        private ILoginRepositorio _loginRepositorio;
        private IUsuarioRepositorio _usuarioRepositorio; 
        //Se lo pasamos al contructor de esta clase
        LoginController(Config config)
        {
            _config = config;
            _loginRepositorio = new LoginRepositorio(config.CadenaConexion);
            _usuarioRepositorio = new UsuarioRepositorio(config.CadenaConexion);
        }

        //anclar nuestro endpoint asi lo llamamos
        [HttpPost("autenticar/validar")]//tapamos las rutas en el navegador con este
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
                    if (user.EstaActivo)
                    {

                    }
                }
            }
            catch (Exception)
            {
            }
        } 
    }
}
