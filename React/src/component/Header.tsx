import React from 'react'
import { Link } from 'react-router-dom'

export const Header = () => {

    const logout = () => {
        localStorage.removeItem("username");
        localStorage.removeItem("isSuccess");
        localStorage.removeItem("userDetails");
      };


  return (
    <article className="article-header">
        <h1>Task Manager Application </h1>
        <Link to="/">
          <button style={{ marginLeft: "1100px" }} onClick={logout}>
            Logout
          </button>

        </Link> {(localStorage.getItem("isadmin"))?<button > Add new </button>: ""}
      </article>
  )
}
