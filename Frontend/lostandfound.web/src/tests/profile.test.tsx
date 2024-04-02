import { render, screen } from "@testing-library/react";
import { ProfileInner, UserProfile } from "components/profile/profile";
import { MemoryRouter } from "react-router-dom";

test("renders profile name", () => {
	var profile = new UserProfile();
	profile.name = "test name";
	render(<ProfileInner profile={profile} />, { wrapper: MemoryRouter });
	const name = screen.getByText(/test name/i);
	expect(name).toBeInTheDocument();
});

test("renders profile surname", () => {
	var profile = new UserProfile();
	profile.surname = "test surname";
	render(<ProfileInner profile={profile} />, { wrapper: MemoryRouter });
	const srnme = screen.getByText(/test surname/i);
	expect(srnme).toBeInTheDocument();
});

test("renders profile description", () => {
	var profile = new UserProfile();
	profile.description = "test description";
	render(<ProfileInner profile={profile} />, { wrapper: MemoryRouter });
	const description = screen.getByText(/test description/i);
	expect(description).toBeInTheDocument();
});

test("renders profile city", () => {
	var profile = new UserProfile();
	profile.city = "test city";
	render(<ProfileInner profile={profile} />, { wrapper: MemoryRouter });
	const cty = screen.getByText(/test city/i);
	expect(cty).toBeInTheDocument();
});

test("renders profile username", () => {
	var profile = new UserProfile();
	profile.username = "test username";
	render(<ProfileInner profile={profile} />, { wrapper: MemoryRouter });
	const usrnme = screen.getByText(/test username/i);
	expect(usrnme).toBeInTheDocument();
});

test("renders profile rating", () => {
	var profile = new UserProfile();
	profile.averageProfileRating = 2.22;
	render(<ProfileInner profile={profile} />, { wrapper: MemoryRouter });
	const rtg = screen.getByText(/2.22/i);
	expect(rtg).toBeInTheDocument();
});
