import { render, screen } from "@testing-library/react";
import { ProfileInner, UserProfile } from "components/profile/profile";
import { ProfileEditInner } from "components/profile/profileEdit";
import { MemoryRouter } from "react-router-dom";

test("renders prof editor username", () => {
	var profile = new UserProfile();
	profile.username = "test username";
	var setProfile = (x: UserProfile) => {};
	var handeSave = () => {};
	render(
		<ProfileEditInner
			profile={profile}
			handleSave={handeSave}
			setProfile={setProfile}
		/>
	);
	const name = screen.queryByText(/test username/i);
	expect(name).toBeInTheDocument();
});

test("renders profile editor name", () => {
	var profile = new UserProfile();
	profile.name = "test name";
	var setProfile = (x: UserProfile) => {};
	var handeSave = () => {};
	render(
		<ProfileEditInner
			profile={profile}
			handleSave={handeSave}
			setProfile={setProfile}
		/>
	);
	const name = screen.queryByDisplayValue(/test name/i);
	expect(name).toBeInTheDocument();
});
test("renders profile editor surname", () => {
	var profile = new UserProfile();
	profile.surname = "test surname";
	var setProfile = (x: UserProfile) => {};
	var handeSave = () => {};
	render(
		<ProfileEditInner
			profile={profile}
			handleSave={handeSave}
			setProfile={setProfile}
		/>
	);
	const name = screen.queryByDisplayValue(/test surname/i);
	expect(name).toBeInTheDocument();
});

test("renders profile editor description", () => {
	var profile = new UserProfile();
	profile.description = "description";
	var setProfile = (x: UserProfile) => {};
	var handeSave = () => {};
	render(
		<ProfileEditInner
			profile={profile}
			handleSave={handeSave}
			setProfile={setProfile}
		/>
	);
	const name = screen.queryByDisplayValue(/description/i);
	expect(name).toBeInTheDocument();
});
