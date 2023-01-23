import { useState } from "react";
import { ITask, PageEnum } from "./Task.type";
import TaskList from "./TaskList";
import "../CSS/Home.style.css";
import { Link, Navigate } from "react-router-dom";
import Api from "./Api";
import { task_data } from "../actions";
import { useDispatch } from "react-redux";
import { Header } from "./Header";

type Props = {
  pull_data_for_edit: (data: ITask) => void;
};

const Home = (props: Props) => {
  const { pull_data_for_edit } = props;
  const [shownPage, setShownPage] = useState(PageEnum.list);
  const dispatch = useDispatch();

  const onAddTaskClickHnd = () => {};

  const deleteTask = (data: ITask) => {
    Api.DeleteTask(data);
    Api.GetUserTask()
      .then((res) => res.json())
      .then((json) => {
        return dispatch(task_data(json.getTaskData));
      });
  };

  const RejectTask = (data: ITask) => {
    Api.RejectTask(data);
    Api.GetUserTask()
      .then((res) => res.json())
      .then((json) => {
        return dispatch(task_data(json.getTaskData));
      });
  };

  const logout = () => {
    localStorage.removeItem("username");
    localStorage.removeItem("isSuccess");
    localStorage.removeItem("userDetails");
  };
  if (localStorage.getItem("username") === null) {
    return <Navigate to="/" />;
  }

  return (
    <>
    <Header/>

      <section className="section-content">
        {shownPage === PageEnum.list && (
          <>
            <Link to="/add">
              <input
                style={{ marginRight: "20px" }}
                type="button"
                value="Add Task"
                onClick={onAddTaskClickHnd}
                className="add-employee-btn"
              />
            </Link>

            <TaskList
              onDeleteClickHnd={deleteTask}
              onReject={RejectTask}
              pulltaskforedit={pull_data_for_edit}
            />
          </>
        )}
      </section>
    </>
  );
};

export default Home;
