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
                Tipo = "Eletronico"
            };
            context.Add(tipo);

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
            Assert.NotNull(result);
            Assert.Equal(mercadoriaDTO.Nome, result.Nome);
        }

        [Fact]
        public async Task MercadoriaService_Delete()
        {
            // Arrange

            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);
            var mercadoriaService = new MercadoriaService(context, tipoService);

            var tipo = new TipoMercadoria()
            {
                Id = 1,
                Tipo = "Eletronico"
            };
            context.Add(tipo);

            var mercadoriaDTO = new MercadoriaDTO
            {
                Descricao = "Teste",
                Fabricante = "Teste2",
                Nome = "Teste3",
                TipoMercadoriaId = 1
            };


            // Act
            var result = await mercadoriaService.Add(mercadoriaDTO);
            await context.SaveChangesAsync();

            await mercadoriaService.Delete(result.Id);
            await context.SaveChangesAsync();

            // Assert
            var mercadoria = await context.Mercadoria.ToListAsync();
            Assert.DoesNotContain(mercadoria, m => m.Descricao == result.Descricao);
        }

        [Fact]
        public async Task MercadoriaService_Update_Return_mercadoriaEncontrada()
        {

            // Arrange

            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);
            var mercadoriaService = new MercadoriaService(context, tipoService);

            var mercadoria = new Mercadoria
            {
                Id = 1,
                Nome = "Nome",
                Descricao = "Descricao",
                Fabricante = "Fabricante",
                TipoMercadoriaId = 1,
                DataCriacao = DateTime.Now
            };

            var mercadoriaAtualizadoDTO = new MercadoriaDTO
            {
                Descricao = "Teste-1",
                Fabricante = "Teste2-1",
                Nome = "Teste3-1",
                TipoMercadoriaId = 2
            };

            context.Add(mercadoria);
            await context.SaveChangesAsync();

            // Act
            var result = await mercadoriaService.Update(1, mercadoriaAtualizadoDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Teste-1", result.Descricao);

        }

        [Fact]
        public async Task MercadoriaService_GetById_Return_mercadoriaEncontrada()
        {
            // Arrange

            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);
            var mercadoriaService = new MercadoriaService(context, tipoService);


            var mercadoriaDTO = new Mercadoria
            {
                Id = 1,
                Descricao = "Teste",
                Fabricante = "Teste2",
                Nome = "Teste3",
                TipoMercadoriaId = 1,
                DataCriacao = DateTime.Now
            };
            context.Mercadoria.Add(mercadoriaDTO);
            await context.SaveChangesAsync();

            // Act
            var result = await mercadoriaService.GetById(mercadoriaDTO.Id);

            // Assert
            result.Id.Should().Be(1);
            mercadoriaDTO.Id.Should().Be(result.Id);
            mercadoriaDTO.Fabricante.Should().Be("Teste2");
        }

        [Fact]
        public async Task MercadoriaService_GetAll_ReturnsList()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);
            var mercadoriaService = new MercadoriaService(context, tipoService);

            var mercadoria = new List<Mercadoria>
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
                    TipoMercadoriaId = 2,
                    DataCriacao = DateTime.Now
                },
            };
            context.Mercadoria.AddRange(mercadoria);
            await context.SaveChangesAsync();
            // Act
            var result = await mercadoriaService.GetAll();
            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

    }
}

