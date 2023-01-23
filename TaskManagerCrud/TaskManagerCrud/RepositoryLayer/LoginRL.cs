using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerCrud.CommonLayer.Model;

namespace TaskManagerCrud.RepositoryLayer
{
    public class LoginRL:ILoginRL
    {
        public readonly IConfiguration _configuration;
        public readonly SqlConnection _sqlConnection;
        public object sqlCommand;

        public LoginRL(IConfiguration configuration)
        {
            _configuration = configuration;
            _sqlConnection = new SqlConnection(_configuration[key: "ConnectionStrings:DBSettingConnection"]);
        }
        public async Task<LoginModelResponse> LoginModel(LoginModelRequest request)
        {
            LoginModelResponse response = new LoginModelResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                string SqlQuery = "Select id from Login_Table where Username Like '@Username' and Password Like '@Password'";
                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue(parameterName: "@Username", request.username);
                    sqlCommand.Parameters.AddWithValue(parameterName: "@Password", request.Password);
                    _sqlConnection.Open();
                    using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            //response.loginModelData = new  LoginModelRequest();
                            //while (await sqlDataReader.ReadAsync())
                            //{
                            //    LoginModelRequest dbData = new LoginModelRequest();
                            //    dbData.username = sqlDataReader[name: "Username"] != DBNull.Value ? sqlDataReader[name: "Username"].ToString() : string.Empty;
                            //    dbData.Password = sqlDataReader[name: "Password"] != DBNull.Value ? sqlDataReader[name: "Password"].ToString() : string.Empty;
                               
                            //    response.loginModelData=dbData;
                            //}
                        }
                    }

                    //sqlCommand.Parameters.AddWithValue("@Username", request.username);
                    //sqlCommand.Parameters.AddWithValue("@Password", request.Password);

                    //await _sqlConnection.OpenAsync();
                    //int Status = await sqlCommand.ExecuteNonQueryAsync();
                    //if (Status <= 0)
                    //{
                    //    response.IsSuccess = false;
                    //    response.Message = "User not found";
                    //}
                }
            }
            catch (Exception ex)
            {
                //response.IsSuccess = false;
                //response.Message = ex.Message;
            }
            finally
            {
                _sqlConnection.Close();
            }
            return response;
        }

    }
}
