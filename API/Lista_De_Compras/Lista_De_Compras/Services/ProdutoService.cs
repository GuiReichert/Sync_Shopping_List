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
            var categoria = db.Categorias.FirstOrDefault(x => x.Id == produto.CategoriaId);
            if (categoria == null) throw new Exception("essa categoria não existe");

            produto.Categoria = categoria;
            db.Produtos.Add(produto);
            await db.SaveChangesAsync();

            return produto;
        }

        public async Task DeleteProdutos(int id)
        {
            var produto = await db.Produtos.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (produto == null) throw new Exception("Não foi possível encontrar o produto.");
            db.Remove(produto);
            await db.SaveChangesAsync();
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

            var categoria = db.Categorias.FirstOrDefault(x => x.Id == produto.CategoriaId);
            if (categoria == null) throw new Exception("essa categoria não existe");


            produto_alterar.Categoria = categoria;
            produto_alterar.Selecionado = produto.Selecionado;

            await db.SaveChangesAsync();

            return produto_alterar;

        }


        public async Task<List<Categoria>> GetAllCategorias()
        {
            return await db.Categorias.ToListAsync();
        }

        public async Task<Categoria> AddCategoria(Categoria categoria)
        {
            await db.Categorias.AddAsync(categoria);
            await db.SaveChangesAsync();
            return categoria;
        }

        public async Task<Categoria> UpdateCategoria(Categoria categoria)
        {
            var categoria_alterar = await db.Categorias.FindAsync(categoria.Id);
            if (categoria_alterar == null) throw new Exception("nao foi possivel encontrar nenhum produto com este id");
            categoria.Nome = categoria.Nome;
            await db.SaveChangesAsync();

            return categoria;

        }

        public async Task DeleteCategoria(int id)
        {
            var categoria = await db.Categorias.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (categoria == null) throw new Exception("Não foi possível encontrar o produto.");
            db.Remove(categoria);
            await db.SaveChangesAsync();
        }
    }
}
