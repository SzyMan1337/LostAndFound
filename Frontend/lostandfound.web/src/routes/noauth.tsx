import { useContext, useEffect, useState } from "react";
import { Navigate, Outlet } from "react-router-dom";
import { userContext, UsrCont } from "userContext";

export default function NoAuth() {
	const userCtx = useContext(userContext);

	if (userCtx.user.isLogged === true)
		return (
			<div>
				Już jesteś zalogowany. Przenoszenie...
				<Navigate to={"/profile"} />
			</div>
		);
	return <Outlet></Outlet>;
}
