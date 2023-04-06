using Blazor_Vista.Interfaces;
using Microsoft.AspNetCore.Components;
using Modelos;

namespace Blazor_Vista.Pages.MisUsuarios
{//ACA IRA EL CODIGO DE C#  Y EN EL COMPONENTE DE  "USUARIOS.RAZOR" IRA EL CODIGO DE HTML
    public partial class Usuarios //Con partial lo que hacemos es tener la misma clase en varios archivos, asi podemos dividir una clase sin ener probelmas y tener ordenado el codigo
    {
        [Inject] private IUsuarioServicio usuarioServicio { get; set; }//INYECCION DE DEPENDENCIA. declaramos el servicio en la clase progrm y la inyectamos aca
        //Con eston podemos ahorrarnos codigo y evitar la intanciacion seguida]

        //La usamos para 
        private IEnumerable<UsuariosM> lista { get; set; }


        //ERRORERRRRRR
        protected override async Task OnInitializedAsync()//Sobreescribe al inicio
        {
            lista = await usuarioServicio.GetListaAsync();//Con esto tenemos la lista de usuarios en esta clase(es como traer una lista en LOAD)
        }

    }
}
