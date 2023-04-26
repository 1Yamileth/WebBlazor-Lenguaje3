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
        Task<UsuariosM> GetPorCodigoAsync(string codigoUsuario);//Le pasaremos el codigo del usuario y nos retornara todo el objeto
        Task<bool> NuevoAsync(UsuariosM usuario);
        Task<bool> ActualizarAsync(UsuariosM usuario);
        Task<bool> EliminarAsync(string codigoUsuario);
        Task<IEnumerable<UsuariosM>> GetListaAsync();//De la interfaz Ienumerable se heredan todas las colecciones 

    }
}
