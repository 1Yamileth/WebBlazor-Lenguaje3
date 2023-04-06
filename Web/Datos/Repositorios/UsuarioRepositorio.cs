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
    public class UsuarioRepositorio : IUsuarioRepositorio
    {//CADENA DE CONEXION
        private string CadenaConexion;//Recibira la cadena de conexion para conectarse a la BD.

        //COSNTRUCTOR
        public UsuarioRepositorio(string _cadenaconexion)
        {
            CadenaConexion = _cadenaconexion;
        }
        //Declaramos la conexion hacia MYSQL
        private MySqlConnection Conexion()
        {
            return new MySqlConnection(CadenaConexion);//Pasando la conexion a mysql
        }
        //METODOS 
        public async Task<bool> ActualizarAsync(UsuariosM usuario)
        {
            bool resultado = false;
            try
            {
                using MySqlConnection _conexion = Conexion();//Asignando el metodo de conexion
                await _conexion.OpenAsync();
                string sql = @"UPDATE  usuario SET Nombre= @Nombre, Contrasena=@Contrasena, Correo=@Correo, Rol= @Rol, Fotografia= @Fotografia, EstadoActivo= @EstadoActivo
                            WHERE CodigoUsuario= @CodigoUsuario ;";//EL @al inicio, nos permite poner la sentencia en variaas lineas.
                resultado = Convert.ToBoolean(await _conexion.ExecuteAsync(sql, usuario));

            }
            catch (Exception)
            {
            }
            return resultado;

        }

        public async Task<bool> EliminarAsync(string codigo)
        {
            bool eliminar = false;
            try
            {
                using MySqlConnection _conexion = Conexion();//Asignando el metodo de conexion
                await _conexion.OpenAsync();
                string sql = "DELETE FROM usuario WHERE CodigoUsuario= @CodigoUsuario ;";
                eliminar = Convert.ToBoolean(await _conexion.ExecuteAsync(sql, new {codigo}));

            }
            catch (Exception)
            {
            }
            return eliminar;
        }

        public async Task<IEnumerable<UsuariosM>> GetListaAsync()
        {
            IEnumerable<UsuariosM> lista = new List<UsuariosM>();//
            try
            {
                using MySqlConnection _conexion = Conexion();//Asignando el metodo de conexion
                await _conexion.OpenAsync();
                string sql = "SELECT * FROM usuario  ;";
                lista = await _conexion.QueryAsync<UsuariosM>(sql);//Devolvemos la lista de tipo "usuario"
            }
            catch (Exception)
            {
            }
            return lista;
        }

        public async Task<UsuariosM> GetPorCodigoAsync(string codigo)
        {
            UsuariosM user = new UsuariosM();
            try
            {
                using MySqlConnection _conexion = Conexion();//Asignando el metodo de conexion
                await _conexion.OpenAsync();
                string sql = "SELECT * FROM usuario WHERE CodigoUsuario= @CodigoUsuario ;";
                user = await _conexion.QueryFirstAsync<UsuariosM>(sql, new { codigo });//El query devuelve un solo resultado de tipo usurio

            }
            catch (Exception)
            {
            }
            return user;
        }

        public async Task<bool> NuevoAsync(UsuariosM usuario)
        {
            bool resultado = false;
            try
            {
                using MySqlConnection _conexion = Conexion();//Asignando el metodo de conexion
                await _conexion.OpenAsync();
                string sql = @"INSERT INTO usuario (Nombre, Contrasena, Correo, Rol, Fotografia, FechaCreacion, EstadoActivo)
                                VALUES (@Nombre, @Contrasena, @Correo, @Rol, @Fotografia, @FechaCreacion, @EstadoActivo);";
                resultado = Convert.ToBoolean(await _conexion.ExecuteAsync(sql, usuario));

            }
            catch (Exception)
            {
            }
            return resultado;
        }
    }
}
