#region REFERENCIAS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HectorGarcia._2024.PruebaTecnica.EN.Categorias___EN;

// Referencias Necesarias Para El Correcto Funcionamiento
using HectorGarcia._2024.PruebaTecnica.EN.Productos___EN;
using Microsoft.EntityFrameworkCore;

#endregion

namespace HectorGarcia._2024.PruebaTecnica.DAL.Productos___DAL
{
    public class ProductosDAL
    {
        #region METODO PARA VALIDAR UNICA EXISTENCIA DEL REGISTRO
        // Metodo Para Validar La Unica Existencia De Un Registro y No Haber Duplicidad
        private static async Task<bool> ProductoExistente(Productos productos, ContextDB contextDB)
        {
            bool result = false;
            var productoss = await contextDB.Productos.FirstOrDefaultAsync(c => c.Id == productos.Id && c.Nombre != productos.Nombre);
            if (productoss != null && productoss.Id > 0 && productoss.Nombre == productos.Nombre)
                result = true;
            return result;
        }
        #endregion

        #region METODO PARA CREAR
        // Metodo Para Guardar Un Nuevo Registro En La Base De Datos
        public static async Task<int> CrearAsync(Productos productos)
        {
            int result = 0;
            // Un bloque de conexion que mientras se permanezca en el bloque la base de datos permanecera abierta y al terminar se destruira
            using (var dbContext = new ContextDB())
            {
                bool productosExists = await ProductoExistente(productos, dbContext);
                if (productosExists == false)
                {
                    dbContext.Productos.Add(productos);
                    result = await dbContext.SaveChangesAsync();
                }
                else
                    throw new Exception("Producto Ya Existente, Vuelve a Intentarlo Nuevamente");
            }
            return result;
        }
        #endregion

        #region METODO PARA MODIFICAR
        // Metodo Para Modificar Un Registro Existente De La Base De Datos
        public static async Task<int> ModificarAsync(Productos productos)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var productosDB = await dbContext.Productos.FirstOrDefaultAsync(c => c.Id == productos.Id);
                if (productosDB != null)
                {
                    bool productosExists = await ProductoExistente(productos, dbContext);
                    if (productosExists == false)
                    {
                        productosDB.Nombre = productos.Nombre;
                        productosDB.Precio = productos.Precio;
                        productosDB.IdCategoria = productos.IdCategoria;

                        dbContext.Update(productosDB);
                        result = await dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("Producto Ya Existente, Vuelve a Intentarlo Nuevamente");
                    }
                }
                else
                {
                    throw new Exception("Producto No Encontrado Para Actualizar.");
                }
            }
            return result;
        }
        #endregion

        #region METODO PARA ELIMINAR
        // Metodo Para Eliminar Un Registro Existente En La Base De Datos
        public static async Task<int> EliminarAsync(Productos productos)
        {
            int result = 0;
            // Un bloque de conexion que mientras se permanezca en el bloque la base de datos permanecera abierta y al terminar se destruira
            using (var dbContext = new ContextDB())
            {
                var productosDB = await dbContext.Productos.FirstOrDefaultAsync(c => c.Id == productos.Id);
                if (productosDB != null)
                {
                    dbContext.Productos.Remove(productosDB);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            return result;
        }
        #endregion

        #region METODO PARA MOSTRAR
        // Metodo Para Mostrar La Lista De Registros
        public static async Task<List<Productos>> ListarAsync()
        {
            var productos = new List<Productos>();
            using (var dbContext = new ContextDB())
            {
                productos = await dbContext.Productos.ToListAsync();
            }
            return productos;
        }
        #endregion

        #region METODO PARA OBTENER POR ID
        // Metodo Para Mostrar Un Registro En Base A Su Id
        public static async Task<Productos> GetByIdAsync(Productos productos)
        {
            var productosDB = new Productos();
            using (var dbContext = new ContextDB())
            {
                productosDB = await dbContext.Productos.Include(m => m.Categorias).FirstOrDefaultAsync(c => c.Id == productos.Id);
            }
            return productosDB!;
        }
        #endregion

        #region METODO PARA BUSCAR REGISTROS MEDIANTE EL USO DE FILTROS
        // Metodo Para Buscar Por Filtros
        // IQueryable es una interfaz que toma un coleccion a la cual se le pueden implementar multiples consultas (Filtros)
        internal static IQueryable<Productos> QuerySelect(IQueryable<Productos> query, Productos productos)
        {
            // Por ID
            if (productos.Id > 0)
                query = query.Where(c => c.Id == productos.Id);

            // Por Nombre
            if (!string.IsNullOrWhiteSpace(productos.Nombre))
                query = query.Where(c => c.Nombre.Contains(productos.Nombre));

            // Por Categorias
            if (productos.IdCategoria > 0)
                query = query.Where(c => c.IdCategoria == productos.IdCategoria);

            // Ordenamos de Manera Desendente
            query = query.OrderByDescending(c => c.Id).AsQueryable();

            return query;
        }
        #endregion

        #region METODO PARA BUSCAR
        // Metodo para Buscar Registros Existentes
        public static async Task<List<Productos>> BuscarAsync(Productos productos)
        {
            var productoss = new List<Productos>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.Productos.AsQueryable();
                select = QuerySelect(select, productos);
                productoss = await select.ToListAsync();
            }
            return productoss;
        }
        #endregion

        #region METODO PARA INCLUIR CATEGORIAS
        // Método que incluye categorias para la búsqueda
        public static async Task<List<Productos>> BuscarIncludeAsync(Productos productos)
        {
            var productoss = new List<Productos>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.Productos.AsQueryable();
                select = QuerySelect(select, productos).Include(c => c.Categorias).AsQueryable();
                productoss = await select.ToListAsync();
            }
            return productoss;
        }
        #endregion
    }
}
