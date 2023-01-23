import axios from "axios";
import React, { useState } from "react";
import { Navigate } from "react-router-dom";

const LoginAdmin = () => {
  const [loading, setLoading] = useState<boolean>(false);
  const [username, setusername] = useState<string>("");
  const [password, setpassword] = useState<string>("");

const OnLogin = () =>{
  const isAdmin = localStorage.getItem('isAdmin')
  axios
    .post(`https://localhost:44310/api/Task/Login/${isAdmin}`, {
      username,
      password,
    })
    .then((response) => {
      localStorage.setItem("isSuccess", "true");
      localStorage.setItem("userDetails", response.data);
      console.log(response)
      localStorage.setItem("isAdmin", "true")
      if (localStorage.getItem("userDetails") === "") {
        localStorage.removeItem("userDetails");
        localStorage.removeItem("isSuccess");
        alert("invalid credential");
      } else {
        localStorage.setItem("username", "Mansi");
      }
      setTimeout(() => {
        localStorage.removeItem("userDetails");
      }, 1000 * 60 * 15);
      if (response.data) {
        setLoading(true);
      }
    });
}
if (loading ? loading : localStorage.getItem("username")) {
  return <Navigate to="/home" />;
}

  return (
    <>
      <div className="container py-5 h-100" style={{ background: "#6a11cb" }}>
        <div className="row d-flex justify-content-center align-items-center h-100">
          <div className="col-12 col-md-8 col-lg-6 col-xl-5">
            <div
              className="card bg-dark text-white"
              style={{ borderRadius: "1rem" }}
            >
              <div className="card-body p-5 text-center">
                <div className="mb-md-5 mt-md-4 pb-5">
                  <h2 className="fw-bold mb-2 text-uppercase">Login</h2>
                  <p className="text-white-50 mb-5">
                    Please enter your login and password!
                  </p>

                  <div className="htmlForm-outline htmlForm-white mb-4">
                  <label className="htmlForm-label" htmlFor="typeEmailX">
                      Email
                    </label>
                    <input
                      type="string"
                      id="typeEmailX"
                      className="htmlForm-control htmlForm-control-lg"
                      onChange={(e: any) => {
                        setusername(e.target.value);
                        
                      }}
                    />
                    
                  </div>

                  <div className="htmlForm-outline htmlForm-white mb-4">
                  <label className="htmlForm-label" htmlFor="typePasswordX">
                      Password
                    </label>
                    <input
                      type="password"
                      id="typePasswordX"
                      className="htmlForm-control htmlForm-control-lg"
                      onChange={(e: any) => {
                        setpassword(e.target.value);
                      }}
                    />
                   
                  </div>

                  <p className="small mb-5 pb-lg-2">
                    <a className="text-white-50" href="#!">
                      htmlForgot password?
                    </a>
                  </p>

                  <button onClick={OnLogin}
                    className="btn btn-outline-light btn-lg px-5"
                    type="submit"
                  >
                    Login
                  </button>

                  <div className="d-flex justify-content-center text-center mt-4 pt-2">
                    <a href="#!" className="text-white" style={{margin:"10px"}}>
                      <i className="bi bi-facebook"></i>
                    </a>
                    <a href="#!" className="text-white" style={{margin:"10px"}}>
                      <i className="bi bi-twitter"></i>
                    </a>
                    <a href="#!" className="text-white" style={{margin:"10px"}}>
                      <i className="bi bi-google"></i>
                    </a>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};
export default LoginAdmin;
