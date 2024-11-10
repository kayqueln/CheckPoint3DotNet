using CP3.Data.AppData;
using CP3.Data.Repositories;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CP3.Tests
{
    public class BarcoRepositoryTests
    {
        private readonly DbContextOptions<ApplicationContext> _options;
        private readonly ApplicationContext _context;
        private readonly BarcoRepository _BarcoRepository;

        public BarcoRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationContext>()
        .UseInMemoryDatabase(databaseName: "TestDb")
        .Options;

            _context = new ApplicationContext(_options);
            _BarcoRepository = new BarcoRepository(_context);
        }

        [Fact]
        public void Adicionar_DeveAdicionarBarcoAoBanco()
        {
            var barco = new BarcoEntity { Id = 1, Nome = "Barco Teste", Modelo = "Modelo Teste" };

            var resultado = _BarcoRepository.Adicionar(barco);
            _context.SaveChanges();

            Assert.NotNull(_context.Barco.Find(barco.Id));
        }

        [Fact]
        public void ObterPorId_DeveRetornarBarcoComIdCorreto()
        {
            var barco = new BarcoEntity { Id = 2, Nome = "Barco Teste", Modelo = "Modelo Teste" };
            _context.Barco.Add(barco);
            _context.SaveChanges();

            var resultado = _BarcoRepository.ObterPorId(2);

            Assert.Equal(barco, resultado);
        }

        [Fact]
        public void Editar_DeveAtualizarBarcoNoBanco()
        {
            var barco = new BarcoEntity { Id = 1, Nome = "Barco Original", Modelo = "Modelo A" };
            _context.Barco.Update(barco);
            _context.SaveChanges();

            barco.Nome = "Barco Editado";

            var resultado = _BarcoRepository.Editar(barco);
            _context.SaveChanges();

            Assert.Equal("Barco Editado", _context.Barco.Find(1).Nome);
        }

        [Fact]
        public void Remover_DeveExcluirBarcoDoBanco()
        {
            var barco = new BarcoEntity { Id = 1, Nome = "Barco para Deletar", Modelo = "Modelo B" };
            _context.Barco.Add(barco);
            _context.SaveChanges();

            var resultado = _BarcoRepository.Remover(barco.Id);
            _context.SaveChanges();

            Assert.Null(_context.Barco.Find(1));
        }
    }
}
