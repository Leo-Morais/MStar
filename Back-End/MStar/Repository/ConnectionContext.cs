using Microsoft.EntityFrameworkCore;
using MStar.Model;

namespace MStar.Repository
{
    public class ConnectionContext : DbContext
    {
        public ConnectionContext(DbContextOptions<ConnectionContext> options) : base(options)
        {

        }

        public DbSet<Mercadoria> Mercadoria { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Mapeamento de Mercadoria
            modelBuilder.Entity<Mercadoria>().HasKey(k => k.Id);
            modelBuilder.Entity<Mercadoria>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Mercadoria>().ToTable("Mercadorias");
            modelBuilder.Entity<Mercadoria>().Property(x => x.Fabricante).HasColumnName("Fabricante");
            modelBuilder.Entity<Mercadoria>().Property(x => x.Descricao).HasColumnName("Descricao");
            modelBuilder.Entity<Mercadoria>().Property(x => x.Nome).HasColumnName("Nome");
            modelBuilder.Entity<Mercadoria>().Property(x => x.DataCriacao).HasColumnName("DataCriacao");
            modelBuilder.Entity<Mercadoria>().Property(x => x.TipoMercadoria).HasColumnName("TipoMercadoria");
        }
    }
}
