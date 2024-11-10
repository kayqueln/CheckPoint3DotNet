using CP3.Application.Services;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using CP3.Domain.Interfaces.Dtos;
using Moq;

namespace CP3.Tests
{
    public class BarcoApplicationServiceTests
    {
        private readonly Mock<IBarcoRepository> _repositoryMock;
        private readonly BarcoApplicationService _BarcoService;

        public BarcoApplicationServiceTests()
        {
            _repositoryMock = new Mock<IBarcoRepository>();
            _BarcoService = new BarcoApplicationService(_repositoryMock.Object);
        }

        [Fact]
        public void ObterTodosBarcos_DeveRetornarListaDeBarcos()
        {
            var barcos = new List<BarcoEntity> { new BarcoEntity { Id = 1, Nome = "Barco A", Modelo = "Modelo A" } };
            _repositoryMock.Setup(repo => repo.ObterTodos()).Returns(barcos);

            var resultado = _BarcoService.ObterTodosBarcos();

            Assert.Equal(barcos, resultado);
            _repositoryMock.Verify(repo => repo.ObterTodos(), Times.Once);
        }

        [Fact]
        public void AdicionarBarco_DeveChamarRepositorioComBarcoCorreto()
        {
            var barcoDto = new Mock<IBarcoDto>();
            var barcoEntity = new BarcoEntity { Id = 1, Nome = "Novo Barco", Modelo = "Modelo A" };
            _repositoryMock.Setup(repo => repo.Adicionar(It.IsAny<BarcoEntity>())).Returns(barcoEntity);

            var resultado = _BarcoService.AdicionarBarco(barcoDto.Object);

            Assert.Equal(barcoEntity, resultado);
            _repositoryMock.Verify(repo => repo.Adicionar(It.IsAny<BarcoEntity>()), Times.Once);
        }

        [Fact]
        public void ObterBarcoPorId_DeveRetornarBarcoCorreto()
        {
            var barcoId = 1;
            var barcoEntity = new BarcoEntity { Id = barcoId, Nome = "Barco A", Modelo = "Modelo A" };
            _repositoryMock.Setup(repo => repo.ObterPorId(barcoId)).Returns(barcoEntity);

            var resultado = _BarcoService.ObterBarcoPorId(barcoId);

            Assert.Equal(barcoEntity, resultado);
            _repositoryMock.Verify(repo => repo.ObterPorId(barcoId), Times.Once);
        }

        [Fact]
        public void RemoverBarco_DeveChamarRepositorioParaRemoverBarco()
        {
            var barcoId = 1;
            _repositoryMock.Setup(repo => repo.Remover(It.IsAny<int>())).Verifiable();

            _BarcoService.RemoverBarco(barcoId);

            _repositoryMock.Verify(repo => repo.Remover(barcoId), Times.Once);
        }

        [Fact]
        public void EditarBarco_DeveLancarExcecaoSeBarcoNaoExistir()
        {
            var id = 1;
            var barcoDto = new Mock<IBarcoDto>();
            _repositoryMock.Setup(r => r.ObterPorId(id)).Returns((BarcoEntity)null);

            var exception = Assert.Throws<ArgumentException>(() => _BarcoService.EditarBarco(id, barcoDto.Object));
            Assert.Equal("Barco não encontrado", exception.Message);

            _repositoryMock.Verify(r => r.ObterPorId(id), Times.Once);
        }
    }
}
