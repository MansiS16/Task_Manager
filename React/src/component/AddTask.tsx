import { useState } from "react";
import Api from "./Api";
import { ITask } from "./Task.type";
import "../CSS/TaskForm.style.css";
import ConnectedUserlist from "./ConnectedUserlist";
import { task_data } from "../actions";
import { useDispatch } from "react-redux";
import { Link, Navigate, useNavigate } from "react-router-dom";

type Props = {
  userlist: [];
  data: ITask;
};

const AddTask = (props: Props) => {
  const [Id, setId] = useState("0");
  const [Title, setTitle] = useState("");
  const [Description, setDescription] = useState("");
  const [DueDate, setDueDate] = useState("");
  const [Status, setStatus] = useState("");
  const [Priority, setPriority] = useState("");
  const [AssignedTo, setAssignedTo] = useState("");
  const [AssignedBy, setAssignedBy] = useState(
    localStorage.getItem("username")
  );

  const { userlist, data } = props;
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const onSubmitBtnClickHnd = (e: any) => {
    e.preventDefault();
    const data: ITask = {
      id: 0,
      title: Title,
      description: Description,
      dueDate: DueDate,
      status: Status,
      priority: Priority,
      assignedTo: AssignedTo,
      assignedBy: AssignedBy,
    };

    Api.AddTask(data);
    Api.GetUserTask()
      .then((res) => res.json())
      .then((json) => {
        return dispatch(task_data(json.getTaskData));
      });

    navigate("/home");
  };

  return (
    <>
      <div className="form-container">
        <div>
          <h3>Add Task</h3>
        </div>
        <div style={{ marginLeft: "200px" }}>
          <form onSubmit={onSubmitBtnClickHnd} style={{ textAlign: "left" }}>
            <div className="form-group row">
              <label htmlFor="inputEmail3" className="col-sm-2 col-form-label">
                Title:
              </label>
              <div className="col-sm-10">
                <input
                  style={{ height: "40px", width: "500px" }}
                  type="text"
                  value={Title}
                  onChange={(e: any) => {
                    setTitle(e.target.value);
                  }}
                  className="form-control"
                  id="inputEmail3"
                  placeholder="Title"
                />
              </div>
            </div>
            <div className="form-group row">
              <label htmlFor="inputEmail3" className="col-sm-2 col-form-label">
                Description:
              </label>
              <div className="col-sm-10">
                <input
                  style={{ height: "40px", width: "500px" }}
                  type="text"
                  value={Description}
                  onChange={(e: any) => {
                    setDescription(e.target.value);
                  }}
                  className="form-control"
                  id="inputEmail3"
                  placeholder="Description"
                />
              </div>
            </div>
            <div className="form-group row">
              <label htmlFor="inputEmail3" className="col-sm-2 col-form-label">
                DueDate:
              </label>
              <div className="col-sm-10">
                <input
                  style={{ height: "40px", width: "500px" }}
                  type="date"
                  value={DueDate}
                  onChange={(e: any) => {
                    setDueDate(e.target.value);
                  }}
                  className="form-control"
                  id="inputEmail3"
                  placeholder="DueDate"
                />
              </div>
            </div>

            <div className="form-group row">
              <label htmlFor="inputEmail3" className="col-sm-2 col-form-label">
                Status:
              </label>
              <div className="col-sm-10">
                <select
                  style={{ height: "40px", width: "500px" }}
                  className="custom-select"
                  id="inputGroupSelect01"
                  onChange={(e: any) => {
                    setStatus(e.target.value);
                  }}
                >
                  <option selected hidden>
                    Choose...
                  </option>
                  <option value="InProgress">In Progress</option>

                  <option value="ToDo">ToDo</option>
                  <option value="Complete">Complete</option>
                </select>
              </div>
            </div>
            <div className="form-group row">
              <label htmlFor="inputEmail3" className="col-sm-2 col-form-label">
                Priority:
              </label>
              <div className="col-sm-10">
                <select
                  style={{ height: "40px", width: "500px" }}
                  className="custom-select"
                  id="inputGroupSelect01"
                  onChange={(e: any) => {
                    setPriority(e.target.value);
                  }}
                >
                  <option selected hidden>
                    Choose...
                  </option>
                  <option value="Low">Low</option>
                  <option value="Medium">Medium</option>
                  <option value="High">High</option>
                </select>
              </div>
            </div>
            <div className="form-group row">
              <label htmlFor="inputEmail3" className="col-sm-2 col-form-label">
                AssignedTo:
              </label>
              <div className="col-sm-10">
                <select
                  style={{ height: "40px", width: "500px" }}
                  className="custom-select"
                  id="inputGroupSelect01"
                  onChange={(e: any) => {
                    setAssignedTo(e.target.value);
                  }}
                >
                  <option selected hidden>
                    Choose...
                  </option>
                  {userlist.map((user) => {
                    return <option value={user}>{user}</option>;
                  })}
                </select>
              </div>
            </div>

            <div>
              <Link to="/home">
                <input style={{ margin: "10px" }} type="button" value="Back" />
              </Link>

              <input type="submit" value="Add Task" />
            </div>
          </form>
        </div>
      </div>
    </>
  );
};

const Add = () => {
  const data: ITask = {
    id: 0,
    title: "",
    description: "",
    dueDate: "",
    status: "",
    priority: "",
    assignedTo: "",
    assignedBy: "",
  };
  return ConnectedUserlist(AddTask, data);
};
export default Add;
