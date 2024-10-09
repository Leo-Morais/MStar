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
        public DbSet<TipoMercadoria> TipoMercadoria { get; set; }
        public DbSet<Movimentacao> Movimentacao { get; set; }
        public DbSet<Estoque> Estoque {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Mapeamento de TipoMercadoria
            modelBuilder.Entity<TipoMercadoria>().HasKey(e => e.Id);
            modelBuilder.Entity<TipoMercadoria>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<TipoMercadoria>().ToTable("TipoMercadoria");
            modelBuilder.Entity<TipoMercadoria>().Property(x => x.Tipo).HasColumnName("Tipo");

            //Mapeamento de Mercadoria
            modelBuilder.Entity<Mercadoria>().HasKey(k => k.Id);
            modelBuilder.Entity<Mercadoria>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Mercadoria>().ToTable("Mercadorias");
            modelBuilder.Entity<Mercadoria>().Property(x => x.Fabricante).HasColumnName("Fabricante");
            modelBuilder.Entity<Mercadoria>().Property(x => x.Descricao).HasColumnName("Descricao");
            modelBuilder.Entity<Mercadoria>().Property(x => x.Nome).HasColumnName("Nome");
            modelBuilder.Entity<Mercadoria>().Property(x => x.DataCriacao).HasColumnName("DataCriacao");
            modelBuilder.Entity<Mercadoria>().Property(x => x.TipoMercadoriaId).HasColumnName("TipoMercadoriaId");
            modelBuilder.Entity<Mercadoria>().HasOne(x => x.TipoMercadoria).WithMany()
            .HasForeignKey(x => x.TipoMercadoriaId)
            .OnDelete(DeleteBehavior.ClientNoAction);
            modelBuilder.Entity<Mercadoria>().Navigation(x => x.TipoMercadoria).AutoInclude();

            //Mapeamento de Movimentação
            modelBuilder.Entity<Movimentacao>().HasKey(x => x.Id);
            modelBuilder.Entity<Movimentacao>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Movimentacao>().ToTable("Movimentacao");
            modelBuilder.Entity<Movimentacao>().Property(x => x.Quantidade).HasColumnName("Quantidade");
            modelBuilder.Entity<Movimentacao>().Property(x => x.LocalMovimentacao).HasColumnName("LocalMovimentacao");
            modelBuilder.Entity<Movimentacao>().Property(x => x.TipoMovimentacao).HasColumnName("TipoMovimentacao");
            modelBuilder.Entity<Movimentacao>().Property(x => x.DataCriacao).HasColumnName("DataCriacao");
            modelBuilder.Entity<Movimentacao>().Property(x => x.IdMercadoria).HasColumnName("IdMercadoria");
            modelBuilder.Entity<Movimentacao>().HasOne(x => x.Mercadoria).WithMany()
            .HasForeignKey(x => x.IdMercadoria)
            .OnDelete(DeleteBehavior.ClientNoAction);
            modelBuilder.Entity<Movimentacao>().Navigation(x => x.Mercadoria).AutoInclude();


            //Mapeamento de Estoque
            modelBuilder.Entity<Estoque>().HasKey(x => x.Id);
            modelBuilder.Entity<Estoque>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Estoque>().ToTable("Estoque");
            modelBuilder.Entity<Estoque>().Property(x => x.Quantidade).HasColumnName("Quantidade");
            modelBuilder.Entity<Estoque>().Property(x => x.DataAtualizacao).HasColumnName("DataAtualizacao");
            modelBuilder.Entity<Estoque>().HasOne(x => x.Mercadoria).WithMany()
            .HasForeignKey(x => x.IdMercadoria)
            .OnDelete(DeleteBehavior.ClientNoAction);
            modelBuilder.Entity<Estoque>().Navigation(x => x.Mercadoria).AutoInclude();
        }
    }
}
