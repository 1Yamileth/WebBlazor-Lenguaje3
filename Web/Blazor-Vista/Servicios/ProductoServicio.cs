﻿using Blazor_Vista.Interfaces;
using Datos.Interfaces;
using Datos.Repositorios;
using Modelos;

namespace Blazor_Vista.Servicios
{
    public class ProductoServicio : IProductoServicio
    {
        private readonly Config _config;
        private IProductoRepositorio productoRepositorio;
        //constructor para acceder a la cadena
        private ProductoServicio(Config config)
        {//Pasamos la cadena de conexion al repositorio
            _config = config;
            productoRepositorio = new ProductoRepositorio(config.CadenaConexion);
        }



        public async Task<bool> ActualizarAsync(ProductosM producto)
        {
            return await productoRepositorio.ActualizarAsync(producto);
       }

        public async Task<bool> EliminarAsync(string CodigoProducto)
        {
            return await productoRepositorio.EliminarAsync(CodigoProducto);
        }

        public async Task<IEnumerable<ProductosM>> GetListaAsync()
        {
            return await productoRepositorio.GetListaAsync();
        }

        public async Task<ProductosM> GetPorCodigoAsync(string CodigoProducto)
        {
            return await productoRepositorio.GetPorCodigoAsync(CodigoProducto);
        }

        public async Task<bool> NuevoAsync(ProductosM producto)
        {
            return await productoRepositorio.NuevoAsync(producto);
        }
    }
}
