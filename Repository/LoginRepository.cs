using Demomvc.Models;
using System.Data;
using System.Data.SqlClient;


namespace Demomvc.Repository
{
    public class LoginRepository : ILoginRepository
    {
        //connecyion string 
        //call stored procedure
        /*
         * in Asp.net core MVC ,it is recommented to use 
         * depencency injection to inject dependencies rather than 
         * creating instances directly in the controller.
         */

        private readonly string connectionString;
        public LoginRepository(IConfiguration configuration)
        {
            //from the controller we are connecting the repository
            connectionString = configuration.GetConnectionString("ConnectionMVC");
        }

        public string Deleteuser(Login login)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("sp_DeleteLogin", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", login.Id);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    return "User successfully deleted.";
                }
                catch (SqlException ex)
                {
                    return $"SQL Error: {ex.Message}";
                }
                catch (Exception ex)
                {
                    return $"Error: {ex.Message}";
                }
                finally
                {
                    connection.Close();
                }
            }

        }

        public string Getuser(Login login)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("sp_SearchLogin", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserName", login.UserName);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        // Assuming you want to return user details as a string
                        // Adapt this part according to your needs
                        string result = "";
                        while (reader.Read())
                        {
                            result += $"ID: {reader["ID"]}, UserName: {reader["UserName"]}, Password: {reader["Password"]}\n";
                        }
                        return result;
                    }
                    else
                    {
                        return "No user found with the provided username.";
                    }
                }
                catch (SqlException ex)
                {
                    return $"SQL Error: {ex.Message}";
                }
                catch (Exception ex)
                {
                    return $"Error: {ex.Message}";
                }
                finally
                {
                    connection.Close();
                }
            }

        }

        public string InsertUser(Login login)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //using sql command
                SqlCommand command = new SqlCommand("sp_InsertNewLogin", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserName", login.UserName);
                command.Parameters.AddWithValue("@Password", login.Password);

                try
                {
                    //open the connection
                    connection.Open();
                    command.ExecuteNonQuery();

                    //close the connection
                    connection.Close();
                    return "User successfully inserted.";
                }
                catch (SqlException ex)
                {
                    // Handle SQL errors
                    return $"SQL Error: {ex.Message}";
                }
                catch (Exception ex)
                {
                    // Handle general errors
                    return $"Error: {ex.Message}";
                }
            }
        }

        public string Searchuser(Login login)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("sp_SearchUser", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserName", login.UserName);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        string result = "";
                        while (reader.Read())
                        {
                            result += $"ID: {reader["ID"]}, UserName: {reader["UserName"]}, Password: {reader["Password"]}\n";
                        }
                        return result;
                    }
                    else
                    {
                        return "No user found with the provided username.";
                    }
                }
                catch (SqlException ex)
                {
                    return $"SQL Error: {ex.Message}";
                }
                catch (Exception ex)
                {
                    return $"Error: {ex.Message}";
                }
                finally
                {
                    connection.Close();
                }
            }

        }

        public string Updateuser(Login login)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("sp_UpdateLoginUser", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", login.Id); // Assuming Id is a property of Login model
                command.Parameters.AddWithValue("@UserName", login.UserName);
                command.Parameters.AddWithValue("@Password", login.Password);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return "User successfully updated.";
                    }
                    else
                    {
                        return "User not found.";
                    }
                }
                catch (SqlException ex)
                {
                    // Handle SQL errors
                    return $"SQL Error: {ex.Message}";
                }
                catch (Exception ex)
                {
                    // Handle general errors
                    return $"Error: {ex.Message}";
                }
            }

        }



        //insert user implememtation
        /*
        public string InsertUser(Login login)
        {
            //using sql connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //using sql command
                SqlCommand command = new SqlCommand("sp_InsertNewLogin", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserName", login.UserName);
                command.Parameters.AddWithValue("@Password", login.Password);

                try
                {
                    //open the connection
                    connection.Open();
                    command.ExecuteNonQuery();

                    //close the connection
                    connection.Close();
                    return "User successfully inserted.";
                }
                catch (SqlException ex)
                {
                    // Handle SQL errors
                    return $"SQL Error: {ex.Message}";
                }
                catch (Exception ex)
                {
                    // Handle general errors
                    return $"Error: {ex.Message}";
                }
            }
        }

        */
        /*
        public string UpdateUser(Login login)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            {
                using SqlCommand command = new SqlCommand("sp_UpdateLoginUser", connection);
                command.CommandType = CommandType.StoredProcedure;
                

                
                

            }
            
        }
        */

        public int UserCredentials(Login login)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //sql command
                SqlCommand command = new SqlCommand("sp_login", connection);
                command.CommandType = CommandType.StoredProcedure;

                //input parameters
                command.Parameters.AddWithValue("@UserName", login.UserName);
                command.Parameters.AddWithValue("@Password", login.Password);

                //output parameters-Isvalid
                SqlParameter outputBitLogin = new SqlParameter();
                outputBitLogin.ParameterName = "@Isvalid";
                outputBitLogin.SqlDbType = SqlDbType.Bit;
                outputBitLogin.Direction = ParameterDirection.Output;

                //Add to sql command
                command.Parameters.Add(outputBitLogin);

                //open connection
                connection.Open();
                command.ExecuteNonQuery();

                //result 
                int result = Convert.ToInt32(outputBitLogin.Value);

                //close connection
                connection.Close();

                //return result
                return result;

                //steps 
                //select all login users


                //Update an existing login user
                //search a particular login user
                //delete a particular login user
            }

        }



    }
}
