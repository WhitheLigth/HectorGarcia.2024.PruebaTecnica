#region REFERENCIAS
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Referencias Necesarias Para El Correcto Funcionamiento
using HectorGarcia._2024.PruebaTecnica.EN.Categorias___EN;
using HectorGarcia._2024.PruebaTecnica.DAL.Categorias___DAL;

#endregion

namespace HectorGarcia._2024.PruebaTecnica.BL.Categorias___BL
{
    public class CategoriasBL
    {
        #region METODO PARA GUARDAR
        // Metodo Para Guardar Un Nuevo Registro a La Base De Datos
        public async Task<int> CrearAsync(Categorias categorias)
        {
            return await CategoriasDAL.CrearAsync(categorias);
        }
        #endregion

        #region METODO PARA MODIFICAR
        // Metodo Para Modificar Un Registro Existente En La Base De Datos
        public async Task<int> ModificarAsync(Categorias categorias)
        {
            return await CategoriasDAL.ModificarAsync(categorias);
        }
        #endregion

        #region METODO PARA ELIMINAR
        // Metodo Para Eliminar Un Registro Existente En La Base De Datos
        public async Task<int> EliminarAsync(Categorias categorias)
        {
            return await CategoriasDAL.EliminarAsync(categorias);
        }
        #endregion

        #region METODO PARA MOSTRAR TODOS
        // Metodo Para Listar y Mostrar Todos Los Resultados
        public async Task<List<Categorias>> ListarAsync()
        {
            return await CategoriasDAL.ListarAsync();
        }
        #endregion

        #region METODO PARA BUSCAR
        // Metodo Para Buscar Registro Existentes En La Base De Datos
        public async Task<List<Categorias>> BuscarAsync(Categorias categorias)
        {
            return await CategoriasDAL.BuscarAsync(categorias);
        }
        #endregion
    }
}
