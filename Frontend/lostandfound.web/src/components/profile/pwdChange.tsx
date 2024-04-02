import { LoginRequestType, login, changePwd } from "commons";
import { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Link } from "react-router-dom";
import { userContext, UsrCont } from "userContext";

export default function PwdChange() {
	const usrCtx = useContext(userContext);
	const nav = useNavigate();
	const [user, setUser] = useState({
		oldpwd: "",
		pwd: "",
		pwd2: "",
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
		if (user.oldpwd.length < 8) nerr.push(valErrors.pass1);
		if (user.pwd.length < 8) nerr.push(valErrors.pass2);
		if (user.pwd !== user.pwd2) nerr.push(valErrors.pass3);

		setVal(nerr);
		if (val.length > 0) return false;
		return true;
	}

	function onChange() {
		if (onValidate() && usrCtx.user.authToken) {
			changePwd(usrCtx.user.authToken, {
				Password: user.oldpwd,
				NewPassword: user.pwd,
			}).then((x) => {
				if (x !== undefined) {
					setUnd(false);
					nav("/profile");
				} else {
					setUnd(true);
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
				<p className="h3">Zmiana hasła</p>
				<div className="m-3">
					<div className="form-label text-start">
						poprzednie hasło:
					</div>
					<input
						name="oldpwd"
						type="password"
						className="form-control"
						value={user.oldpwd}
						placeholder="poprzednie hasło"
						onChange={(e) => handleChange(e)}
					></input>
					{val.includes(valErrors.pass1) && (
						<div className="ms-1 text-danger text-start">
							hasło musi zawierać conajmniej 8 znaków
						</div>
					)}
				</div>
				<div className="m-3">
					<div className="form-label text-start">nowe hasło:</div>
					<input
						name="pwd"
						type="password"
						className="form-control"
						value={user.pwd}
						placeholder="nowe hasło"
						onChange={(e) => handleChange(e)}
					></input>
					{val.includes(valErrors.pass2) && (
						<div className="ms-1 text-danger text-start">
							hasło musi zawierać conajmniej 8 znaków
						</div>
					)}
				</div>
				<div className="m-3">
					<div className="form-label text-start">
						powtórz nowe hasło:
					</div>
					<input
						name="pwd2"
						type="password"
						className="form-control"
						value={user.pwd2}
						placeholder="nowe hasło"
						onChange={(e) => handleChange(e)}
					></input>
					{val.includes(valErrors.pass3) && (
						<div className="ms-1 text-danger text-start">
							hasła muszą być identyczne
						</div>
					)}
				</div>
				{und && <div className="text-danger"> niepoprawne dane</div>}
				<div
					className="btn btn-primary rounded-5 m-3"
					onClick={() => onChange()}
				>
					Zmień
				</div>
			</div>
		</div>
	);
}

enum valErrors {
	pass1,
	pass2,
	pass3,
}
