using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class UsuariosM
    {
        [Required(ErrorMessage ="EL CODIGO DE USUARIO ES REQUERIDO OBLIGATORIAMENTE")]//Con esta propiedad estamos diciendo que el codigo es obligatoria
        public string  CodigoUsuario { get; set; }
        [Required(ErrorMessage = "EL CODIGO DE USUARIO ES REQUERIDO OBLIGATORIAMENTE")]
        public string Nombre { get; set; }
        public string Constrasena{ get; set; }
        public string Correo  { get; set; }
        [Required(ErrorMessage = "EL CODIGO DE USUARIO ES REQUERIDO OBLIGATORIAMENTE")]
        public string Rol { get; set; }
        public byte[] Fotografia { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool EstadoActivo { get; set; }

        public UsuariosM()
        {
        }

        public UsuariosM(string codigoUsuario, string nombre, string constrasena, string correo, string rol, byte[] fotografia, DateTime fechaCreacion, bool estadoActivo)
        {
            CodigoUsuario = codigoUsuario;
            Nombre = nombre;
            Constrasena = constrasena;
            Correo = correo;
            Rol = rol;
            Fotografia = fotografia;
            FechaCreacion = fechaCreacion;
            EstadoActivo = estadoActivo;
        }
    }
}
