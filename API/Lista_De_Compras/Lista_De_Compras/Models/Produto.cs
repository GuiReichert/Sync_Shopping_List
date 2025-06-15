using System.Text.Json.Serialization;

namespace Lista_De_Compras.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        [JsonIgnore]
        public bool Selecionado { get; set; } = true;

        [JsonIgnore]
        public Categoria Categoria { get; set; } = new Categoria();
        public int CategoriaId { get; set; }

    }
}
