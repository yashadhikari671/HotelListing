using HotelListing.Data;
using System;
using System.Threading.Tasks;

namespace HotelListing.IRepository
{
    public interface IunitOfWork : IDisposable
    {
        IGenericRepository<Register> RegisterRepo { get; }
        Task Save();
    }
}
