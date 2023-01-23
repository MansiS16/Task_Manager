import  { useState } from "react";
import { Formik, Field, Form, ErrorMessage } from "formik";
import * as Yup from "yup";
import axios from "axios";
import { Navigate } from "react-router-dom";

const Signin = () => {
  const [loading, setLoading] = useState<boolean>(false);
  const [message, setMessage] = useState<string>("");
  const [username, setusername] = useState<string>("");

  const initialValues: {
    username: string;
    password: string;
  } = {
    username: "",
    password: "",
  };

  const validationSchema = Yup.object().shape({
    username: Yup.string().required("This field is required!"),
    password: Yup.string().required("This field is required!"),
  });

  async function handleLogin(formValue: {
    username: string;
    password: string;
  }) {
    const { username, password } = formValue;
    setMessage("");
    setusername(username);
const isAdmin = localStorage.getItem('isAdmin')
    return await axios
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
          localStorage.setItem("username", username);
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
      <h2
        className=" d-flex justify-content-center"
        style={{ marginTop: "5%" }}
      >
        Welcome to Task Manager Application.
      </h2>
      <div
        className="col-md-12"
        style={{
          marginTop: "10%",
          height: "auto",
          width: "40%",
          margin: "auto",
        }}
      >
        <div className="card card-container">
          <Formik
            initialValues={initialValues}
            validationSchema={validationSchema}
            onSubmit={handleLogin}
          >
            <Form>
              <div className="form-group">
                <label htmlFor="username">Username</label>
                <Field
                  name="username"
                   style={{ height: "40px", width: "98%", marginRight:"2px" }}
                  type="text"
                  className="form-control"
                />
                <ErrorMessage
                  name="username"
                  component="div"
                  className="alert alert-danger"
                />
              </div>

              <div className="form-group">
                <label htmlFor="password">Password</label>
                <Field
                  name="password"
                  style={{ height: "40px", width: "98%", marginRight:"2px",marginLeft:"4px" }}
                  type="password"
                  className="form-control"
                />
                <ErrorMessage
                  name="password"
                  component="div"
                  className="alert alert-danger"
                />
              </div>
              <div className="form-group justify-content-center">
                <button
                  style={{ marginLeft: "35%", width: "30%" }}
                  type="submit"
                  className="btn btn-primary btn-block"
                  disabled={loading}
                >
                  {loading && (
                    <span className="spinner-border spinner-border-sm"></span>
                  )}
                  <span>Login</span>
                </button>
              </div>

              {message && (
                <div className="form-group">
                  <div className="alert alert-danger" role="alert">
                    {message}
                  </div>
                </div>
              )}
            </Form>
          </Formik>
        </div>
      </div>
    </>
  );
};

export default Signin;
