using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagerCrud.CommonLayer.Model;
using static TaskManagerCrud.CommonLayer.Model.GetTask;

namespace TaskManagerCrud.RepositoryLayer
{
    public class TaskRepositoryLayer : ITaskRepositoryLayer
    {
        public readonly IConfiguration _configuration;
        public readonly SqlConnection _sqlConnection;
        public object sqlCommand;


        public TaskRepositoryLayer(IConfiguration configuration)
        {
            _configuration = configuration;
            _sqlConnection = new SqlConnection(_configuration[key: "ConnectionStrings:DBSettingConnection"]);
        }

        public async Task<CreateTaskResponse> CreateTask(CreatetaskRequest taskData)
        {
            CreateTaskResponse response = new CreateTaskResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                string SqlQuery = "exec Task_SP  @Title,@Description,@DueDate,@Status,@Priority,@AssignedTo, @AssignedBy";
               // string SqlQuery = "Insert into Task_Table(Title,Description,DueDate,Status,Priority,AssignedTo,AssignedBy) values(@Title, @Description,@DueDate,@Status,@Priority,@AssignedTo,@AssignedBy)";
                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@Title", taskData.title);
                    sqlCommand.Parameters.AddWithValue("@Description", taskData.description);
                    sqlCommand.Parameters.AddWithValue("@DueDate", taskData.dueDate);
                    sqlCommand.Parameters.AddWithValue("@Status", taskData.status);
                    sqlCommand.Parameters.AddWithValue("@Priority", taskData.priority);
                    sqlCommand.Parameters.AddWithValue("@AssignedTo", taskData.assignedTo);
                    sqlCommand.Parameters.AddWithValue("@AssignedBy", taskData.assignedBy);
                    await _sqlConnection.OpenAsync();
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Create Information Not Executed";
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                _sqlConnection.Close();
            }
            return response;
        }

        public async Task<DeleteTaskResponse> DeleteTask(int id)
        {
            DeleteTaskResponse response = new DeleteTaskResponse();
            response.IsSuccess = true;
            response.Message = "success";
            try
            {
                // string SqlQuery = "Delete from Task_Table where Id = @id";
                string SqlQuery = "exec Delete_Task_SP @id";
                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue(parameterName: "@id", id);
                    _sqlConnection.Open();
                    int status = await sqlCommand.ExecuteNonQueryAsync();
                    if (status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Something Went wrong";
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                _sqlConnection.Close();
            }

            return response;
        }

        public async Task<GetTaskResponse> GetTaskAssignedByMe(string username)
        {
            GetTaskResponse response = new GetTaskResponse();
            response.IsSuccess = true;
            response.Message = "successful";
            try
            {
                string SqlQuery = "select * from Task_Table where ( AssignedBy = @username);";
                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue(parameterName: "@username", username);
                    _sqlConnection.Open();
                    using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            response.getTaskData = new List<GetTaskData>();
                            while (await sqlDataReader.ReadAsync())
                            {
                                GetTaskData dbData = new GetTaskData();
                                dbData.Id = sqlDataReader[name: "Id"] != DBNull.Value ? Convert.ToInt32(sqlDataReader[name: "Id"]) : 0;
                                dbData.Title = sqlDataReader[name: "Title"] != DBNull.Value ? sqlDataReader[name: "Title"].ToString() : string.Empty;
                                dbData.Description = sqlDataReader[name: "Description"] != DBNull.Value ? sqlDataReader[name: "Description"].ToString() : string.Empty;
                                dbData.DueDate = sqlDataReader[name: "DueDate"] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[name: "DueDate"]) : DateTime.Now;
                                dbData.Status = sqlDataReader[name: "Status"] != DBNull.Value ? sqlDataReader[name: "Status"].ToString() : string.Empty;
                                dbData.Priority = sqlDataReader[name: "Priority"] != DBNull.Value ? sqlDataReader[name: "Priority"].ToString() : string.Empty;
                                dbData.AssignedTo = sqlDataReader[name: "AssignedTo"] != DBNull.Value ? sqlDataReader[name: "AssignedTo"].ToString() : string.Empty;
                                dbData.AssignedBy = sqlDataReader[name: "AssignedBy"] != DBNull.Value ? sqlDataReader[name: "AssignedBy"].ToString() : string.Empty;
                                response.getTaskData.Add(dbData);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _sqlConnection.Close();
            }
            return response;
        }
        public async Task< List<ReadTaskData>> ReadTask(string username, string status, string assign, string priority)
        {
            List<ReadTaskData> response = new List<ReadTaskData>();

            try
            {
                string SqlQuery, strstatus = "",strpriority="";
                 if (status != "Status" )
                 {
                    strstatus = "and Status = @Status ";
                 }
                 if (priority != "Priority")
                 {
                    strpriority = "and Priority = @Priority ";
                 }
                    SqlQuery = "select * from Task_Table where " + assign + "  = @username " + strstatus + strpriority ;
                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue(parameterName: "@username", username);
                    sqlCommand.Parameters.AddWithValue(parameterName: "@Status", status);
                    sqlCommand.Parameters.AddWithValue(parameterName: "@Priority", priority);
            
                    _sqlConnection.Open();
                    using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (sqlDataReader.HasRows)
                        {
                           
                            while (await sqlDataReader.ReadAsync())
                            {
                                ReadTaskData dbData = new ReadTaskData();
                                dbData.Id = sqlDataReader[name: "Id"] != DBNull.Value ? Convert.ToInt32(sqlDataReader[name: "Id"]) : 0;
                                dbData.Title = sqlDataReader[name: "Title"] != DBNull.Value ? sqlDataReader[name: "Title"].ToString() : string.Empty;
                                dbData.Description = sqlDataReader[name: "Description"] != DBNull.Value ? sqlDataReader[name: "Description"].ToString() : string.Empty;
                                dbData.DueDate = sqlDataReader[name: "DueDate"] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[name: "DueDate"]) : DateTime.Now;
                                dbData.Status = sqlDataReader[name: "Status"] != DBNull.Value ? sqlDataReader[name: "Status"].ToString() : string.Empty;
                                dbData.Priority = sqlDataReader[name: "Priority"] != DBNull.Value ? sqlDataReader[name: "Priority"].ToString() : string.Empty;
                                dbData.AssignedTo = sqlDataReader[name: "AssignedTo"] != DBNull.Value ? sqlDataReader[name: "AssignedTo"].ToString() : string.Empty;
                                dbData.AssignedBy = sqlDataReader[name: "AssignedBy"] != DBNull.Value ? sqlDataReader[name: "AssignedBy"].ToString() : string.Empty;
                                response.Add(dbData);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            finally
            {

            }
            return response;
        }

        public async Task<UpdateTaskResponse> UpdateTask(UpdateTaskRequest updatedData)
        {
            UpdateTaskResponse response = new UpdateTaskResponse();
            response.IsSuccess = true;
            response.Message = "Success";
            try
            {
                //string SqlQuery = "Update Task_Table Set Status = @Status,Title = @Title,Description =@Description,AssignedTo = @AssignedTo,Priority = @Priority, DueDate = @DueDate, AssignedBy =@AssignedBy where Id = @Id";
                string SqlQuery = "Update Task_Table Set Status = @Status,Title = @Title,Description =@Description,AssignedTo = @AssignedTo,Priority = @Priority, DueDate = @DueDate where Id = @Id";
                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue(parameterName: "@Status", updatedData.Status);
                    sqlCommand.Parameters.AddWithValue(parameterName: "@Title", updatedData.Title);
                    sqlCommand.Parameters.AddWithValue(parameterName: "@Description", updatedData.Description);
                    sqlCommand.Parameters.AddWithValue(parameterName: "@AssignedTo", updatedData.AssignedTo);
                    sqlCommand.Parameters.AddWithValue(parameterName: "@Priority", updatedData.Priority);
                    sqlCommand.Parameters.AddWithValue(parameterName: "@DueDate", updatedData.DueDate);
                    //sqlCommand.Parameters.AddWithValue(parameterName: "@AssignedBy", request.AssignedBy);
                    sqlCommand.Parameters.AddWithValue(parameterName: "@Id", updatedData.Id);
                    _sqlConnection.Open();
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "something went wrong";
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                _sqlConnection.Close();
            }

            return response;
        }

        public async Task<GetTaskResponse> GetTask(string AssignedTo)
        {

            GetTaskResponse response = new GetTaskResponse();
            response.IsSuccess = true;
            response.Message = "successful";
            try
            {
                string SqlQuery = "select Id,Title,Description,DueDate,Status,Priority,AssignedTo,AssignedBy from Task_Table where AssignedTo = @AssignedTo;";
                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _sqlConnection))
                {

                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue(parameterName: "@AssignedTo", AssignedTo);
                    _sqlConnection.Open();
                    using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            response.getTaskData = new List<GetTaskData>();
                            while (await sqlDataReader.ReadAsync())
                            {
                                GetTaskData dbData = new GetTaskData();
                                dbData.Id = sqlDataReader[name: "Id"] != DBNull.Value ? Convert.ToInt32(sqlDataReader[name: "Id"]) : 0;
                                dbData.Title = sqlDataReader[name: "Title"] != DBNull.Value ? sqlDataReader[name: "Title"].ToString() : string.Empty;
                                dbData.Description = sqlDataReader[name: "Description"] != DBNull.Value ? sqlDataReader[name: "Description"].ToString() : string.Empty;
                                dbData.DueDate = sqlDataReader[name: "DueDate"] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[name: "DueDate"]) : DateTime.Now;
                                dbData.Status = sqlDataReader[name: "Status"] != DBNull.Value ? sqlDataReader[name: "Status"].ToString() : string.Empty;
                                dbData.Priority = sqlDataReader[name: "Priority"] != DBNull.Value ? sqlDataReader[name: "Priority"].ToString() : string.Empty;
                                dbData.AssignedTo = sqlDataReader[name: "AssignedTo"] != DBNull.Value ? sqlDataReader[name: "AssignedTo"].ToString() : string.Empty;
                                dbData.AssignedBy = sqlDataReader[name: "AssignedBy"] != DBNull.Value ? sqlDataReader[name: "AssignedBy"].ToString() : string.Empty;
                                response.getTaskData.Add(dbData);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            finally
            {
                _sqlConnection.Close();
            }
            return response;
        }

        public async Task<RejectTaskResponse> RejectTask(int id)
        {

            RejectTaskResponse response = new RejectTaskResponse();
            response.IsSuccess = true;
            response.Message = "success";
            try
            {
                string SqlQuery = "Update Task_Table set AssignedTo=AssignedBy where Id = @id";
                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue(parameterName: "@id", id);
                    _sqlConnection.Open();
                    int status = await sqlCommand.ExecuteNonQueryAsync();
                    if (status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Something Went wrong";
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                _sqlConnection.Close();
            }

            return response;
        }


        //public async Task<string> LoginModel(LoginModelRequest IdPass)
        //{
        //    LoginModelResponse response = new LoginModelResponse();
        //    response.IsSuccess = true;
        //    response.Message = "Successful";
        //    response.Username = "";
        //    var token = "";
        //    try { 
        //        string SqlQuery = "Select id from Login_Table where Username = @Username and Password = @Password";
        //        using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _sqlConnection))
        //        {
        public async Task<string> LoginModel(LoginModelRequest IdPass, string isAdmin)
        {
            string SqlQuery = "";
            if (Convert.ToBoolean(isAdmin))
            {
                SqlQuery = "Select id from AdminLoginTable where Username = @Username and Password = @Password";
            }
            else
            {
                SqlQuery = "Select id from Login_Table where Username = @Username and Password = @Password";
            }
            LoginModelResponse response = new LoginModelResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            response.Username = "";
            var token = "";
            try
            {
                //string SqlQuery = "Select id from Login_Table where Username = @Username and Password = @Password";
                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue(parameterName: "@Username", IdPass.username);
                    sqlCommand.Parameters.AddWithValue(parameterName: "@Password", IdPass.Password);

                    var username = IdPass.username;
                    _sqlConnection.Open();
                    using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            response.Username = username;   
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                response.Username = "";
            }
            finally
            {
                _sqlConnection.Close();
                if(response.Username != "")
                {
                    token = Generate(response);
                }
            }
            return token;
        }
       
        private string Generate(LoginModelResponse user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
             {
                new Claim("IsSuccess",user.IsSuccess.ToString()),
                new Claim("Message",user.Message),
                new Claim("Username",user.Username),
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
           _configuration["Jwt:Audience"],
           claims,
           expires: DateTime.Now.AddMinutes(15),
           signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<List<string>> GetUserlist()
        {
            List<string> response = new List<string>();  
            try
            {
                string SqlQuery = "select Username from Login_Table ;";
                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    _sqlConnection.Open();
                    using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            string username = "";
                            while (await sqlDataReader.ReadAsync())
                            {
                                username = sqlDataReader[name: "username"] != DBNull.Value ? sqlDataReader[name: "username"].ToString() : string.Empty;
                                response.Add(username);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            finally
            {
                _sqlConnection.Close();
            }
            return response;
        }

        public async Task<GetTaskResponse> FilterTask(string request , string username)
        {
            GetTaskResponse response = new GetTaskResponse();
            response.IsSuccess = true;
            response.Message = "successful";
            try
            {
                string SqlQuery = "select * from Task_Table where (Status = @request or Priority = @request or AssignedBy = @request) and AssignedTo = @username;";
                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue(parameterName: "@request", request);
                    sqlCommand.Parameters.AddWithValue(parameterName: "@username", username);
                    _sqlConnection.Open();
                    using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            response.getTaskData = new List<GetTaskData>();
                            while (await sqlDataReader.ReadAsync())
                            {
                                GetTaskData dbData = new GetTaskData();
                                dbData.Id = sqlDataReader[name: "Id"] != DBNull.Value ? Convert.ToInt32(sqlDataReader[name: "Id"]) : 0;
                                dbData.Title = sqlDataReader[name: "Title"] != DBNull.Value ? sqlDataReader[name: "Title"].ToString() : string.Empty;
                                dbData.Description = sqlDataReader[name: "Description"] != DBNull.Value ? sqlDataReader[name: "Description"].ToString() : string.Empty;
                                dbData.DueDate = sqlDataReader[name: "DueDate"] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[name: "DueDate"]) : DateTime.Now;
                                dbData.Status = sqlDataReader[name: "Status"] != DBNull.Value ? sqlDataReader[name: "Status"].ToString() : string.Empty;
                                dbData.Priority = sqlDataReader[name: "Priority"] != DBNull.Value ? sqlDataReader[name: "Priority"].ToString() : string.Empty;
                                dbData.AssignedTo = sqlDataReader[name: "AssignedTo"] != DBNull.Value ? sqlDataReader[name: "AssignedTo"].ToString() : string.Empty;
                                dbData.AssignedBy = sqlDataReader[name: "AssignedBy"] != DBNull.Value ? sqlDataReader[name: "AssignedBy"].ToString() : string.Empty;
                                response.getTaskData.Add(dbData);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _sqlConnection.Close();
            }
            return response;
        }

        public async Task<ReadTaskData> TaskById(string Id)
        {
             ReadTaskData dbData = new ReadTaskData();
            try
            {
                string SqlQuery = "select * from Task_Table where Id = @Id;";
                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue(parameterName: "@Id", Id);
                    _sqlConnection.Open();
                    using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            while (await sqlDataReader.ReadAsync())
                            {
                                //ReadTaskData dbData = new ReadTaskData();
                                dbData.Id = sqlDataReader[name: "Id"] != DBNull.Value ? Convert.ToInt32(sqlDataReader[name: "Id"]) : 0;
                                dbData.Title = sqlDataReader[name: "Title"] != DBNull.Value ? sqlDataReader[name: "Title"].ToString() : string.Empty;
                                dbData.Description = sqlDataReader[name: "Description"] != DBNull.Value ? sqlDataReader[name: "Description"].ToString() : string.Empty;
                                dbData.DueDate = sqlDataReader[name: "DueDate"] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[name: "DueDate"]) : DateTime.Now;
                                dbData.Status = sqlDataReader[name: "Status"] != DBNull.Value ? sqlDataReader[name: "Status"].ToString() : string.Empty;
                                dbData.Priority = sqlDataReader[name: "Priority"] != DBNull.Value ? sqlDataReader[name: "Priority"].ToString() : string.Empty;
                                dbData.AssignedTo = sqlDataReader[name: "AssignedTo"] != DBNull.Value ? sqlDataReader[name: "AssignedTo"].ToString() : string.Empty;
                                dbData.AssignedBy = sqlDataReader[name: "AssignedBy"] != DBNull.Value ? sqlDataReader[name: "AssignedBy"].ToString() : string.Empty;
                               
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            finally
            {

            }
            return dbData;

        }

        public async Task<Boolean> VBLogin(string username, string password)
        {
            bool res = false;
            try
            {
                string SqlQuery = "Select id from Login_Table where Username = @Username and Password = @Password";
                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue(parameterName: "@Username", username);
                    sqlCommand.Parameters.AddWithValue(parameterName: "@Password", password);
                    _sqlConnection.Open();
                    using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            res = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
            }
            finally
            {
                _sqlConnection.Close();
                
            }
            return res;
        }

    }
}
    


