using Blazor_Vista.Interfaces;
using Microsoft.AspNetCore.Components;
using Modelos;

namespace Blazor_Vista.Pages.ProductosMios
{
    public partial class Productos
    {
        [Inject] IProductoServicio productoServicio { get; set; }
        IEnumerable<ProductosM> listaProductos { get; set;}

        protected override async Task OnInitializedAsync()
        {
            listaProductos = await productoServicio.GetListaAsync();
        }
    }
}
