using Blazor_Vista.Interfaces;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Modelos;

namespace Blazor_Vista.Pages.UsuarioMio
{
    public partial class EditarUsuario
    {
        //Abrimos etiqueta y accedemos a la interfaz deseada
        [Inject] private IUsuarioServicio usuarioServicio { get; set; }

        [Inject] private NavigationManager navigationManager { get; set; }/*Objeto de navegacion, ya que cada vez que guardamos o editamos un usuario, queremos que nos devuelva a la 
        pagina anterior, o sea lista de usuarios, o se que navegaremos entre rutas*/

        private UsuariosM user = new UsuariosM();//Lo usamos para consultar en la BD para poderlo editar
        [Parameter] public string CodigoUsuario { get; set; }//Capturaremos el parametro que estamos recibiendo en "EditarUsuario"
        [Inject] private SweetAlertService Swal { get; set; }
        string imgURL = string.Empty;

        //Sobreescribiremos el metodo que secargara
        protected override async Task  OnInitializedAsync()//Este tipo de metodo se carga al momento de inciarlo
        {
            if (!string.IsNullOrEmpty(CodigoUsuario))
            {
                user = await  usuarioServicio.GetPorCodigoAsync(CodigoUsuario);
            }
        }
        //Con este metodo mostraremos la imagen
        private async Task SelecionarImagen(InputFileChangeEventArgs e)
        {
            IBrowserFile imgFile = e.File;//Le asginamos lo que trae el evento
            var buffers = new byte[imgFile.Size];
            //CON LO DE ARRIBA TENEMOS SELECCIONADA LA FOTO
            user.Fotografia = buffers;
            //MOSTRAMOS UNA VISTA PREVIA
            await imgFile.OpenReadStream().ReadAsync(buffers);
            //Veremos que tipo de imagen es pnj, jpg, etc
            string imagenType = imgFile.ContentType;
            //PARA PODERLA VER EN PANTALLA
            imgURL = $"data:{imgFile};base64,{Convert.ToBase64String(buffers)}";
        }


        //Desde ac se agrgaran los metodos para los botones de ediatr usuario
        protected async void Guardar()
        {
            //Validamos que el usuario ingreso datos
            if (string.IsNullOrWhiteSpace(user.CodigoUsuario) || string.IsNullOrWhiteSpace(user.Nombre) || 
                    string.IsNullOrWhiteSpace(user.Constrasena) || string.IsNullOrWhiteSpace(user.Rol) || user.Rol== "Seleccionar")
            {
                return;
            }
            //Si los datos esta llenos pasa a esta variable 
            bool edito = await usuarioServicio.ActualizarAsync(user);
            if (edito)
            {
                await Swal.FireAsync("FELICIDADES", "Usuario Actualizado", SweetAlertIcon.Success);
            }
            else
            {
                await Swal.FireAsync("ERROR", "Usuario No Actualizado", SweetAlertIcon.Error);
            }
        }
        protected async void Cancelar()
        {
            navigationManager.NavigateTo("/Usuarios");
        }
        protected async void Eliminar()
        {
            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = "¿Seguro que desea eliminar el usuario?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
                ConfirmButtonText = "Aceptar",
                CancelButtonText="Cancelar"
            }) ;

            if (!string.IsNullOrEmpty(result.Value))
            {
                bool elimino = await usuarioServicio.EliminarAsync(user.CodigoUsuario);//ELIMINAMOS EL REGISTRO
                if (elimino)
                {
                    await Swal.FireAsync("FELICIDADES", "Usuario eliminado", SweetAlertIcon.Success);
                    navigationManager.NavigateTo("/Usuarios");//ELIMINA EL REGISTRO Y LO MANDA DIRECTAMENTE A LA PARTE DE USUARIOS

                }
                else
                {
                    await Swal.FireAsync("ERROR", "Usuario No Eliminado", SweetAlertIcon.Error);
                }
            }
           
        }
    }
}
enum Roles//Los valores que se coloque aca se mostraran n el imputselect del "EditarUsuario.razor"
{
    Seleccionar, 
    Administrador,
    Usuario
}