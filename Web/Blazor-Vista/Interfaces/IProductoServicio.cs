using Modelos;

namespace Blazor_Vista.Interfaces
{
    public interface IProductoServicio
    {
        Task<bool> NuevoAsync(ProductosM producto);
        Task<bool> ActualizarAsync(ProductosM producto);
        Task<bool> EliminarAsync(string codigo);
        Task<IEnumerable<ProductosM>> GetListaAsync();
        Task<ProductosM> GetPorCodigoAsync(string codigo);
    }
}
