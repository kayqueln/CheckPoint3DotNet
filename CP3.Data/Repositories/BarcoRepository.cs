using CP3.Data.AppData;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;

namespace CP3.Data.Repositories
{
    public class BarcoRepository : IBarcoRepository
    {
        private readonly ApplicationContext _context;

        public BarcoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<BarcoEntity>? ObterTodos()
        {
            return _context.Set<BarcoEntity>().ToList();
        }

        public BarcoEntity? ObterPorId(int Id)
        {
            return _context.Set<BarcoEntity>().Find(Id);
        }

        public BarcoEntity? Adicionar(BarcoEntity barco)
        {
            _context.Set<BarcoEntity>().Add(barco);
            _context.SaveChanges();
            return barco;
        }

        public BarcoEntity? Editar(BarcoEntity barco)
        {
            var barcoExistente = _context.Set<BarcoEntity>().Find(barco.Id);
            if (barcoExistente == null) return null;

            barcoExistente.Nome = barco.Nome;
            barcoExistente.Modelo = barco.Modelo;
            barcoExistente.Ano = barco.Ano;
            barcoExistente.Tamanho = barco.Tamanho;

            _context.SaveChanges();
            return barcoExistente;
        }

        public BarcoEntity? Remover(int Id)
        {
            var barco = _context.Set<BarcoEntity>().Find(Id);
            if (barco == null) return null;

            _context.Set<BarcoEntity>().Remove(barco);
            _context.SaveChanges();
            return barco;
        }
    }
}
