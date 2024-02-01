
using HostelModels.Models.Db;

namespace HostelApiRest.Contracts
{
    public interface IUnitOfWork
    {
        public IRepository<Hotel> HotelRepository { get; }
        public IRepository<Customer> CustomerRepository { get; }

        public IRepository<Booking> BookingRepository { get; }
    }
}
