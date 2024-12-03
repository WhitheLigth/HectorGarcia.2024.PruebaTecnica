#region REFERENCIAS
// Referencias Necesarias Para El Correcto Funcionamiento
using HectorGarcia._2024.PruebaTecnica.BL.Categorias___BL;
using HectorGarcia._2024.PruebaTecnica.BL.Productos___BL;
using HectorGarcia._2024.PruebaTecnica.EN.Categorias___EN;
using HectorGarcia._2024.PruebaTecnica.EN.Productos___EN;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace HectorGarcia._2024.PruebaTecnica.WebApp.Controllers.Productos___Controller
{
    public class ProductosController : Controller
    {
        // Instancia de la clase logica de negocio
        ProductosBL productosBL = new ProductosBL();
        CategoriasBL categoriasBL = new CategoriasBL();

        #region METODO PARA MOSTRAR INDEX
        // Accion Para Mostrar La Vista Index
        public async Task<IActionResult> Index(Productos productos = null!)
        {
            if (productos == null)
                productos = new Productos();

            var productoss = await productosBL.BuscarIncludeAsync(productos);
            var categorias = await categoriasBL.ListarAsync();

            ViewBag.Categorias = categorias;
            return View(productoss);
        }
        #endregion

        #region METODO PARA CREAR
        // Accion Para Mostrar La Vista De Crear
        public async Task<IActionResult> Crear()
        {
            var categorias = await categoriasBL.ListarAsync();
            ViewBag.Categorias = categorias;
            ViewBag.Error = "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Productos productos)
        {
            try
            {
                int result = await productosBL.CrearAsync(productos);
                TempData["SuccessMessageCreate"] = "Producto Agregado Exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                ViewBag.Categorias = await categoriasBL.ListarAsync();
                return View(productos);
            }
        }
        #endregion

        #region METODO PARA MODIFICAR
        // Accion Que Muestra El Formulario
        public async Task<IActionResult> Modificar(int id)
        {
            var productos = await productosBL.ObtenerPorIdAsync(new Productos { Id = id });
            productos.Categorias = await categoriasBL.ObtenerPorIdAsync(new Categorias { Id = productos.Id });
            ViewBag.Categorias = await categoriasBL.ListarAsync();
            return View(productos);
        }

        // Accion Que Recibe Los Datos y Los Envia a La Base De Datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modificar(int id, Productos productos)
        {
            try
            {
                int result = await productosBL.ModificarAsync(productos);
                TempData["SuccessMessageUpdate"] = "Producto Modificado Exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                ViewBag.Categorias = await categoriasBL.ListarAsync();
                return View(productos);
            }
        }
        #endregion

        #region METODO PARA ELIMINAR
        // Accion Que Muestra El Formulario
        public async Task<IActionResult> Eliminar(int id)
        {
            var productos = await productosBL.ObtenerPorIdAsync(new Productos { Id = id });
            productos.Categorias = await categoriasBL.ObtenerPorIdAsync(new Categorias { Id = productos.Id });
            return View(productos);
        }

        // Accion Que Recibe Los Datos y Los Envia a La Base De Datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id, Productos productos)
        {
            try
            {
                int result = await productosBL.EliminarAsync(productos);
                TempData["SuccessMessageDelete"] = "Producto Eliminado Exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                var productosDb = await productosBL.ObtenerPorIdAsync(productos);
                if (productosDb == null)
                    productosDb = new Productos();
                if (productosDb.Id > 0)
                    productosDb.Categorias = await categoriasBL.ObtenerPorIdAsync(new Categorias { Id = productosDb.IdCategoria });
                return View(productosDb);
            }
        }
        #endregion
    }
}
