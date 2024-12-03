#region REFERENCIAS
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Referencias Necesarias Para El Correcto Funcionamiento
using HectorGarcia._2024.PruebaTecnica.EN.Categorias___EN;
using Microsoft.EntityFrameworkCore;

#endregion

namespace HectorGarcia._2024.PruebaTecnica.DAL.Categorias___DAL
{
    public class CategoriasDAL
    {
        #region METODO PARA VALIDAR UNICA EXISTENCIA DEL REGISTRO
        // Metodo Para Validar La Unica Existencia De Un Registro y No Haber Duplicidads
        private static async Task<bool> CategoriaExistente(Categorias categorias, ContextDB dbContext)
        {
            bool result = false;
            var categoriass = await dbContext.Categorias.FirstOrDefaultAsync(p => p.Nombre == categorias.Nombre && p.Id != categorias.Id);
            if (categoriass != null && categoriass.Id > 0 && categoriass.Nombre == categorias.Nombre)
                result = true;

            return result;
        }
        #endregion

        #region METODO PARA CREAR
        // Metodo Para Guardar Un Nuevo Registro En La Base De Datos
        public static async Task<int> CrearAsync(Categorias categorias)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                bool CategoriasExists = await CategoriaExistente(categorias, dbContext);
                if (CategoriasExists == false)
                {
                    dbContext.Categorias.Add(categorias);
                    result = await dbContext.SaveChangesAsync(); // Await sirve para esperar a terminar todos los procesos para devolverlos todos juntos
                }
                else
                    throw new Exception("Categoria Ya Existente, Vuelve a Intentarlo");
            }
            return result;  // Si se realizo con exito devuelve 1 sino devuelve 0
        }
        #endregion

        #region METODO PARA MODIFICAR
        // Metodo Para Modificar Un Registro Existente En La Base De Datos
        public static async Task<int> ModificarAsync(Categorias categorias)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                // Obtiene el primer registro con el Id específico desde la base de datos.
                var categoriasDB = await dbContext.Categorias.FirstOrDefaultAsync(c => c.Id == categorias.Id);
                // Validamos que sea diferente de NULL
                if (categoriasDB != null)
                {
                    bool categoriasExists = await CategoriaExistente(categorias, dbContext);
                    if (categoriasExists == false)
                    {
                        categoriasDB.Nombre = categorias.Nombre;

                        dbContext.Update(categoriasDB);
                        result = await dbContext.SaveChangesAsync();
                    }
                    else
                        throw new Exception("Categoria Ya Existente, Vuelve a Intentarlo");
                }
            }
            return result;  // Si se realizo con exito devuelve 1 sino devuelve 0
        }
        #endregion

        #region METODO PARA ELIMINAR
        // Metodo Para Eliminar Un Registro Existente En La Base De Datos
        public static async Task<int> EliminarAsync(Categorias categorias)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                // Obtiene el primer registro con el Id específico de desde la base de datos.
                var categoriasDB = await dbContext.Categorias.FirstOrDefaultAsync(c => c.Id == categorias.Id);
                // Validamos que sea diferente de NULL
                if (categoriasDB != null)
                {
                    dbContext.Categorias.Remove(categoriasDB);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            return result;  // Si se realizo con exito devuelve 1 sino devuelve 0
        }
        #endregion

        #region METODO PARA MOSTRAR
        // Metodo Para Mostrar La Lista De Registros
        public static async Task<List<Categorias>> ListarAsync()
        {
            var categorias = new List<Categorias>();
            using (var dbContext = new ContextDB())
            {
                categorias = await dbContext.Categorias.ToListAsync();
            }
            return categorias;
        }
        #endregion

        #region METODO PARA OBTENER POR ID
        // Metodo Para Obtener Un Registro Por Su Id
        public static async Task<Categorias> ObtenerPorIdAsync(Categorias categorias)
        {
            var categoriasDb = new Categorias();
            using (var dbContext = new ContextDB())
            {
                categoriasDb = await dbContext.Categorias.FirstOrDefaultAsync(r => r.Id == categorias.Id);
            }
            return categoriasDb!;
        }
        #endregion

        #region METODO PARA BUSCAR REGISTROS MEDIANTE EL USO DE FILTROS (Por Nombre)
        // Metodo Para Buscar Por Filtros
        // IQueryable es una interfaz que toma un coleccion a la cual se le pueden implementar multiples consultas (Filtros)
        internal static IQueryable<Categorias> QuerySelect(IQueryable<Categorias> query, Categorias categorias)
        {
            // Por ID
            if (categorias.Id > 0)
                query = query.Where(c => c.Id == categorias.Id);

            // Por Nomnbre, Si es verdadero lo vuelve falso y viceversa 
            if (!string.IsNullOrWhiteSpace(categorias.Nombre))
                query = query.Where(c => c.Nombre.Contains(categorias.Nombre));

            // Ordenamos de Manera Desendente
            query = query.OrderByDescending(c => c.Id).AsQueryable();

            return query;
        }
        #endregion

        #region METODO PARA BUSCAR
        // Metodo para Buscar Registros Existentes
        public static async Task<List<Categorias>> BuscarAsync(Categorias categorias)
        {
            var categoriass = new List<Categorias>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.Categorias.AsQueryable();
                select = QuerySelect(select, categorias);
                categoriass = await select.ToListAsync();
            }
            return categoriass;
        }
        #endregion
    }
}
