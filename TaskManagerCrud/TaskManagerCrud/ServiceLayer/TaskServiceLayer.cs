using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerCrud.CommonLayer.Model;
using TaskManagerCrud.RepositoryLayer;
using static TaskManagerCrud.CommonLayer.Model.GetTask;

namespace TaskManagerCrud.ServiceLayer
{
    public class TaskServiceLayer : ITaskServiceLayer
    {
        public readonly ITaskRepositoryLayer _crudRL;

        public TaskServiceLayer(ITaskRepositoryLayer crudRL)
        {
            _crudRL = crudRL; 
        }
        public async Task<CreateTaskResponse> CreateTask(CreatetaskRequest taskData)
        {
            return await _crudRL.CreateTask(taskData); 
        }

        public async Task<DeleteTaskResponse> DeleteTask(int id)
        {
            return await _crudRL.DeleteTask(id);
        }

        public async Task<List<ReadTaskData>> ReadTask( string request, string filter, string assignby, string priority)
        {
            return await _crudRL.ReadTask( request, filter, assignby, priority);
        }

        public async Task<UpdateTaskResponse> UpdateTask(UpdateTaskRequest updatedData)
        {
            return await _crudRL.UpdateTask(updatedData);
        }

        public async Task<GetTaskResponse> GetTask(string AssignedTo)
        {
            return await _crudRL.GetTask(AssignedTo);
        }

        public async Task<RejectTaskResponse> RejectTask(int id)
        {
            return await _crudRL.RejectTask(id);
        }

        public async Task<string> LoginModel(LoginModelRequest IdPass, string isAdmin)
        {
            return await _crudRL.LoginModel(IdPass, isAdmin);
        }

        public async Task<List<string>> GetUserlist()
        {
            return await _crudRL.GetUserlist();
        }

        public async Task<GetTaskResponse> FilterTask(string request , string username)
        {
            return await _crudRL.FilterTask(request , username);
        }

        public async Task<ReadTaskData> TaskById(string Id)
        {
            return await _crudRL.TaskById(Id);
        }

        public async Task<Boolean> VBLogin (string username, string password)
        {
            return await _crudRL.VBLogin(username, password);
        }

        public async Task<GetTaskResponse> GetTaskAssignedByMe(string username)
        {
            return await _crudRL.GetTaskAssignedByMe(username);
        }
    }
}
