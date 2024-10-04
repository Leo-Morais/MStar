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
            Assert.NotNull(result);
            Assert.Equal(tipoDTO.Tipo, result.Tipo);
        }

        [Fact]
        public async Task TipoMercadoriaService_Delete()
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
            await context.SaveChangesAsync();

            await tipoService.Delete(result.Id);
            await context.SaveChangesAsync();

            // Assert
            var tipoMercadoria = await context.TipoMercadoria.ToListAsync();
            Assert.DoesNotContain(tipoMercadoria, m => m.Tipo == result.Tipo);
        }

        [Fact]
        public async Task TipoMercadoriaService_Update_ReturnTipo()
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
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Teste2", result.Tipo);

        }

        [Fact]
        public async Task TipoMercadoriaService_GetById_ReturnTipo()
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
            result.Id.Should().Be(1);
            tipoMercadoria.Id.Should().Be(result.Id);
            tipoMercadoria.Tipo.Should().Be("Eletronico");
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
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

    }
}

