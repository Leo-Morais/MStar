namespace MStar.Model
{
    public class Estoque
    {
        public Estoque() { }

        public Estoque(int quantidade) 
        {
            this.Quantidade = quantidade;
            this.DataAtualizacao = DateTime.Now;
        }
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataAtualizacao { get; set; }

        //Foreign Key
        public Mercadoria Mercadoria { get; set; }
        public int IdMercadoria { get; set; }
    }
}
