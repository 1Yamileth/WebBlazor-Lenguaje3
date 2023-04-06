using Blazor_Vista.Interfaces;
using Datos.Interfaces;
using Datos.Repositorios;
using Modelos;


namespace Blazor_Vista.Servicios
{//ERROR
    public class LoginServicio : ILoginServicio
    {//TRABAJAMOS CON LOS SEVRICIOS ANTES AGREGADOS
        private readonly Config _config;
        private ILoginRepositorio loginRepositorio;

        public LoginServicio(Config config)
        {
            _config = config;
            loginRepositorio = new LoginRepositorio(config.CadenaConexion);
        }
        public async  Task<bool> ValidarUsuarioAsync(LoginM login)
        {
            return await loginRepositorio.ValidarUsuarioAsync(login);
        }
    }
}
