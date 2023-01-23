using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskManagerCrud.CommonLayer.Model;
using TaskManagerCrud.Controllers;
using TaskManagerCrud.ServiceLayer;
using System.Linq;
using System.Collections.Generic;
using static TaskManagerCrud.CommonLayer.Model.UserList;

namespace ApiTestProject
{
    [TestClass]
    public class TaskControllerTests
    {
        private Mock<ITaskServiceLayer> _taskServiceMock;
        private Fixture _fixture;
        private TaskController _controller;

        public TaskControllerTests()
        {
            _fixture = new Fixture();
            _taskServiceMock = new Mock<ITaskServiceLayer>();
        }

        [TestMethod]
        public async Task PostTask_ReturnOk()
        {
            var task = _fixture.Create<CreateTaskResponse>();
            var taskreq = _fixture.Create<CreatetaskRequest>();
            _taskServiceMock.Setup(repo => repo.CreateTask(It.IsAny<CreatetaskRequest>())).ReturnsAsync(task);
            _controller = new TaskController(_taskServiceMock.Object);
            var result = await _controller.CreateTask(taskreq);
            var obj = result as ObjectResult;
            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public async Task PostTask_ThrowException()
        {
            var task = _fixture.Create<CreateTaskResponse>();
            var taskreq = _fixture.Create<CreatetaskRequest>();
            _taskServiceMock.Setup(repo => repo.CreateTask(taskreq)).Throws(new Exception());
            _controller = new TaskController(_taskServiceMock.Object);

            var result = await _controller.CreateTask(taskreq);
            var obj = result as ObjectResult;
            Assert.AreEqual(400, obj.StatusCode);
           
        }

        [TestMethod]
        public async Task PutTask_ReturnOk()
        {
            var task = _fixture.Create<UpdateTaskResponse>();
            var taskreq = _fixture.Create<UpdateTaskRequest>();

            _taskServiceMock.Setup(repo => repo.UpdateTask(It.IsAny<UpdateTaskRequest>())).ReturnsAsync(task);
            _controller = new TaskController(_taskServiceMock.Object);

            var result = await _controller.UpdateTask(taskreq);
            var obj = result as ObjectResult;
            Assert.AreEqual(200, obj.StatusCode);

        }

        [TestMethod]
        public async Task PutTask_ThrowException()
        {
            var task = _fixture.Create<UpdateTaskResponse>();
            var taskreq = _fixture.Create<UpdateTaskRequest>();

            _taskServiceMock.Setup(repo => repo.UpdateTask(It.IsAny<UpdateTaskRequest>())).Throws(new Exception());
            _controller = new TaskController(_taskServiceMock.Object);

            var result = await _controller.UpdateTask(taskreq);
            var obj = result as ObjectResult;
            Assert.AreEqual(400, obj.StatusCode);
           
        }

        [TestMethod]
        public async Task DeleteTask_ReturnOk()
        {
            var task = _fixture.Create<DeleteTaskResponse>();

            _taskServiceMock.Setup(repo => repo.DeleteTask(It.IsAny<int>())).ReturnsAsync(task);
            _controller = new TaskController(_taskServiceMock.Object);

            var result = await _controller.DeleteTask(It.IsAny<int>());
            var obj = result as ObjectResult;
            Assert.AreEqual(200, obj.StatusCode);

        }


        [TestMethod]
        public async Task DeleteTask_ThrowException()
        {

            _taskServiceMock.Setup(repo => repo.DeleteTask(It.IsAny<int>())).Throws(new Exception());
            _controller = new TaskController(_taskServiceMock.Object);

            var result = await _controller.DeleteTask(It.IsAny<int>());
            var obj = result as ObjectResult;
            Assert.AreEqual(400, obj.StatusCode);

        }

        [TestMethod]
        public async Task RejectTask_ReturnOk()
        {
            var task = _fixture.Create<RejectTaskResponse>();
            var taskreq = _fixture.Create<UpdateTaskRequest>();

            _taskServiceMock.Setup(repo => repo.RejectTask( taskreq.Id)).ReturnsAsync(task);
            _controller = new TaskController(_taskServiceMock.Object);

            var result = await _controller.RejectTask(taskreq.Id);
            var obj = result as ObjectResult;
            Assert.AreEqual(200, obj.StatusCode);
           
        }

        [TestMethod]
        public async Task RejectTask_ThrowException()
        {
            var task = _fixture.Create<RejectTaskResponse>();
            var taskreq = _fixture.Create<UpdateTaskRequest>();

            _taskServiceMock.Setup(repo => repo.RejectTask(taskreq.Id)).Throws(new Exception());
            _controller = new TaskController(_taskServiceMock.Object);

            var result = await _controller.RejectTask(taskreq.Id);
            var obj = result as ObjectResult;
            Assert.AreEqual(400, obj.StatusCode);
          
        }

        [TestMethod]
        public async Task GetTask_ReturnOK()
        {
            Random rnd = new Random();

            string[] assigntoby = { "AssignedTo", "AssignedBy" };
            var assign = assigntoby[rnd.Next(2)];
            var username = _fixture.Create<LoginModelResponse>();
            var taskreq = _fixture.Create<CreatetaskRequest>();

            var task = _fixture.Create<List<ReadTaskData>>();
            _taskServiceMock.Setup(repo => repo.ReadTask(username.Username, taskreq.status, assign, taskreq.priority)).ReturnsAsync(task);
            _controller = new TaskController(_taskServiceMock.Object);

            var result = await _controller.ReadTask(username.Username, taskreq.status, assign, taskreq.priority);
            var obj = result as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
          
        }



        [TestMethod]
        public async Task GetTask_ThrowException()
        {
            Random rnd = new Random();
            string[] assigntoby = { "AssignedTo", "AssignedBy" };
            var assign = assigntoby[rnd.Next(2)];
            var username = _fixture.Create<LoginModelResponse>();
            var taskreq = _fixture.Create<CreatetaskRequest>();

            _taskServiceMock.Setup(repo => repo.ReadTask(username.Username, taskreq.status, assign, taskreq.priority)).Throws(new Exception());
            _controller = new TaskController(_taskServiceMock.Object);

            var result = await _controller.ReadTask(username.Username, taskreq.status, assign, taskreq.priority);
            var obj = result as ObjectResult;
            Assert.AreEqual(400, obj.StatusCode);
          
        }
    } 
}
