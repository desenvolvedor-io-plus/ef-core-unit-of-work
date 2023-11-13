using DevIO.Business.Interfaces;
using DevIO.Data.Context;

namespace DevIO.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MeuDbContext _context;

        public UnitOfWork(MeuDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
