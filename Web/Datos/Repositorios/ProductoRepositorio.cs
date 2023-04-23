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
    public class ProductoRepositorio : IProductoRepositorio
    {//CADENA DE CONEXION
        private string CadenaConexion;//Recibira la cadena de conexion para conectarse a la BD.

        //COSNTRUCTOR
        public ProductoRepositorio(string _cadenaconexion)
        {
            CadenaConexion = _cadenaconexion;
        }
        //Declaramos la conexion hacia MYSQL
        private MySqlConnection Conexion()
        {
            return new MySqlConnection(CadenaConexion);//Pasando la conexion a mysql
        }

        public async Task<bool> ActualizarAsync(ProductosM producto)
        {
            bool resultado = false;
            try
            {
                using MySqlConnection _conexion = Conexion();//Asignando el metodo de conexion
                await _conexion.OpenAsync();
                string sql = @"UPDATE  producto SET Descripcion= @Descripcion, Existencia=@Existencia, Precio=@Precio, 
                             Foto= @Foto, EstaActivo= @EstaActivo WHERE CodigoProducto= @CodigoProducto ;";//EL @al inicio, nos permite poner la sentencia en variaas lineas.
                resultado = Convert.ToBoolean(await _conexion.ExecuteAsync(sql, producto));

            }
            catch (Exception)
            {
            }
            return resultado;
        }

        public async Task<bool> EliminarAsync(string CodigoProducto)
        {
            bool eliminar = false;
            try
            {
                using MySqlConnection _conexion = Conexion();//Asignando el metodo de conexion
                await _conexion.OpenAsync();
                string sql = "DELETE FROM producto WHERE CodigoProducto= @CodigoProducto ;";
                eliminar = Convert.ToBoolean(await _conexion.ExecuteAsync(sql, new { CodigoProducto }));

            }
            catch (Exception)
            {
            }
            return eliminar;
        }

        public async  Task<IEnumerable<ProductosM>> GetListaAsync()
        {
            IEnumerable<ProductosM> lista = new List<ProductosM>();
            try
            {
                using MySqlConnection _conexion = Conexion();//Asignando el metodo de conexion
                await _conexion.OpenAsync();
                string sql = "SELECT * FROM producto  ;";
                lista = await _conexion.QueryAsync<ProductosM>(sql);//Devolvemos la lista de tipo "usuario"
            }
            catch (Exception)
            {
            }
            return lista;
        }

        public async Task<ProductosM> GetPorCodigoAsync(string CodigoProducto)
        {
            ProductosM prod = new ProductosM();
            try
            {
                using MySqlConnection _conexion = Conexion();//Asignando el metodo de conexion
                await _conexion.OpenAsync();
                string sql = "SELECT * FROM producto WHERE CodigoProducto= @CodigoProducto ;";
                prod = await _conexion.QueryFirstAsync<ProductosM>(sql, new { CodigoProducto });//El query devuelve un solo resultado de tipo usurio

            }
            catch (Exception)
            {
            }
            return prod;
        }

        public async Task<bool> NuevoAsync(ProductosM producto)
        {
            bool resultado = false;
            try
            {
                using MySqlConnection _conexion = Conexion();//Asignando el metodo de conexion
                await _conexion.OpenAsync();
                string sql = @"INSERT INTO producto (CodigoProducto, Descripcion, Existencia, Precio, Foto, EstaActivo)
                                VALUES (@CodigoProducto, @Descripcion, @Existencia, @Precio, @Foto, @EstaActivo);";
                resultado = Convert.ToBoolean(await _conexion.ExecuteAsync(sql, producto));

            }
            catch (Exception)
            {
            }
            return resultado;
        }
    }
}
