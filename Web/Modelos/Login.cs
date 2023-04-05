using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Login
    {

        public string CodigoUsuario { get; set; }
        public string Contrasena { get; set; }

        public Login()
        {
        }

        public Login(string codigoUsuario, string contrasena)
        {
            CodigoUsuario = codigoUsuario;
            Contrasena = contrasena;
        }
    }
}
