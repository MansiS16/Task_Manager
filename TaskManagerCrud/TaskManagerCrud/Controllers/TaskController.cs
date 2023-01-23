using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagerCrud.CommonLayer.Model;
using TaskManagerCrud.ServiceLayer;
using static TaskManagerCrud.CommonLayer.Model.GetTask;

namespace TaskManagerCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase 
    {
        public readonly ITaskServiceLayer _crudSL;

        public TaskController(ITaskServiceLayer crudSL)
        {
            _crudSL = crudSL;
        }

        [HttpPost]
        [Route (template:"CreateTask")]
        public async Task<IActionResult> CreateTask(CreatetaskRequest taskData)
        {
            CreateTaskResponse response = null;
            try
            {
                response = await _crudSL.CreateTask(taskData);
            }
            catch (Exception ex)
            {
                return BadRequest("not found");
            }

            return Ok(response);
        }

        [HttpGet]
        [Route(template: "ReadTask/{username}/{status}/{assignby}/{priority}")]
        public async Task<IActionResult> ReadTask(string username, string status, string assignby, string priority)
        {
            List<ReadTaskData> response = null;
            try
            {
                response = await _crudSL.ReadTask(username, status, assignby, priority);
            }
            catch (Exception ex)
            {
                return BadRequest("not found");
            }

            return Ok(response);
        }

        [HttpPut]
        [Route(template: "UpdateTask")]
        public async Task<IActionResult> UpdateTask(UpdateTaskRequest updatedData)
        {
            UpdateTaskResponse response = null;
            try
            {
                response = await _crudSL.UpdateTask(updatedData);
            }
            catch (Exception ex)
            {
                return BadRequest("not found");
            }

            return Ok(response);
        }

        [HttpDelete]
        [Route( "DeleteTask/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            DeleteTaskResponse response = null;
            try
            {
                response = await _crudSL.DeleteTask(id);
            }
            catch (Exception ex)
            {
                return BadRequest("not found");
            }

            return Ok(response);
        }


        [HttpGet] 
        [Route ( "GetUserTask/{AssignedTo}")]
        public async Task<IActionResult> GetTask(string AssignedTo)
        {
            GetTaskResponse response = null;
            try
            {
                response = await _crudSL.GetTask(AssignedTo);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("GetUserList/")]
        public async Task<IActionResult> GetUserlist()
        {
            List<string>  response = null;
            try
            {
                response = await _crudSL.GetUserlist();
            }
            catch (Exception ex)
            {
            
            }

            return Ok(response);
        }


        [HttpPut]
        [Route(template: "RejectTask/{id}")]
        public async Task<IActionResult> RejectTask(int id)
        {
            RejectTaskResponse response = null;
            try
            {
                response = await _crudSL.RejectTask(id);
            }
            catch (Exception ex)
            {
                return BadRequest("not found");
            }

            return Ok(response);
        }


        [HttpPost]
        [Route(template: "Login/{isAdmin}")]
        public async Task<IActionResult> LoginModel(LoginModelRequest IdPass, string isAdmin)
        {
            string response = "";
            try
            {

                response = await _crudSL.LoginModel(IdPass,isAdmin);
                return Ok(response);
            }
            catch (Exception ex)
            {
                //response.IsSuccess = false;
                //response.Message = ex.Message;
            }

            return NotFound("User not Found");
        }

        //[HttpGet]
        //[Route("filterTask/{request}/{username}")]
        //public async Task<IActionResult> FilterTask(string request , string username)
        //{
        //    GetTaskResponse response = null;
        //    try
        //    {
        //        response = await _crudSL.FilterTask(request , username);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.IsSuccess = false;
        //        response.Message = ex.Message;
        //    }

        //    return Ok(response);
        //}



        //[HttpPost]
        //[Route(template: "Login")]
        //public async Task<IActionResult> LoginModel(LoginModelRequest request)
        //{
        //    string response = "";
        //    try
        //    {

        //        response = await _crudSL.LoginModel(request);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        //response.IsSuccess = false;
        //        //response.Message = ex.Message;
        //    }

        //    return NotFound("User not Found");
        //}

        [HttpGet]
        [Route("assignTask/{username}")]
        public async Task<IActionResult> GetTaskAssignedByMe(string username)
        {
            GetTaskResponse response = null;
            try
            {
                response = await _crudSL.GetTaskAssignedByMe(username);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }


        [HttpGet]
        [Route(template: "TaskById/{Id}")]
        public async Task<IActionResult> TaskById(string Id)
        {
            ReadTaskData response = null;
            try
            {
                response = await _crudSL.TaskById(Id);
            }
            catch (Exception ex)
            {
                // response.IsSuccess = false;
                // response.Message = ex.Message;
                return BadRequest("not found");
            }

            return Ok(response);
            //return StatusCode(200,response);
        }

        [HttpPost]
        [Route(template: "vbLogin")]
        public async Task<IActionResult> VBLogin(string username, string password)
        {
            try
            {
                return Ok(await _crudSL.VBLogin( username,password));
            }
            catch (Exception ex)
            {
                //response.IsSuccess = false;
                //response.Message = ex.Message;
            }

            return NotFound("User not Found");
        }



    }

}
