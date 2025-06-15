using Lista_De_Compras.Models;

namespace Lista_De_Compras.Services
{
    public interface IProdutoService
    {
        public Task<List<Produto>> GetAllProdutos();
        public Task<Produto> AddProdutos(Produto produto);
        public Task<Produto> UpdateProdutos(Produto produto);
        public Task DeleteProdutos(int id);


        public Task<List<Categoria>> GetAllCategorias();
        public Task<Categoria> AddCategoria(Categoria categoria);
        public Task<Categoria> UpdateCategoria(Categoria categoria);
        public Task DeleteCategoria(int id);



        public Task SincronizarAlteracoes();


    }
}
