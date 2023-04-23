using Blazor_Vista.Interfaces;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Modelos;

namespace Blazor_Vista.Pages.ProductosMios
{
    public partial class EditarProducto
    {
        [Inject] IProductoServicio productoServicio { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private SweetAlertService Swal { get; set; }
        [Parameter] public string CodigoProducto { get; set; }//Parametro que le pasamos al momento de editar al componente "ediatrproducto.razor"
        
        ProductosM prod = new ProductosM();

        string imgURL = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(CodigoProducto))//Si es diferente de null trae algo
            {
                prod = await productoServicio.GetPorCodigoAsync(CodigoProducto);
            }
        } 

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
           

            //Si los datos esta llenos pasa a esta variable 
            bool edito = await productoServicio.ActualizarAsync(prod);
            if (edito)
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

        protected async void Eliminar()
        {
            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = "¿Seguro que desea eliminar el producto?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
                ConfirmButtonText = "Aceptar",
                CancelButtonText = "Cancelar"
            });

            if (!string.IsNullOrEmpty(result.Value))
            {
                bool elimino = await productoServicio.EliminarAsync(prod.CodigoProducto);//ELIMINAMOS EL REGISTRO
                if (elimino)
                {
                    await Swal.FireAsync("FELICIDADES", "Producto eliminado", SweetAlertIcon.Success);
                    navigationManager.NavigateTo("/Productos");//ELIMINA EL REGISTRO Y LO MANDA DIRECTAMENTE A LA PARTE DE USUARIOS

                }
                else
                {
                    await Swal.FireAsync("ERROR", "Producto No Eliminado", SweetAlertIcon.Error);
                }
            }

        }



    }
}
