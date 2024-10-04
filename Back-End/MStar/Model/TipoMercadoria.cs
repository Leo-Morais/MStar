namespace MStar.Model
{
    public class TipoMercadoria
    {
        public TipoMercadoria() { }

        public TipoMercadoria(string tipo) 
        { 
            this.Tipo = tipo;
        }

        public int Id { get; set; }
        public string Tipo { get; set; }
    }
}
