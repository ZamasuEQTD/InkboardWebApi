using Application.Core.Abstractions;

namespace Infraestructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InkboardDbContext _context;

        public UnitOfWork(InkboardDbContext context)
        {
            _context = context;
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => _context.SaveChangesAsync(cancellationToken);
    }
}