#region REFERENCIAS
// Referencias Necesarias Para El Correcto Funcionamiento
using HectorGarcia._2024.PruebaTecnica.BL.Categorias___BL;
using HectorGarcia._2024.PruebaTecnica.EN.Categorias___EN;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace HectorGarcia._2024.PruebaTecnica.WebApp.Controllers.Categorias___Controller
{
    public class CategoriasController : Controller
    {
        // Instancia de la clase logica de negocio
        CategoriasBL categoriasBL = new CategoriasBL();

        #region METODO PARA INDEX
        // Metodo Para Mostrar La Vista Index
        public async Task<IActionResult> Index(Categorias categorias = null!)
        {
            if (categorias == null)
                categorias = new Categorias();

            var categoriass = await categoriasBL.BuscarAsync(categorias);
            return View(categoriass);
        }
        #endregion

        #region METODO PARA GUARDAR
        // Metodo Para Mostrar La Vista Guardar
        public IActionResult Crear()
        {
            ViewBag.Error = "";
            return View();
        }

        // Metodo Que Recibe y Envia a La Base De Datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Categorias categorias)
        {
            try
            {
                int result = await categoriasBL.CrearAsync(categorias);
                TempData["SuccessMessageCreate"] = "Categoria Agregada Exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(categorias);
            }
        }
        #endregion

        #region METODO PARA MODIFICAR
        // Metodo Para Mostrar La Vista De Modificar
        public async Task<IActionResult> Edit(int id)
        {
            var categorias = await categoriasBL.ObtenerPorIdAsync(new Categorias { Id = id });
            ViewBag.Error = "";
            return View(categorias);
        }

        // Metodo Que Recibe y Envia a La Base De Datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Categorias categorias)
        {
            try
            {
                int result = await categoriasBL.ModificarAsync(categorias);
                TempData["SuccessMessageUpdate"] = "Categoria Modificada Exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(categorias);
            }
        }
        #endregion

        #region METODO PARA ELIMINAR
        // Metodo Para Mostrar La Vista De Eliminar
        public async Task<IActionResult> Delete(int id)
        {
            var categorias = await categoriasBL.ObtenerPorIdAsync(new Categorias { Id = id });
            ViewBag.Error = "";
            return View(categorias);
        }

        // Metodo Que Recibe y Envia a La Base De Datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Categorias categorias)
        {
            try
            {
                int result = await categoriasBL.EliminarAsync(categorias);
                TempData["SuccessMessageDelete"] = "Categoria Eliminada Exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(categorias);
            }
        }
        #endregion
    }
}
