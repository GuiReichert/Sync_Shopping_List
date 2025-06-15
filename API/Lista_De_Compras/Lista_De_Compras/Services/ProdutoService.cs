using Lista_De_Compras.Context;
using Lista_De_Compras.Models;
using Microsoft.EntityFrameworkCore;

namespace Lista_De_Compras.Services
{
    public class ProdutoService : IProdutoService
    {
        private Lista_De_Compras_Context db;

        public ProdutoService(Lista_De_Compras_Context db) 
        {
            this.db = db;
        }

        public async Task<Produto> AddProdutos(Produto produto)
        {
            await db.Produtos.AddAsync(produto);
            return produto;
        }

        public async Task DeleteProdutos(int id)
        {
            var produto = await db.Produtos.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (produto == null) throw new Exception("Não foi possível encontrar o produto.");
            db.Remove(produto);
        }

        public async Task<List<Produto>> GetAllProdutos()
        {
            return await db.Produtos.ToListAsync();
        }

        public async Task<Produto> UpdateProdutos(Produto produto)
        {
            var produto_alterar = await db.Produtos.FindAsync(produto.Id);
            if (produto_alterar == null) throw new Exception("nao foi possivel encontrar nenhum produto com este id");
            produto_alterar.Nome = produto.Nome;
            produto_alterar.Categoria = produto.Categoria;
            produto_alterar.Selecionado = produto.Selecionado;

            return produto_alterar;

        }


        public async Task<List<Categoria>> GetAllCategorias()
        {
            return await db.Categorias.ToListAsync();
        }

        public async Task<Categoria> AddCategoria(Categoria categoria)
        {
            await db.Categorias.AddAsync(categoria);
            return categoria;
        }

        public async Task<Categoria> UpdateCategoria(Categoria categoria)
        {
            var categoria_alterar = await db.Categorias.FindAsync(categoria.Id);
            if (categoria_alterar == null) throw new Exception("nao foi possivel encontrar nenhum produto com este id");
            categoria.Nome = categoria.Nome;

            return categoria;

        }

        public async Task DeleteCategoria(int id)
        {
            var categoria = await db.Categorias.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (categoria == null) throw new Exception("Não foi possível encontrar o produto.");
            db.Remove(categoria);
        }





        public async Task SincronizarAlteracoes()
        {
            await db.SaveChangesAsync();
        }
    }
}
