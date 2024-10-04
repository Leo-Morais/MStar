namespace MStar.DTO
{

    public class MercadoriaDTO
    {
        public string Nome { get; set; }
        public string Fabricante { get; set; }
        public string Descricao { get; set; }
        public int TipoMercadoriaId { get; set; }
    }

    public class MercadoriaEstoqueDTO : MercadoriaDTO
    {
        public int QuantidadeInicial { get; set; }

    }
}
