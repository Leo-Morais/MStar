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
    public class EstoqueServiceTests
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
        public async Task EstoqueService_Add_ReturnsSuccess()
        {
            // Arrange

            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);
            var mercadoriaService = new MercadoriaService(context, tipoService);
            var estoqueService = new EstoqueService(context, mercadoriaService);

            var mercadoria = new Mercadoria()
            {
                Id = 1,
                Descricao = "Teste",
                Fabricante = "Teste2",
                Nome = "Teste3",
                TipoMercadoriaId = 1,
                DataCriacao = DateTime.Now
            };
            context.Add(mercadoria);

            var estoqueDTO = new EstoqueDTO
            {
                Quantidade = 1
            };


            // Act
            var result = await estoqueService.Add(1,estoqueDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(estoqueDTO.Quantidade, result.Quantidade);
        }

        [Fact]
        public async Task EstoqueService_Delete()
        {
            // Arrange

            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);
            var mercadoriaService = new MercadoriaService(context, tipoService);
            var estoqueService = new EstoqueService(context, mercadoriaService);

            var mercadoria = new Mercadoria()
            {
                Id = 1,
                Descricao = "Teste",
                Fabricante = "Teste2",
                Nome = "Teste3",
                TipoMercadoriaId = 1,
                DataCriacao = DateTime.Now
            };
            context.Add(mercadoria);

            var estoqueDTO = new EstoqueDTO
            {
                Quantidade = 1
            };


            // Act
            var result = await estoqueService.Add(1,estoqueDTO);
            await context.SaveChangesAsync();

            await estoqueService.Delete(result.Id);
            await context.SaveChangesAsync();

            // Assert
            var estoque = await context.Estoque.ToListAsync();
            Assert.DoesNotContain(estoque, m => m.Quantidade == result.Quantidade);
        }

        [Fact]
        public async Task EstoqueService_Update_Return_estoqueEncontrado()
        {

            // Arrange

            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);
            var mercadoriaService = new MercadoriaService(context, tipoService);
            var estoqueService = new EstoqueService(context, mercadoriaService);

            var estoque = new Estoque
            {
                Quantidade = 1,
                DataAtualizacao = DateTime.Now,
                IdMercadoria = 1 
            };

            context.Add(estoque);

            var estoqueDTOAtualizado = new EstoqueDTO
            {
                Quantidade = 2
            };

            await context.SaveChangesAsync();

            // Act
            var result = await estoqueService.Update(1, estoqueDTOAtualizado);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(2, result.Quantidade);

        }

        [Fact]
        public async Task EstoqueService_GetById_Return_estoqueEncontrado()
        {
            // Arrange

            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);
            var mercadoriaService = new MercadoriaService(context, tipoService);
            var estoqueService = new EstoqueService(context, mercadoriaService);

            var estoque = new Estoque
            {
                Id = 1,
                Quantidade = 1,
                DataAtualizacao = DateTime.Now,
                IdMercadoria = 1,
            };
            context.Estoque.Add(estoque);
            await context.SaveChangesAsync();

            // Act
            var result = await estoqueService.GetById(estoque.Id);

            // Assert
            result.Id.Should().Be(1);
            estoque.Id.Should().Be(result.Id);
            estoque.Quantidade.Should().Be(1);
        }

        [Fact]
        public async Task EstoqueService_GetAll_Return_List()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);
            var mercadoriaService = new MercadoriaService(context, tipoService);
            var estoqueService = new EstoqueService(context, mercadoriaService);

            var estoque = new List<Estoque>
            {
                new Estoque
                {
                    Id = 1,
                    Quantidade = 1,
                    DataAtualizacao = DateTime.Now,
                    IdMercadoria = 1,
                },
                new Estoque
                {
                    Id = 2,
                    Quantidade = 15,
                    DataAtualizacao = DateTime.Now,
                    IdMercadoria = 3,
                },
            };
            context.Estoque.AddRange(estoque);
            await context.SaveChangesAsync();
            // Act
            var result = await estoqueService.GetAll();
            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

    }
}

