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
using Xunit;

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
            await context.SaveChangesAsync();

            var movimentacaoDTO = new MovimentacaoDTO
            {
                IdMercadoria = 1,
                LocalMovimentacao = "Teste",
                Quantidade = 1,
                TipoMovimentacao = "S",

            };

            // Act
            var result = await movimentacaoService.Add(movimentacaoDTO);

            // Assert
            result.Should().NotBeNull();
            result.IdMercadoria.Should().Be(movimentacaoDTO.IdMercadoria);
            result.LocalMovimentacao.Should().Be(movimentacaoDTO.LocalMovimentacao);
        }

        [Fact]
        public async Task MovimentacaoService_GetById_ReturnsMovimentacaoEncontrada()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);
            var mercadoriaService = new MercadoriaService(context, tipoService);
            var movimentacaoService = new MovimentacaoService(context, mercadoriaService);

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
            await context.SaveChangesAsync();

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
            result.Should().NotBeNull();
            result.Id.Should().Be(movimentacao.Id);
            result.LocalMovimentacao.Should().Be(movimentacao.LocalMovimentacao);
        }

        [Fact]
        public async Task MovimentacaoService_GetAll_ReturnsList()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);
            var mercadoriaService = new MercadoriaService(context, tipoService);
            var movimentacaoService = new MovimentacaoService(context, mercadoriaService);

            var tipo = new TipoMercadoria()
            {
                Id = 1,
                Tipo = "Eletrônico"
            };
            context.Add(tipo);
            await context.SaveChangesAsync();

            var mercadoria = new Mercadoria
            {
                Id = 1,
                Nome = "Nome",
                Descricao = "Descricao",
                Fabricante = "Fabricante",
                TipoMercadoriaId = 1,
                DataCriacao = DateTime.Now
            };

            var mercadoria2 = new Mercadoria
            {
                Id = 2,
                Nome = "Teste",
                Descricao = "Desc",
                Fabricante = "Fab",
                TipoMercadoriaId = 1,
                DataCriacao = DateTime.Now
            };

            await context.AddAsync(mercadoria);
            await context.AddAsync(mercadoria2);
            await context.SaveChangesAsync();

            var movimentacoes = new List<Movimentacao>
            {
                new Movimentacao
                {
                    Id = 1,
                    IdMercadoria = 1,
                    LocalMovimentacao = "Teste1",
                    Quantidade = 1,
                    TipoMovimentacao = "S",
                    DataCriacao = DateTime.Now
                },
                new Movimentacao
                {
                    Id = 2,
                    IdMercadoria = 2,
                    LocalMovimentacao = "Teste2",
                    Quantidade = 2,
                    TipoMovimentacao = "E",
                    DataCriacao = DateTime.Now
                }
            };

            context.Movimentacao.AddRange(movimentacoes);
            await context.SaveChangesAsync();

            // Act
            var result = await movimentacaoService.GetAll();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }
    }
}
