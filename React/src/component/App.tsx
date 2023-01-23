import { BrowserRouter, Route, Routes } from "react-router-dom";
import Home from "./Home";
import Signin from "./Signin";
import { useSelector, useDispatch } from "react-redux";
import { task_data } from "../actions";
import { ITask } from "./Task.type";
import { useEffect, useState } from "react";
import Api from "./Api";
import Add from "./AddTask";
import Edit from "./EditTask";
import MainPage  from "./MainPage";
import LoginAdmin from "./Adminn/LoginAdmin";


const App = () => {
  const [task, settask] = useState({} as ITask);

  const myState = useSelector((state: any) => state.task_detail_reducer);
  const dispatch = useDispatch();
  useEffect(() => {
    Api.GetUserTask()
      .then((res) => res.json())
      .then((json) => {
        return dispatch(task_data(json.getTaskData));
      });
  }, []);

  const pull_data = (data: ITask) => {
    settask(data);
  };

  return (
    <div>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Signin />} />
          <Route path="/main" element={<MainPage />} />
         
          <Route
            path="/home"
            element={<Home pull_data_for_edit={pull_data} />}
          />
          <Route path="/add" element={<Add />} />
          <Route path="/edit" element={<Edit data={task} />} />
          <Route path="/Adlogin" element={<LoginAdmin />} />
        </Routes>
      </BrowserRouter>
    </div>
  );
};

export default App;
