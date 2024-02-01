using HostelApiRest.Contracts;
using HostelApiRest.Database;
using HostelModels.Models.Db;
using System.Data;
using System.Data.SqlClient;

namespace HostelApiRest.Repositories
{
    public class BookingRepository : IRepository<Booking>
    {
        private readonly IDatabase db;

        public BookingRepository(IDatabase db)
        {
            this.db = db;
        }

        public async Task<List<Booking>> ListAsync()
        {
            var list = new List<Booking>();
            try
            {
                using (var connection = db.GetConnection())
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("dbo.spBooking_GetList", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(
                                    new Booking()
                                    {
                                        BookingId = Convert.ToInt32(reader["BookingId"]),
                                        CheckIn = Convert.ToDateTime(reader["CheckIn"]),
                                        CheckOut = Convert.ToDateTime(reader["CheckOut"]),
                                        HotelId = Convert.ToInt32(reader["HotelId"]),
                                        CustomerId = Convert.ToInt32(reader["CustomerId"])
                                    }
                                );
                        }
                    }
                    connection.Close();
                }
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Booking> SearchAsync(int id)
        {
            bool isNull = true;
            var model = new Booking();
            try
            {
                using (var connection = db.GetConnection())
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("dbo.spBooking_GetById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            model.BookingId = Convert.ToInt32(reader["BookingId"]);
                            model.CheckIn = (DateTime)reader["CheckIn"];
                            model.CheckOut = (DateTime)reader["CheckOut"];
                            model.HotelId = (int)reader["HotelId"];
                            model.CustomerId = (int)reader["CustomerId"];



                            isNull = false;
                        }
                    }
                    connection.Close();
                }
                return isNull == false ? model : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<int> AddAsync(Booking model)
        {
            int state = 0;
            try
            {
                using (var connection = db.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("dbo.spBooking_AddNew", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CheckIn", SqlDbType.VarChar, 50).Value = model.CheckIn;
                    cmd.Parameters.Add("@CheckOut", SqlDbType.VarChar, 50).Value = model.CheckOut;
                    cmd.Parameters.Add("@HotelId", SqlDbType.VarChar, 50).Value = model.HotelId;
                    cmd.Parameters.Add("@CustomerId", SqlDbType.VarChar, 50).Value = model.CustomerId;

                    connection.Open();
                    state = await cmd.ExecuteNonQueryAsync();
                    connection.Close();
                }
                return state;
            }
            catch (Exception)
            {
                return -1;
            }
        }

       
       

        public async Task<int> ChangeAsync(Booking model)
        {
            int state = 0;
            try
            {
                using (var connection = db.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("dbo.spBooking_Update", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = model.BookingId;
                    cmd.Parameters.Add("@CheckIn", SqlDbType.VarChar, 50).Value = model.CheckIn;
                    cmd.Parameters.Add("@CheckOut", SqlDbType.VarChar, 50).Value = model.CheckOut;
                    cmd.Parameters.Add("@HotelId", SqlDbType.VarChar, 50).Value = model.HotelId;
                    cmd.Parameters.Add("@CustomerId", SqlDbType.VarChar, 50).Value = model.CustomerId;

                    connection.Open();
                    state = await cmd.ExecuteNonQueryAsync();
                    connection.Close();
                }
                return state;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<int> EraseAsync(int id)
        {
            int state = 0;
            try
            {
                using (var connection = db.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("dbo.spBooking_Delete", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    connection.Open();
                    state = await cmd.ExecuteNonQueryAsync();
                    connection.Close();
                }
                return state;
            }
            catch (Exception)
            {
                return -1;
            }
        }

    }
}
