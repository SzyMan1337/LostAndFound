import { useContext } from "react";
import { Navigate, Outlet } from "react-router-dom";
import { userContext } from "userContext";

export default function Auth() {
	const userCtx = useContext(userContext);

	if (userCtx.user.isLogged === false) {
		return <Navigate to="/"></Navigate>;
	}
	return <Outlet></Outlet>;
}
