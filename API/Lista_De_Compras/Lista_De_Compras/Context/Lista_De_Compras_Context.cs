using Lista_De_Compras.Models;
using Microsoft.EntityFrameworkCore;

namespace Lista_De_Compras.Context
{
    public class Lista_De_Compras_Context : DbContext
    {
        public Lista_De_Compras_Context(DbContextOptions<Lista_De_Compras_Context> options) : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}
