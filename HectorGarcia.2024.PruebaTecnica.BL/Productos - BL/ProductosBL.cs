#region REFERENCIAS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Referencias Necesarias Para El Correcto Funcionamiento
using HectorGarcia._2024.PruebaTecnica.EN.Productos___EN;
using HectorGarcia._2024.PruebaTecnica.DAL.Productos___DAL;

#endregion

namespace HectorGarcia._2024.PruebaTecnica.BL.Productos___BL
{
    public class ProductosBL
    {
        #region METODO PARA CREAR
        // Metodo Para Guardar Un Nuevo Registro
        public async Task<int> CrearAsync(Productos productos)
        {
            return await ProductosDAL.CrearAsync(productos);
        }
        #endregion

        #region METODO PARA MODIFICAR
        // Metodo Para Guardar Un Nuevo Registro
        public async Task<int> ModificarAsync(Productos productos)
        {
            return await ProductosDAL.ModificarAsync(productos);
        }
        #endregion

        #region METODO PARA ELIMINAR
        // Metodo Para Eliminar Un Registro Existente En La Base De Datos
        public async Task<int> EliminarAsync(Productos productos)
        {
            return await ProductosDAL.EliminarAsync(productos);
        }
        #endregion

        #region METODO PARA MOSTRAR
        // Metodo Para Mostrar Una Lista De Registros
        public async Task<List<Productos>> ListarAsync()
        {
            return await ProductosDAL.ListarAsync();
        }
        #endregion

        #region METODO PARA MOSTRAR POR ID
        // Metodo Para Mostrar Un Registro Especifico Bajo Un Id
        public async Task<Productos> ObtenerPorIdAsync(Productos productos)
        {
            return await ProductosDAL.ObtenerPorIdAsync(productos);
        }
        #endregion

        #region METODO PARA BUSCAR
        // Metodo Para Buscar Registros Existentes
        public async Task<List<Productos>> BuscarAsync(Productos productos)
        {
            return await ProductosDAL.BuscarAsync(productos);
        }
        #endregion

        #region METODO PARA INCLUIR CATEGORIA
        public async Task<List<Productos>> BuscarIncludeAsync(Productos productos)
        {
            return await ProductosDAL.BuscarIncludeAsync(productos);
        }
        #endregion
    }
}
