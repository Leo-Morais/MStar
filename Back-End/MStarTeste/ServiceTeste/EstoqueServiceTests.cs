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
            await context.SaveChangesAsync(); // Salvar a mercadoria antes de adicionar o estoque

            var estoqueDTO = new EstoqueDTO
            {
                Quantidade = 1,
                IdMercadoria = 1 // Definir o IdMercadoria para associar corretamente
            };

            // Act
            var result = await estoqueService.Add(estoqueDTO);

            // Assert
            result.Should().NotBeNull();
            result.Quantidade.Should().Be(estoqueDTO.Quantidade);
            result.IdMercadoria.Should().Be(estoqueDTO.IdMercadoria);
        }

        [Fact]
        public async Task EstoqueService_Delete_Success()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);
            var mercadoriaService = new MercadoriaService(context, tipoService);
            var estoqueService = new EstoqueService(context, mercadoriaService);

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

            var estoqueDTO = new EstoqueDTO
            {
                Quantidade = 1,
                IdMercadoria = 1
            };
            var estoque = await estoqueService.Add(estoqueDTO);

            // Act
            await estoqueService.Delete(estoque.Id);

            // Assert
            var estoqueList = await context.Estoque.ToListAsync();
            estoqueList.Should().NotContain(m => m.Id == estoque.Id);
        }

        [Fact]
        public async Task EstoqueService_Update_ReturnsUpdatedEstoque()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);
            var mercadoriaService = new MercadoriaService(context, tipoService);
            var estoqueService = new EstoqueService(context, mercadoriaService);

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

            var estoque = new Estoque
            {
                Id = 1, // Defina o ID aqui para simular uma entidade existente
                Quantidade = 1,
                DataAtualizacao = DateTime.Now,
                IdMercadoria = 1
            };
            context.Add(estoque);
            await context.SaveChangesAsync();

            var estoqueDTOAtualizado = new EstoqueDTO
            {
                Quantidade = 2,
                IdMercadoria = 1
            };

            // Act
            var result = await estoqueService.Update(estoque.Id, estoqueDTOAtualizado);

            // Assert
            result.Should().NotBeNull();
            result.Quantidade.Should().Be(2);
            result.IdMercadoria.Should().Be(estoqueDTOAtualizado.IdMercadoria);
        }

        [Fact]
        public async Task EstoqueService_GetById_ReturnsEstoque()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);
            var mercadoriaService = new MercadoriaService(context, tipoService);
            var estoqueService = new EstoqueService(context, mercadoriaService);

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

            var estoque = new Estoque
            {
                Id = 1,
                Quantidade = 1,
                DataAtualizacao = DateTime.Now,
                IdMercadoria = 1
            };
            context.Add(estoque);
            await context.SaveChangesAsync();

            // Act
            var result = await estoqueService.GetById(estoque.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(estoque.Id);
            result.Quantidade.Should().Be(estoque.Quantidade);
        }

        [Fact]
        public async Task EstoqueService_GetAll_ReturnsList()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var tipoService = new TipoMercadoriaService(context);
            var mercadoriaService = new MercadoriaService(context, tipoService);
            var estoqueService = new EstoqueService(context, mercadoriaService);

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

            var estoqueList = new List<Estoque>
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
                    IdMercadoria = 1,
                },
            };
            context.AddRange(estoqueList);
            await context.SaveChangesAsync();

            // Act
            var result = await estoqueService.GetAll();

            // Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(2);
        }
    }
}
