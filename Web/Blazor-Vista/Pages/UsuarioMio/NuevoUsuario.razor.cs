using Blazor_Vista.Interfaces;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Modelos;

namespace Blazor_Vista.Pages.UsuarioMio
{
    public partial class NuevoUsuario
    {
        //Abrimos etiqueta y accedemos a la interfaz deseada
        [Inject] private IUsuarioServicio usuarioServicio { get; set; }

        [Inject] private NavigationManager navigationManager { get; set; }/*Objeto de navegacion, ya que cada vez que guardamos o editamos un usuario, queremos que nos devuelva a la 
        pagina anterior, o sea lista de usuarios, o se que navegaremos entre rutas*/
        [Inject] private SweetAlertService Swal { get; set; }

        private UsuariosM user = new UsuariosM();//Lo usamos para consultar en la BD para poderlo editar
        [Parameter] public string CodigoUsuario { get; set; }//Capturaremos el parametro que estamos recibiendo en "EditarUsuario"
        string imgURL = string.Empty;

        //Con este metodo mostraremos y buscaremos la imagen
        private async Task SelecionarImagen(InputFileChangeEventArgs e)
        {
            IBrowserFile imgFile = e.File;//Le asginamos lo que trae el evento
            var buffers = new byte[imgFile.Size];
            //CON LO DE ARRIBA TENEMOS SELECCIONADA LA FOTO
            user.Foto = buffers;
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
            if (string.IsNullOrWhiteSpace(user.CodigoUsuario) || string.IsNullOrWhiteSpace(user.Nombre) ||
                    string.IsNullOrWhiteSpace(user.Constrasena) || string.IsNullOrWhiteSpace(user.Rol) || user.Rol == "Seleccionar")
            {
                return;
            }
            user.FechaCreacion = DateTime.Now;//Le pasamos la fecha actual del sistema en la que se creo el usuario

            //Si los datos esta llenos pasa a esta variable 
            bool inserto = await usuarioServicio.NuevoAsync(user);
            if (inserto)
            {
                await Swal.FireAsync("FELICIDADES", "Usuario Guardado", SweetAlertIcon.Success);
            }
            else
            {
                await Swal.FireAsync("ERROR", "Usuario No Guardado", SweetAlertIcon.Error);
            }
        }
        protected async void Cancelar()
        {
            navigationManager.NavigateTo("/Usuarios");
        }
    }
}
