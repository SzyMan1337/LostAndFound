import { useContext, useState } from "react";
import { Link } from "react-router-dom";
import { userContext, UsrCont } from "userContext";
import { login, LoginRequestType } from "commons";

export default function Login() {
	const usrCtx = useContext(userContext);

	const [user, setUser] = useState({
		name: "",
		email: "",
		pwd: "",
	});
	const [val, setVal] = useState([] as valErrors[]);
	const [und, setUnd] = useState(false);
	const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
		setUser({
			...user,
			[event.target.name]: event.target.value,
		});
	};

	function onValidate() {
		var nerr = [] as valErrors[];
		const mailexp: RegExp = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i;
		if (!mailexp.test(user.email)) nerr.push(valErrors.mail);
		if (user.pwd.length < 8) nerr.push(valErrors.pass1);
		setVal(nerr);
		if (val.length > 0) return false;
		return true;
	}

	function onLogin() {
		let lgn: LoginRequestType = { email: user.email, password: user.pwd };
		if (onValidate()) {
			login(lgn).then((x) => {
				if (x !== undefined) {
					setUnd(false);
					usrCtx.setUser({
						isLogged: true,
						authToken: x.accessToken,
						refreshToken: x.refreshToken,
						expirationDate: x.accessTokenExpirationTime,
					} as UsrCont);
				} else {
					setUnd(true);
					usrCtx.setUser({ isLogged: false } as UsrCont);
				}
			});
		}
	}

	return (
		<div className="d-flex justify-content-evenly align-items-center h-75">
			<div
				className="bg-light border border-dark rounded-5 shadow p-3 pb-0 "
				style={{
					width: "15%",
					minWidth: "500px",
				}}
			>
				<p className="h3">Logowanie</p>
				<div className="m-3">
					<div className="form-label text-start">e-mail:</div>
					<input
						name="email"
						type="text"
						className="form-control"
						value={user.email}
						placeholder="e-mail"
						onChange={(e) => handleChange(e)}
					></input>
					{val.includes(valErrors.mail) && (
						<div className="ms-1 text-danger text-start">
							e-mail musi być poprawny
						</div>
					)}
				</div>
				<div className="m-3">
					<div className="form-label text-start">hasło:</div>
					<input
						name="pwd"
						type="password"
						className="form-control"
						value={user.pwd}
						placeholder="hasło"
						onChange={(e) => handleChange(e)}
					></input>
					{val.includes(valErrors.pass1) && (
						<div className="ms-1 text-danger text-start">
							hasło musi zawierać conajmniej 8 znaków
						</div>
					)}
				</div>
				{und && <div className="text-danger"> niepoprawne dane</div>}
				<div
					className="btn btn-primary rounded-5"
					onClick={() => onLogin()}
				>
					Zaloguj
				</div>
				<p className="mt-3">
					Nie masz konta?{" "}
					<Link className="" to={"/register"}>
						Zarejestruj się
					</Link>
				</p>
			</div>
			<div className="w-25 fs-5">
				<div className="m-2">Masz już konto?</div>
				Dobrze cię znowu widzieć! Mamy nadzieję, że zostaniesz z nami na
				dłużej.
			</div>
		</div>
	);
}

enum valErrors {
	mail,
	pass1,
}
