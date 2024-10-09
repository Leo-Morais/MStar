using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MStar.DTO;
using MStar.Model;
using MStar.Repository;
using MStar.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MStarTeste.ServiceTeste
{
    public class MercadoriaServiceTests
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
        public async Task MercadoriaService_Add_ReturnsSuccess()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);
            var mercadoriaService = new MercadoriaService(context, tipoService);

            var tipo = new TipoMercadoria()
            {
                Id = 1,
                Tipo = "Eletrônico"
            };
            context.Add(tipo);
            await context.SaveChangesAsync(); // Save changes to ensure TipoMercadoria is persisted

            var mercadoriaDTO = new MercadoriaDTO
            {
                Descricao = "Teste",
                Fabricante = "Teste2",
                Nome = "Teste3",
                TipoMercadoriaId = 1
            };

            // Act
            var result = await mercadoriaService.Add(mercadoriaDTO);

            // Assert
            result.Should().NotBeNull();
            result.Nome.Should().Be(mercadoriaDTO.Nome);
        }

        [Fact]
        public async Task MercadoriaService_Delete_Success()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);
            var mercadoriaService = new MercadoriaService(context, tipoService);

            var tipo = new TipoMercadoria()
            {
                Id = 1,
                Tipo = "Eletrônico"
            };
            context.Add(tipo);
            await context.SaveChangesAsync();

            var mercadoriaDTO = new MercadoriaDTO
            {
                Descricao = "Teste",
                Fabricante = "Teste2",
                Nome = "Teste3",
                TipoMercadoriaId = 1
            };

            var addedMercadoria = await mercadoriaService.Add(mercadoriaDTO);

            // Act
            await mercadoriaService.Delete(addedMercadoria.Id);

            // Assert
            var mercadoria = await context.Mercadoria.FindAsync(addedMercadoria.Id);
            mercadoria.Should().BeNull(); // Verifica se a mercadoria foi removida
        }

        [Fact]
        public async Task MercadoriaService_Update_ReturnsUpdatedMercadoria()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);
            var mercadoriaService = new MercadoriaService(context, tipoService);

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
            context.Add(mercadoria);
            await context.SaveChangesAsync();

            var mercadoriaAtualizadoDTO = new MercadoriaDTO
            {
                Descricao = "Teste-1",
                Fabricante = "Teste2-1",
                Nome = "Teste3-1",
                TipoMercadoriaId = 1 // Alterado para um tipo existente
            };

            // Act
            var result = await mercadoriaService.Update(1, mercadoriaAtualizadoDTO);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Descricao.Should().Be("Teste-1");
        }

        [Fact]
        public async Task MercadoriaService_GetById_ReturnsMercadoria()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);
            var mercadoriaService = new MercadoriaService(context, tipoService);

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
                Descricao = "Teste",
                Fabricante = "Teste2",
                Nome = "Teste3",
                TipoMercadoriaId = 1,
                DataCriacao = DateTime.Now
            };
            context.Add(mercadoria);
            await context.SaveChangesAsync();

            // Act
            var result = await mercadoriaService.GetById(mercadoria.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Fabricante.Should().Be("Teste2");
        }

        [Fact]
        public async Task MercadoriaService_GetAll_ReturnsList()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);
            var mercadoriaService = new MercadoriaService(context, tipoService);

            var tipo = new TipoMercadoria()
            {
                Id = 1,
                Tipo = "Eletrônico"
            };
            context.Add(tipo);
            await context.SaveChangesAsync();

            var mercadorias = new List<Mercadoria>
            {
                new Mercadoria
                {
                    Id = 1,
                    Descricao = "Teste",
                    Fabricante = "Teste2",
                    Nome = "Teste3",
                    TipoMercadoriaId = 1,
                    DataCriacao = DateTime.Now
                },
                new Mercadoria
                {
                    Id = 2,
                    Descricao = "Desc",
                    Fabricante = "Fab",
                    Nome = "Nom",
                    TipoMercadoriaId = 1,
                    DataCriacao = DateTime.Now
                },
            };
            context.AddRange(mercadorias);
            await context.SaveChangesAsync();

            // Act
            var result = await mercadoriaService.GetAll();

            // Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(2);
        }
    }
}
