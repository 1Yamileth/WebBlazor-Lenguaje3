using Blazor_Vista.Interfaces;
using Blazor_Vista.Pages.UsuarioMio;
using Datos.Interfaces;
using Datos.Repositorios;
using Modelos;

namespace Blazor_Vista.Servicios
{//ERRORE ACA 
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly Config _config;
        private IUsuarioRepositorio usuarioRepositorio;
        //constructor para acceder a la cadena
        private UsuarioServicio(Config config)
        {
            _config = config;
            usuarioRepositorio = new UsuarioRepositorio(config.CadenaConexion);
        }

        public async Task<bool> ActualizarAsync(Modelos.UsuariosM usuario)
        {
            return await usuarioRepositorio.ActualizarAsync(usuario);
        }

        public async Task<bool> EliminarAsync(string codigo)
        {
            return await usuarioRepositorio.EliminarAsync(codigo);
        }

        public async Task<IEnumerable<Modelos.UsuariosM>> GetListaAsync()
        {
            return await usuarioRepositorio.GetListaAsync();
        }

        public async Task<Modelos.UsuariosM> GetPorCodigoAsync(string codigo)
        {
            return await usuarioRepositorio.GetPorCodigoAsync(codigo);
        }

        public async Task<bool> NuevoAsync(Modelos.UsuariosM usuario)
        {
            return await usuarioRepositorio.NuevoAsync(usuario);
        }
    }
}
