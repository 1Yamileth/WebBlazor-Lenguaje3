using Blazor_Vista.Interfaces;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Modelos;

namespace Blazor_Vista.Pages.ProductosMios
{
    public partial class NuevoProducto
    {
        [Inject] IProductoServicio productoServicio { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private SweetAlertService Swal { get; set; }
        ProductosM prod = new ProductosM();

        string imgURL = string.Empty;

        //Con este metodo mostraremos y buscaremos la imagen
        private async Task SelecionarImagen(InputFileChangeEventArgs e)
        {
            IBrowserFile imgFile = e.File;//Le asginamos lo que trae el evento
            var buffers = new byte[imgFile.Size];
            //CON LO DE ARRIBA TENEMOS SELECCIONADA LA FOTO
            prod.Foto = buffers;
            //MOSTRAMOS UNA VISTA PREVIA
            await imgFile.OpenReadStream().ReadAsync(buffers);
            //Veremos que tipo de imagen es pnj, jpg, etc
            string imagenType = imgFile.ContentType;
            //PARA PODERLA VER EN PANTALLA
            imgURL = $"data:{imgFile};base64,{Convert.ToBase64String(buffers)}";
        }

        protected async void Guardar()
        {
            //Validamos que el usuario ingreso datos
            if (string.IsNullOrWhiteSpace(prod.CodigoProducto) || string.IsNullOrWhiteSpace(prod.Descripcion))
            {
                return;
            }
            //No guardara producto repetido
            ProductosM prodExistente = new ProductosM();
            prodExistente = await productoServicio.GetPorCodigoAsync(prod.CodigoProducto);
            if (prodExistente !=  null)
            {
                if (!string.IsNullOrEmpty(prodExistente.CodigoProducto))//Si no esta vavio y nule, es porque existe un producto con el codigo
                {
                    await Swal.FireAsync("ADVERTENCIA", "Ya existe un producto con el mismo codigo", SweetAlertIcon.Warning);
                    return;
                }
                //SI no entra aca, es porque no hay un producto igual y se guardara bien.
            } 

            //Si los datos esta llenos pasa a esta variable 
            bool inserto = await productoServicio.NuevoAsync(prod);
            if (inserto)
            {
                await Swal.FireAsync("FELICIDADES", "Producto Guardado", SweetAlertIcon.Success);
            }
            else
            {
                await Swal.FireAsync("ERROR", "Producto No Guardado", SweetAlertIcon.Error);
            }
        }

        protected async void Cancelar()
        {
            navigationManager.NavigateTo("/Productos");
        }
    }
}
