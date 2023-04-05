using Dapper;
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
        public LoginRepositorio (string _cadenaconexion)//Le pasamos la cadena desde el proyecto blazor, por medio de este constructor
        {
            CadenaConexion = _cadenaconexion;
        }
        //Declaramos la conexion hacia MYSQL
        private MySqlConnection Conexion()
        {
            return new MySqlConnection(CadenaConexion);//Pasando la conexion a mysql
        }


        public async Task<bool> ValidarUsuarioAsync(Login login)
        {
            bool valido = false;
            try
            {
                using MySqlConnection _conexion = Conexion();//Asignando el metodo de conexion
                await _conexion.OpenAsync();//Abriendo la conexion assincrona
                //SENTENCIA SQUL=En vez del stringbuilder usamos una variables normal
                string sql = "SELECT 1 FROM usuario WHERE CodigoUsuario= @CodigoUsuario AND Contrasena = @Contrasena;";/*Lanza un 1 si encuentra alguna coincidencia en la tabla de usuario.
                                                                                                                        Como usamos dapper, los campos tiene que llamarse estrictametne iguaal, ya que sino no encontrara
                                                                                                                        coincidencia y lanzara error*/
                valido = await _conexion.ExecuteScalarAsync<bool>(sql, login  );/*Los parametros pueden ir solo con el objeto de la case o de la siguiente manera: new {login.CodigoUsuario, login.Contrasena}*/
                
            }
            catch (Exception )
            {

            }
            return valido;
        }
    }
}
