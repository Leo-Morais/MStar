using System.ComponentModel.DataAnnotations.Schema;

namespace MStar.Model
{
    public class Mercadoria
    {
        public Mercadoria() { }

        public Mercadoria(int id, string nome, string fabricante, string tipoMercadoria, string descricao) 
        {
            this.Id = id;
            this.Nome = nome;
            this.Fabricante = fabricante;
            this.TipoMercadoria = tipoMercadoria;
            this.Descricao = descricao;
            this.DataCriacao = DateTime.Now;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Fabricante { get; set; }
        public string TipoMercadoria { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
