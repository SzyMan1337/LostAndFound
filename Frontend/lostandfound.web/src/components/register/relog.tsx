import { refreshToken } from "commons";
import { useEffect, useState } from "react";
import { UsrCont } from "userContext";
export default function Relog({
	user,
	setUser,
}: {
	user: UsrCont;
	setUser: (usr: UsrCont) => void;
}) {
	const [ldg, setLdg] = useState(true);

	useEffect(() => {
		if (ldg) {
			setLdg(false);
			if (user.refreshToken)
				refreshToken(user.refreshToken).then((x) => {
					if (x)
						setUser({
							authToken: x?.accessToken,
							expirationDate: x?.accessTokenExpirationTime,
							isLogged: true,
							refreshToken: x.refreshToken,
						});
					else {
						setUser({
							authToken: null,
							expirationDate: null,
							isLogged: false,
							refreshToken: null,
						});
					}
				});
		}
	}, [user]);
	return <div className="spinner-border" role="status"></div>;
}
