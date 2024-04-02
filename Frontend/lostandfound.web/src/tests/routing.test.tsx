import { render, screen } from "@testing-library/react";
import { MemoryRouter, Route, Routes } from "react-router-dom";
import Auth from "routes/auth";
import NoAuth from "routes/noauth";
import { userContext, UsrCont } from "userContext";

const routes = (
	<Routes>
		<Route path="/auth" element={<Auth />}>
			<Route path="" element={<div data-testid={"authdiv"} />}></Route>
		</Route>
		<Route path="/noauth" element={<NoAuth />}>
			<Route path="" element={<div data-testid={"noauthdiv"} />}></Route>
		</Route>
		<Route path="*" element={<div data-testid={"root"} />}></Route>
	</Routes>
);

test("correctly renders auth route", () => {
	const user = new UsrCont();
	user.isLogged = true;
	render(
		<userContext.Provider
			value={{ user: user, setUser: (arg: UsrCont) => {} }}
		>
			<MemoryRouter initialEntries={["/auth/"]}>{routes}</MemoryRouter>
		</userContext.Provider>
	);
	var authDiv = screen.queryByTestId("authdiv");
	expect(authDiv).toBeInTheDocument();
});

test("correctly renders noauth route", () => {
	const user = new UsrCont();
	user.isLogged = false;
	render(
		<userContext.Provider
			value={{ user: user, setUser: (arg: UsrCont) => {} }}
		>
			<MemoryRouter initialEntries={["/noauth/"]}>{routes}</MemoryRouter>
		</userContext.Provider>
	);
	var noauthDiv = screen.queryByTestId("noauthdiv");
	expect(noauthDiv).toBeInTheDocument();
});

test("navigates when authed in noauth ", () => {
	const user = new UsrCont();
	user.isLogged = true;
	render(
		<userContext.Provider
			value={{ user: user, setUser: (arg: UsrCont) => {} }}
		>
			<MemoryRouter initialEntries={["/noauth/"]}>{routes}</MemoryRouter>
		</userContext.Provider>
	);
	var noauthDiv = screen.queryByTestId("root");
	expect(noauthDiv).toBeInTheDocument();
});

test("navigates when not authed in auth ", () => {
	const user = new UsrCont();
	user.isLogged = false;
	render(
		<userContext.Provider
			value={{ user: user, setUser: (arg: UsrCont) => {} }}
		>
			<MemoryRouter initialEntries={["/auth/"]}>{routes}</MemoryRouter>
		</userContext.Provider>
	);
	var noauthDiv = screen.queryByTestId("root");
	expect(noauthDiv).toBeInTheDocument();
});
