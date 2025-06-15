using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Lista_De_Compras.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        [JsonIgnore]
        public List<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
