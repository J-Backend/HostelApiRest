using HostelApiRest.Contracts;
using HostelApiRest.Database;
using HostelModels.Models.Db;

namespace HostelApiRest.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabase db;
        //private HotelRepository _hotelRepository;
        //private CustomerRepository _customerRepository;
        //private CustomerRepository _customerRepository;

        //public IHotelRepository HotelRepository;
        public UnitOfWork(IDatabase db)
        {
            this.db = db;

        }

        public IRepository<Customer> CustomerRepository => new CustomerRepository(this.db);

        public IRepository<Booking> BookingRepository => new BookingRepository(db);

        public IRepository<Hotel> HotelRepository => new HotelRepository(this.db);


        //public IRepository<Hotel> HotelRepository
        //{
        //    get
        //    {
        //        if (_hotelRepository == null)
        //        {
        //            _hotelRepository = new HotelRepository(db);
        //        }
        //        return _hotelRepository;
        //    }
        //}

        //public IRepository<Customer> CustomerRepository
        //{
        //    get
        //    {
        //        if (_customerRepository == null)
        //        {
        //            _customerRepository = new CustomerRepository(db);
        //        }
        //        return _customerRepository;
        //    }
        //}
    }
}
