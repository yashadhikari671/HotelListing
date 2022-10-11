using HotelListing.Data;
using HotelListing.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace HotelListing.Repository
{
    public class UnitOfWork : IunitOfWork
    {
        private readonly DatabaseContext _context;
        private IGenericRepository<Register> _register;
        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public IGenericRepository<Register> RegisterRepo => _register??= new GenericRepository<Register>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
