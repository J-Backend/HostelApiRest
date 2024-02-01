using HostelApiRest.Contracts;
using HostelApiRest.Database;
using HostelModels.Models.Db;
using System.Data;
using System.Data.SqlClient;

namespace HostelApiRest.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly IDatabase db;

        public CustomerRepository(IDatabase db)
        {
            this.db = db;
        }


        public async Task<List<Customer>> ListAsync()
        {
            var list = new List<Customer>();
            try
            {
                using (var connection = db.GetConnection())
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("dbo.spCustomer_GetList", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(
                                    new Customer()
                                    {
                                        CustomerId = Convert.ToInt32(reader["CustomerId"]),
                                        UserName = reader["UserName"].ToString(),
                                        Email = reader["Email"].ToString(),
                                        Keyword = reader["Keyword"].ToString(),
                                        TypeUser = Convert.ToInt32(reader["TypeUser"])
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

        public async Task<Customer> SearchAsync(int id)
        {
            bool isNull = true;
            var model = new Customer();
            try
            {
                using (var connection = db.GetConnection())
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("dbo.spCustomer_GetById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            model.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                            model.UserName = reader["UserName"].ToString();
                            model.Email = reader["Email"].ToString();
                            model.Keyword = reader["Keyword"].ToString();
                            model.TypeUser = Convert.ToInt32(reader["TypeUser"]);
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

        public async Task<int> AddAsync(Customer model)
        {
            int state = 0;
            try
            {
                using (var connection = db.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("dbo.spCustomer_AddNew", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = model.UserName;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = model.Email;
                    cmd.Parameters.Add("@Keyword", SqlDbType.VarChar, 50).Value = model.Keyword;
                    cmd.Parameters.Add("@TypeUser", SqlDbType.VarChar, 50).Value = model.TypeUser;

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

        
        public async Task<int> ChangeAsync(Customer model)
        {
            int state = 0;
            try
            {
                using (var connection = db.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("dbo.spCustomer_Update", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = model.CustomerId;
                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = model.UserName;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = model.Email;
                    cmd.Parameters.Add("@Keyword", SqlDbType.VarChar, 50).Value = model.Keyword;
                    cmd.Parameters.Add("@TypeUser", SqlDbType.VarChar, 50).Value = model.TypeUser;

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
                    SqlCommand cmd = new SqlCommand("dbo.spCustomer_Delete", connection);
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
