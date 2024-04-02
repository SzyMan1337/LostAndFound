import { getProfile, getProfileDetails } from "commons";
import { useContext, useEffect, useState } from "react";
import { Link, Navigate, useParams } from "react-router-dom";
import { userContext } from "userContext";
import ProfileComments from "./profileComments";
import { FiEdit, FiStar } from "react-icons/fi";
import { useNavigate } from "react-router-dom";

export default function Profile() {
	const usrCtx = useContext(userContext);
	const [prof, setProf] = useState(undefined as UserProfile | undefined);
	useEffect(() => {
		if (usrCtx.user.authToken !== null)
			getProfile(usrCtx.user.authToken).then((x) => {
				if (x !== undefined) {
					setProf({
						uId: x.userId,
						username: x.username,
						name: x.name,
						surname: x.surname,
						email: x.email,
						description: x.description,
						city: x.city,
						averageProfileRating: x.averageProfileRating,
						pictureUrl:
							x.pictureUrl ??
							"https://avatars.dicebear.com/api/bottts/stefan.svg",
						me: true,
					});
				} else {
					usrCtx.setUser({ ...usrCtx.user, isLogged: false });
				}
			});
	}, []);
	if (prof === undefined)
		return (
			<div>
				<div
					className=" spinner-border col-12 ms-auto me-auto"
					role="status"
				></div>
			</div>
		);
	return <ProfileInner profile={prof} refresh={() => {}}></ProfileInner>;
}

export function ProfileOther() {
	let nav = useNavigate();
	let { userId } = useParams();
	const usrCtx = useContext(userContext);
	const [prof, setProf] = useState(undefined as UserProfile | undefined);
	const [ldg, setLdg] = useState(false);
	const [me, setMe] = useState("");
	useEffect(() => {
		setLdg(true);
	}, [userId]);

	useEffect(() => {
		if (ldg === true) {
			setLdg(false);
			if (usrCtx.user.authToken !== null && userId !== undefined) {
				getProfile(usrCtx.user.authToken ?? "")
					.then((x) => {
						if (x !== undefined) {
							setMe(x.userId);
						}
					})
					.then((y) => {
						if (userId && usrCtx.user.authToken !== null)
							getProfileDetails(
								userId,
								usrCtx.user.authToken
							).then((x) => {
								if (x !== undefined) {
									setLdg(false);

									if (me == x.userId) nav("/profile");
									setProf({
										uId: x.userId,
										username: x.username,
										name: x.name,
										surname: x.surname,
										email: x.email,
										description: x.description,
										city: x.city,
										averageProfileRating:
											x.averageProfileRating,
										pictureUrl:
											x.pictureUrl ??
											"https://avatars.dicebear.com/api/bottts/stefan.svg",
										me: false,
									});
								} else {
									nav("/");
								}
							});
					});
			}
		}
	}, [ldg]);

	if (ldg === true)
		return (
			<div>
				<div
					className=" spinner-border col-12 ms-auto me-auto"
					role="status"
				></div>
			</div>
		);
	if (prof === undefined) return <div>...</div>;

	return (
		<ProfileInner
			profile={prof}
			refresh={() => setLdg(true)}
			nav={() => nav(`/chats/${prof.uId}`)}
		></ProfileInner>
	);
}

export function ProfileInner({
	profile,
	refresh,
	nav,
}: {
	profile: UserProfile;
	refresh?: () => void;
	nav?: () => void;
}) {
	let ref = refresh !== undefined ? refresh : () => {};
	return (
		<div
			data-testid="profileInner"
			className="container  border border-dark rounded-5 bg-light p-3 shadow-lg"
		>
			<div className="row">
				<div className="col-5 ">
					<img
						className="h-auto w-100 rounded-5"
						src={profile.pictureUrl}
						alt="profileImage"
					></img>
				</div>
				<div className="col text-start p-2">
					<div className="d-flex">
						<h1>{profile.username}</h1>
						{profile.me && (
							<>
								<Link
									className="text-dark btn float-end"
									to="/profile/edit"
								>
									<FiEdit size="38" />
								</Link>
								<Link
									className="me-auto text mt-auto mb-auto"
									to="/profile/password"
								>
									Zmień hasło
								</Link>
							</>
						)}
						{!profile.me && nav && (
							<div
								className="btn btn-primary my-auto ms-3 rounded-5"
								onClick={() => nav()}
							>
								Czat
							</div>
						)}
						<div className="h1 align-self-center ms-auto me-4 d-flex align-items-center">
							<span>
								{profile.averageProfileRating?.toFixed(2)}
							</span>
							<FiStar
								className="ms-2 mt-1"
								fill="#ffc107"
								color="#ffc107"
							/>
						</div>
					</div>
					{(profile.name || profile.surname) && (
						<div className="p-2">
							<strong className="row">Imie i nazwisko: </strong>
							{profile.name} {profile.surname}
						</div>
					)}
					{profile.city && (
						<div className="p-2">
							<strong className="row">Miasto: </strong>
							<span className="p-2 fst-italic">
								{profile.city}
							</span>
						</div>
					)}
					{profile.description && (
						<div className="p-2 pt-3 ">
							<strong className="row ">Opis: </strong>
							<div className="fst-italic">
								{profile.description}
							</div>
						</div>
					)}
				</div>
			</div>

			<ProfileComments
				me={profile.me}
				profId={profile.uId}
				refresh={() => ref()}
			></ProfileComments>
		</div>
	);
}
export class UserProfile {
	uId: string = "";
	email: string | undefined = "EMPTY";
	username: string | undefined = "EMPTY";
	name: string | undefined = "Adam";
	surname: string | undefined = "Toleracko";
	description: string | undefined =
		"Bardzo lubie pomagac znajdowac zagubione rzeczy.";
	city: string | undefined = "Warszawa";
	averageProfileRating: number | undefined = 4.78;
	pictureUrl: string | undefined =
		"https://avatars.dicebear.com/api/bottts/stefan.svg";
	me: boolean = true;
}
