using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class ProductosM
    {
        [Required(ErrorMessage = "EL CODIGO DE USUARIO ES REQUERIDO OBLIGATORIAMENTE")]
        public string CodigoProducto{ get; set; }
        [Required(ErrorMessage = "EL CODIGO DE USUARIO ES REQUERIDO OBLIGATORIAMENTE")]
        public string Descripcion { get; set; }
        public string Existencia { get; set; }
        public string Precio { get; set; }
        public byte[] Foto { get; set; }
        public bool EstaActivo { get; set; }

        public ProductosM()
        {
        }

        public ProductosM(string codigoProducto, string descripcion, string existencia, string precio, byte[] foto, bool estaActivo)
        {
            CodigoProducto = codigoProducto;
            Descripcion = descripcion;
            Existencia = existencia;
            Precio = precio;
            Foto = foto;
            EstaActivo = estaActivo;
        }
    }
}
