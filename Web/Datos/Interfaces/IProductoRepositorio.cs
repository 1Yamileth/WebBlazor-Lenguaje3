using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Interfaces
{
    public interface IProductoRepositorio
    {
        Task<bool> NuevoAsync(ProductosM producto);
        Task<bool> ActualizarAsync(ProductosM producto);
        Task<bool> EliminarAsync(string CodigoProducto);
        Task<IEnumerable<ProductosM>> GetListaAsync();
        Task<ProductosM> GetPorCodigoAsync(string CodigoProducto);//Le pasaremos el codigo del usuario y nos retornara todo el objeto

    }
}
