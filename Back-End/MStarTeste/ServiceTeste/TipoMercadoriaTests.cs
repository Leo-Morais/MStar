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
    public class TipoMercadoriaTests
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
        public async Task TipoMercadoriaService_Add_ReturnsSuccess()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);

            var tipoDTO = new TipoMercadoriaDTO
            {
                Tipo = "Teste"
            };

            // Act
            var result = await tipoService.Add(tipoDTO);

            // Assert
            result.Should().NotBeNull();
            result.Tipo.Should().Be(tipoDTO.Tipo);
        }

        [Fact]
        public async Task TipoMercadoriaService_Delete_RemovesTipoMercadoria()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);

            var tipoDTO = new TipoMercadoriaDTO
            {
                Tipo = "Teste"
            };

            var result = await tipoService.Add(tipoDTO);
            await context.SaveChangesAsync();

            // Act
            await tipoService.Delete(result.Id);
            await context.SaveChangesAsync();

            // Assert
            var tipoMercadoria = await context.TipoMercadoria.ToListAsync();
            tipoMercadoria.Should().NotContain(m => m.Tipo == result.Tipo);
        }

        [Fact]
        public async Task TipoMercadoriaService_Update_ReturnsUpdatedTipo()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);

            var tipoMercadoria = new TipoMercadoria
            {
                Id = 1,
                Tipo = "Teste",
            };

            var tipoAtualizadoDTO = new TipoMercadoriaDTO
            {
                Tipo = "Teste2"
            };

            context.Add(tipoMercadoria);
            await context.SaveChangesAsync();

            // Act
            var result = await tipoService.Update(1, tipoAtualizadoDTO);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Tipo.Should().Be("Teste2");
        }

        [Fact]
        public async Task TipoMercadoriaService_GetById_ReturnsTipo()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);

            var tipoMercadoria = new TipoMercadoria
            {
                Id = 1,
                Tipo = "Eletronico",
            };
            context.TipoMercadoria.Add(tipoMercadoria);
            await context.SaveChangesAsync();

            // Act
            var result = await tipoService.GetById(tipoMercadoria.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Tipo.Should().Be("Eletronico");
        }

        [Fact]
        public async Task TipoMercadoriaService_GetAll_ReturnsList()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);

            var tipoMercadoria = new List<TipoMercadoria>
            {
                new TipoMercadoria
                {
                    Id = 1,
                    Tipo = "Eletronico"
                },
                new TipoMercadoria
                {
                    Id = 2,
                    Tipo = "Material de Construção"
                },
            };
            context.TipoMercadoria.AddRange(tipoMercadoria);
            await context.SaveChangesAsync();

            // Act
            var result = await tipoService.GetAll();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }
    }
}
