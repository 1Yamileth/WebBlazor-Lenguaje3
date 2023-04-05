using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelos;

namespace Datos.Interfaces
{//Solo desclaramos los metodos, nada de ingresar codigo
    //Asi los podemos usar en las clases 
    public interface ILoginRepositorio
    {
        Task<bool> ValidarUsuario(Login login);
    }
}
