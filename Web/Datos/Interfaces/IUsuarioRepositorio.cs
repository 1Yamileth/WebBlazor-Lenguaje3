using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario> GetPorCodigoAsync(string codigo);//Le pasaremos el codigo del usuario y nos retornara todo el objeto
        Task<bool> NuevoAsync(Usuario usuario);
        Task<bool> ActualizarAsync(Usuario usuario);
        Task<bool> EliminarAsync(string codigo);
        Task<IEnumerable<Usuario>> GetListaAsync();//De la interfaz Ienumerable se heredan todas las colecciones 

    }
}
