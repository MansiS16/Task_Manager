import axios from "axios";
import { ITask } from "./Task.type";

async function GetUserList() {
  var result = await fetch("https://localhost:44310/api/Task/GetUserList");
  return result;
}

function GetUserTask() {
  let username = localStorage.getItem("username");
  var result = fetch(
    `https://localhost:44310/api/Task/GetUserTask/${username}`
  );
  return result;
}

function GetTaskAssignedByMe() {
  let username = localStorage.getItem("username");
  var result = fetch(`https://localhost:44310/api/Task/assignTask/${username}`);
  return result;
}

function AddTask(data: ITask) {
  axios
    .post("https://localhost:44310/api/Task/CreateTask", data)
    .then((res) => {
      console.log(res);
    })
    .catch(function (error) {
      console.log(error);
    });
}

function DeleteTask(data: ITask) {
  let confirm = window.confirm("Are you sure want to delete the record ?");
  if (confirm) {
    axios
      .delete(`https://localhost:44310/api/Task/DeleteTask/${data.id}`)
      .then((res) => {
        console.log(res);
        alert("Item has been deleted..");
      })
      .catch(function (error) {
        console.log(error);
      });
  }
}

function RejectTask(data: ITask) {
  let confirm = window.confirm("Are you sure want to reject the task ?");
  if (confirm) {
    axios
      .put(`https://localhost:44310/api/Task/RejectTask/${data.id}`)
      .then((res) => {
        console.log(res);
      })
      .catch(function (error) {
        console.log(error);
      });
  }
}

function UpdateTask(data: ITask) {
  axios
    .put("https://localhost:44310/api/Task/UpdateTask", data)
    .then((res) => {
      console.log(res);
    })
    .catch(function (error) {
      console.log(error);
    });
}

function GetUpdate(){
 window.location.reload() 
}


export default {
  GetUserTask,
  GetUserList,
  AddTask,
  DeleteTask,
  RejectTask,
  UpdateTask,
  GetTaskAssignedByMe,
  GetUpdate,
};
