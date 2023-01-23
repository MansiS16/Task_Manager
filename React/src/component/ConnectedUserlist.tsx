import { useEffect, useState } from "react";
import { Link, Navigate } from "react-router-dom";
import Api from "./Api";

const ConnectedUserlist = (HOC: any, data: any) => {
  const [userlist, setUserlist] = useState([]);
  useEffect(() => {
    Api.GetUserList()
      .then((res) => res.json())
      .then((json) => setUserlist(json));
  }, [userlist]);

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
      <article className="article-header">
        <h1>Task Manager Application </h1>
        <Link to="/">
          <button style={{ marginLeft: "1100px" }} onClick={logout}>
            Logout
          </button>
        </Link>
      </article>
      <div className="form-container">
        <HOC userlist={userlist} data={data}></HOC>
      </div>
    </>
  );
};

export default ConnectedUserlist;
