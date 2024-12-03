#region REFERENCIAS
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Referencias Necesarias Para El Correcto Funcionamiento
using Microsoft.EntityFrameworkCore;
using HectorGarcia._2024.PruebaTecnica.EN.Categorias___EN;
using HectorGarcia._2024.PruebaTecnica.EN.Productos___EN;

#endregion

namespace HectorGarcia._2024.PruebaTecnica.DAL
{
    public class ContextDB : DbContext
    {
        #region REFERENCIAS DE TABLAS DE LA DB
        //Coleccion que hace referencia a la tabla de la base de datos
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Productos> Productos { get; set; }
        #endregion

        #region STRING DE CONEXION
        // Metodo de Conexion a la Base de Datos
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@""); // String de Conexion
        }
        #endregion
    }
}
