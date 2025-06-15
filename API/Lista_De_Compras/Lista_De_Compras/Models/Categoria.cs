using System.Collections.Generic;

namespace Lista_De_Compras.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public List<Produto> Produtos { get; set; }
    }
}
