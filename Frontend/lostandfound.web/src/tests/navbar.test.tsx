import { render, screen } from "@testing-library/react";
import Navbar from "components/navbar";
import { userContext, UsrCont } from "userContext";
import { MemoryRouter } from "react-router-dom";

test("renders login button when not logged", () => {
	const user = new UsrCont();
	user.isLogged = false;
	render(
		<userContext.Provider
			value={{ user: user, setUser: (arg: UsrCont) => {} }}
		>
			<Navbar />
		</userContext.Provider>,
		{ wrapper: MemoryRouter }
	);
	var loginBtn = screen.getByTestId("btnsignin");
	var logoutBtn = screen.queryByTestId("btnsignout");
	var profileLnk = screen.queryByTestId("linktoprofile");

	expect(loginBtn).toBeInTheDocument();
	expect(logoutBtn).not.toBeInTheDocument();
	expect(profileLnk).not.toBeInTheDocument();
});

test("renders logout button and profile link when logged in", () => {
	const user = new UsrCont();
	user.isLogged = true;
	render(
		<userContext.Provider
			value={{ user: user, setUser: (arg: UsrCont) => {} }}
		>
			<Navbar />
		</userContext.Provider>,
		{ wrapper: MemoryRouter }
	);

	var logoutBtn = screen.getByTestId("btnsignout");
	var profileLnk = screen.getByTestId("linktoprofile");
	var loginBtn = screen.queryByTestId("btnsignin");

	expect(logoutBtn).toBeInTheDocument();
	expect(profileLnk).toBeInTheDocument();
	expect(loginBtn).not.toBeInTheDocument();
});
