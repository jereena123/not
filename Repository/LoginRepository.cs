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
