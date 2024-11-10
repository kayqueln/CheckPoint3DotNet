using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using CP3.Domain.Interfaces.Dtos;

namespace CP3.Application.Services
{
    public class BarcoApplicationService : IBarcoApplicationService
    {
        private readonly IBarcoRepository _repository;

        public BarcoApplicationService(IBarcoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<BarcoEntity> ObterTodosBarcos()
        {
            return _repository.ObterTodos();
        }

        public BarcoEntity ObterBarcoPorId(int id)
        {
            return _repository.ObterPorId(id);
        }

        public BarcoEntity AdicionarBarco(IBarcoDto barcoDto)
        {
            var barco = new BarcoEntity
            {
                Nome = barcoDto.Nome,
                Modelo = barcoDto.Modelo,
                Ano = barcoDto.Ano,
                Tamanho = barcoDto.Tamanho
            };

            return _repository.Adicionar(barco);
        }

        public BarcoEntity EditarBarco(int Id, IBarcoDto barcoDto)
        {
            var barcoExistente = _repository.ObterPorId(Id);
            if (barcoExistente == null)
                throw new ArgumentException("Barco não encontrado");

            barcoExistente.Nome = barcoDto.Nome;
            barcoExistente.Modelo = barcoDto.Modelo;
            barcoExistente.Ano = barcoDto.Ano;
            barcoExistente.Tamanho = barcoDto.Tamanho;

            return _repository.Editar(barcoExistente);
        }

        public BarcoEntity RemoverBarco(int id)
        {
            return _repository.Remover(id);
        }
    }
}
