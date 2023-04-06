using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class LoginM
    {

        public string CodigoUsuario { get; set; }
        public string Contrasena { get; set; }

        public LoginM()
        {
        }

        public LoginM(string codigoUsuario, string contrasena)
        {
            CodigoUsuario = codigoUsuario;
            Contrasena = contrasena;
        }
    }
}
