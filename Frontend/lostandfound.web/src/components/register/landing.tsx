import { useContext } from "react";
import { Link, Navigate } from "react-router-dom";
import { userContext } from "userContext";

export default function Landing() {
	const usrCtx = useContext(userContext);
	if (!usrCtx.user.isLogged) {
		return <Invitation />;
	}
	return <Navigate to={"/posts"} replace={true}></Navigate>;
}

function Invitation() {
	return (
		<div className="w-25 m-auto bg-light border rounded-5 p-3 border-dark shadow-lg">
			<div className="mb-3">
				<div className="h4">Uwaga!</div>
				Nie jesteś zalogowany! Niezalogowani użytkownicy nie mają
				dostępu do serwisu. <Link to={"/login"}>Zaloguj się.</Link>
			</div>
			<p>
				Nie masz jeszcze konta?{" "}
				<Link to={"/register"}>Zarejestruj się</Link> i dołącz do grona
				użytkowników.
			</p>
		</div>
	);
}
