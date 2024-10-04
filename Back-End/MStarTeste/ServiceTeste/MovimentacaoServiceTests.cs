using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MStar.DTO;
using MStar.Model;
using MStar.Repository;
using MStar.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MStarTeste.ServiceTeste
{
    public class MovimentacaoServiceTests
    {
        private ConnectionContext GetInMemoryContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ConnectionContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var context = new ConnectionContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public async Task MovimentacaoService_Add_ReturnsSuccess()
        {
            // Arrange

            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);
            var mercadoriaService = new MercadoriaService(context, tipoService);
            var movimentacaoService = new MovimentacaoService(context, mercadoriaService);

            var movimentacaoDTO = new MovimentacaoDTO
            {
               IdMercadoria = 1,
               LocalMovimentacao = "Teste",
               Quantidade = 1,
               TipoMovimentacao = "S"
            };


            var mercadoria = new Mercadoria
            {
                Id = 1,
                Nome = "Nome",
                Descricao = "Descricao",
                Fabricante = "Fabricante",
                TipoMercadoriaId = 1,
                DataCriacao = DateTime.Now
            };
            context.Add(mercadoria);

            // Act
            var result = await movimentacaoService.Add(movimentacaoDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(movimentacaoDTO.LocalMovimentacao, result.LocalMovimentacao);
        }

        [Fact]
        public async Task MovimentacaoService_Delete()
        {
            // Arrange

            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);
            var mercadoriaService = new MercadoriaService(context, tipoService);
            var movimentacaoService = new MovimentacaoService(context, mercadoriaService);

            var movimentacaoDTO = new MovimentacaoDTO
            {
                IdMercadoria = 1,
                LocalMovimentacao = "Teste",
                Quantidade = 1,
                TipoMovimentacao = "S"
            };

            var mercadoria = new Mercadoria
            {
                Id = 1,
                Nome = "Nome",
                Descricao = "Descricao",
                Fabricante = "Fabricante",
                TipoMercadoriaId = 1,
                DataCriacao = DateTime.Now
            };
            context.Add(mercadoria);

            // Act
            var result = await movimentacaoService.Add(movimentacaoDTO);
            await context.SaveChangesAsync();

            await movimentacaoService.Delete(result.Id);
            await context.SaveChangesAsync();

            // Assert
            var movimentacao = await context.Movimentacao.ToListAsync();
            Assert.DoesNotContain(movimentacao, m => m.TipoMovimentacao == result.TipoMovimentacao);
        }

        [Fact]
        public async Task MovimentacaoService_Update_Return_movimentacaoEncontrada()
        {

            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);
            var mercadoriaService = new MercadoriaService(context, tipoService);
            var movimentacaoService = new MovimentacaoService(context, mercadoriaService);

            var movimentacao = new Movimentacao
            {
                Id = 1,
                IdMercadoria = 1,
                LocalMovimentacao = "Teste",
                Quantidade = 7,
                TipoMovimentacao = "S",
                DataCriacao = DateTime.Now
            };

            context.Add(movimentacao);
            await context.SaveChangesAsync();

            var movimentacaoDTOAtualizado = new MovimentacaoDTO
            {
                IdMercadoria = 1,
                LocalMovimentacao = "Teste-2",
                Quantidade = 3,
                TipoMovimentacao = "E"
            };

            // Act
            var result = await movimentacaoService.Update(1, movimentacaoDTOAtualizado);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(3, result.Quantidade);

        }

        [Fact]
        public async Task MovimentacaoService_GetById_Return_movimentacaoEncontrada()
        {
            // Arrange

            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);
            var mercadoriaService = new MercadoriaService(context, tipoService);
            var movimentacaoService = new MovimentacaoService(context, mercadoriaService);

            var movimentacao = new Movimentacao
            {
                Id = 1,
                IdMercadoria = 1,
                LocalMovimentacao = "Teste",
                Quantidade = 1,
                TipoMovimentacao = "S",
                DataCriacao = DateTime.Now
            };
            context.Movimentacao.Add(movimentacao);
            await context.SaveChangesAsync();

            // Act
            var result = await movimentacaoService.GetById(movimentacao.Id);

            // Assert
            result.Id.Should().Be(1);
            movimentacao.Id.Should().Be(result.Id);
            movimentacao.TipoMovimentacao.Should().Be("S");
        }

        [Fact]
        public async Task MovimentacaoService_GetAll_Return_List()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);
            var mercadoriaService = new MercadoriaService(context, tipoService);
            var movimentacaoService = new MovimentacaoService(context, mercadoriaService);

            var movimentacao = new List<Movimentacao>
            {
                new Movimentacao
                {
                    Id = 1,
                    IdMercadoria = 1,
                    LocalMovimentacao = "Teste",
                    Quantidade = 1,
                    TipoMovimentacao = "S",
                    DataCriacao = DateTime.Now
                },
                new Movimentacao
                {
                    Id = 2,
                    IdMercadoria = 2,
                    LocalMovimentacao = "Teste-2",
                    Quantidade = 5,
                    TipoMovimentacao = "E",
                    DataCriacao = DateTime.Now
                },
            };
            context.Movimentacao.AddRange(movimentacao);
            await context.SaveChangesAsync();
            // Act
            var result = await movimentacaoService.GetAll();
            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

    }
}

