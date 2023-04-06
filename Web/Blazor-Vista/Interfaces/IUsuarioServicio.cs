using Modelos;

namespace Blazor_Vista.Interfaces
{
    public interface IUsuarioServicio
    {
        Task<UsuariosM> GetPorCodigoAsync(string codigo);//Le pasaremos el codigo del usuario y nos retornara todo el objeto
        Task<bool> NuevoAsync(UsuariosM usuario);
        Task<bool> ActualizarAsync(UsuariosM usuario);
        Task<bool> EliminarAsync(string codigo);
        Task<IEnumerable<UsuariosM>> GetListaAsync();//De la interfaz Ienumerable se heredan todas las colecciones 

    }
}
