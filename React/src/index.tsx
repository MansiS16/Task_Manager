import React from "react";
import ReactDOM from "react-dom/client";
import App from "./component/App";
import "../src/CSS/index.css";
import reportWebVitals from "./reportWebVitals";
import store from "./store";
import {Provider} from "react-redux";

// store.subscribe(() => console.log(store.getState()));

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <React.StrictMode>
    <Provider store={store}>
    <App />
    </Provider>
  </React.StrictMode>
);
reportWebVitals();
