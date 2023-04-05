using Datos.Interfaces;
using Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class LoginRepositorio : ILoginRepositorio/*Estamos heredadnod de la interfaz login repositorio*/
    {/*Al momento de heredarlo, nos lanza de un solo los metodos declarados en esa interfaz, solo falta codificar ese metodo aca*/
        private string CadenaConexion;//Recibira la cadena de conexion para conectarse a la BD.
       
        //COSNTRUCTOR
        public LoginRepositorio (string cadenaconexion)//Le pasamos la cadena desde el proyecto blazor, por medio de este constructor
        {
            CadenaConexion = cadenaconexion;
        }
        //Declaramos la conexion hacia MYSQL
        private MySqlConnection Conexion()
        {
            return new MySqlConnection(CadenaConexion);//Pasando la conexion a mysql
        }


        public Task<bool> ValidarUsuario(Login login)
        {
            bool valido = false;
            try
            {
                using MySqlConnection _conexion = Conexion();
            }
            catch (Exception )
            {

            }
        }
    }
}
