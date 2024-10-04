namespace MStar.Model
{
    public class Movimentacao
    {
        public Movimentacao() { }

        public Movimentacao(int id, int quantidade, string localMovimentacao, string tipoMovimentacao) 
        {
            this.Id = id;
            this.Quantidade = quantidade;
            this.TipoMovimentacao = tipoMovimentacao;
            this.LocalMovimentacao = localMovimentacao;
            this.DataCriacao = DateTime.Now;
        } 


        public int Id { get; set; }
        public int Quantidade { get; set; }
        public string LocalMovimentacao { get; set; }
        public string TipoMovimentacao { get; set; }
        public DateTime DataCriacao { get; set; }


        //Foreign Key
        public Mercadoria Mercadoria { get; set; }
        public int IdMercadoria { get; set; }
    }
}
