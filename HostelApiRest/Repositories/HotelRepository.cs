using HostelApiRest.Contracts;
using HostelApiRest.Database;
using HostelModels.Models.Db;
using System.Data;
using System.Data.SqlClient;

namespace HostelApiRest.Repositories
{
    public class HotelRepository : IRepository<Hotel>
    {
        private readonly IDatabase db;

        public HotelRepository(IDatabase db)
        {
            this.db = db;
        }

        public async Task<List<Hotel>> ListAsync()
        {
            var list = new List<Hotel>();
            try
            {
                using (var connection = db.GetConnection())
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("dbo.spHotel_GetList", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(
                                    new Hotel()
                                    {
                                        HotelId = Convert.ToInt32(reader["HotelId"]),
                                        Title = reader["HotelName"].ToString()
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

        public async Task<Hotel> SearchAsync(int id)
        {
            bool isNull = true;
            var model = new Hotel();
            try
            {
                using (var connection = db.GetConnection())
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("dbo.spHotel_GetById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            model.Title = reader["HotelName"].ToString();
                            model.HotelId = Convert.ToInt32(reader["HotelId"]);
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

        public async Task<int> AddAsync(Hotel model)
        {
            int state = 0;
            try
            {
                using (var connection = db.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("dbo.spHotel_AddNew", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@title", SqlDbType.VarChar, 50).Value = model.Title;

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


        public async Task<int> ChangeAsync(Hotel model)
        {
            int state = 0;
            try
            {
                using (var connection = db.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("dbo.spHotel_Update", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = model.HotelId;
                    cmd.Parameters.Add("@title", SqlDbType.VarChar, 50).Value = model.Title;

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
                    SqlCommand cmd = new SqlCommand("dbo.spHotel_Delete", connection);
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
