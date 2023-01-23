import { url } from "inspector";
import React from "react";
import { useNavigate } from "react-router-dom";

const MainPage = () => {
  const navigate = useNavigate();
  return (
    <>
      <div
        style={{
          backgroundImage: "url(/main.jpg)",
          height: "100vh",
          backgroundRepeat: "no-repeat",
        }}
      >
        <h1
          style={{
            textAlign: "center",
            paddingTop: "4%",
            fontFamily: "Georgia, serif",
            fontSize: "3rem",
          }}
        >
          {" "}
          Welcome To Task Manager
        </h1>
        <button
          className="btn btn-dark"
          style={{
            marginLeft: "50%",
            marginTop: "12%",
            width: "15%",
            fontSize: "1.5rem",
            fontFamily: "Georgia, serif",
          }}
          onClick={() => {localStorage.setItem("isAdmin", "true");
          navigate("/Adlogin");
        }}
        >
          Admin
        </button>
        {/* </div>  */}

        {/* <div className="btn btn-primary" style={{marginLeft: "55%" , marginTop:"12%"}} onClick={() => localStorage.setItem("isAdmin", "true")}> */}
        <button
          className="btn btn-dark"
          style={{
            marginLeft: "50%",
            marginTop: "5%",
            width: "15%",
            fontSize: "1.5rem",
            fontFamily: "Georgia, serif",
          }}
          onClick={() => {
            localStorage.setItem("isAdmin", "false");
            navigate("/");
          }}
        >
          User
        </button>
      </div>
      {/* </div> */}
      {/* </div> */}
    </>
  );
};
export default MainPage;
