import React from "react";
import ReactDOM from "react-dom/client";
import "bootstrap/dist/js/bootstrap.min.js";
import "./main.scss";
import { BrowserRouter } from "react-router-dom";
import MainRouter from "routes/main";

const root = ReactDOM.createRoot(
	document.getElementById("root") as HTMLElement
);
root.render(
	<React.StrictMode>
		<BrowserRouter>
			<MainRouter></MainRouter>
		</BrowserRouter>
	</React.StrictMode>
);
