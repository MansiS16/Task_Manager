import { useState, useEffect } from "react";
import Api from "./Api";
import { ITask } from "./Task.type";
import "../CSS/TaskForm.style.css";
import ConnectedUserlist from "./ConnectedUserlist";
import moment from "moment";
import { Link, useNavigate } from "react-router-dom";

type Prop = {
  userlist: [];
  data: ITask;
};
type Props = {
  data: ITask;
};

const EditTask = (props: Prop) => {
  const { data, userlist } = props;
  const [Id, setId] = useState(data.id);
  const [Title, setTitle] = useState(data.title);
  const [Description, setDescription] = useState(data.description);
  const [DueDate, setDueDate] = useState(data.dueDate);
  const [Status, setStatus] = useState(data.status);
  const [Priority, setPriority] = useState(data.priority);
  const [AssignedTo, setAssignedTo] = useState(data.assignedTo);
  const [AssignedBy, setAssignedBy] = useState(data.assignedBy);
  const [formattedDate, setformattedDate] = useState(
    new Date(moment(data.dueDate).format("MM/DD/YYYY"))
  );
  const navigate = useNavigate();

  useEffect(() => {
    setformattedDate(new Date(moment(DueDate).format("MM/DD/YYYY")));
  });

  const onSubmitBtnClickHnd = (e: any) => {
    e.preventDefault();
    const updatedData: ITask = {
      id: Id,
      title: Title,
      description: Description,
      dueDate: DueDate,
      status: Status,
      priority: Priority,
      assignedTo: AssignedTo,
      assignedBy: AssignedBy,
    };
    Api.UpdateTask(updatedData);
    navigate("/home");
  };

  return (
    <div className="form-container">
      <div>
        <h3>Update Task</h3>
      </div>
      <div style={{ marginLeft: "200px" }}>
        <form onSubmit={onSubmitBtnClickHnd} style={{ textAlign: "left" }}>
          <div className="form-group row">
            <label htmlFor="inputEmail3" className="col-sm-2 col-form-label">
              Title
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
              Description
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
              DueDate
            </label>
            <div className="col-sm-10">
              <input
                style={{ height: "40px", width: "500px" }}
                type="Date"
                value={formattedDate.toLocaleDateString("en-CA")}
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
              Status
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
                <option hidden>{Status}</option>
                <option value="InProgress">In Progress</option>
                <option value="ToDo">ToDo</option>
                <option value="Complete">Complete</option>
              </select>
            </div>
          </div>
          <div className="form-group row">
            <label htmlFor="inputEmail3" className="col-sm-2 col-form-label">
              Priority
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
                <option hidden>{Priority}</option>
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
                <option hidden>{AssignedTo}</option>
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
            <input type="submit" value="Update Task" />
          </div>
        </form>
      </div>
    </div>
    // </>
  );
};
const Edit = (props: Props) => {
  const { data } = props;
  return ConnectedUserlist(EditTask, data);
};
export default Edit;
