import {
	deleteProfilePhoto,
	editProfile,
	editProfilePhoto,
	editProfilePhotoWeb,
	getProfile,
} from "commons";
import UploadAndDisplayImage from "components/imagePicker";
import { useContext, useEffect, useState } from "react";
import { Link, Navigate } from "react-router-dom";
import { fileURLToPath } from "url";
import { userContext } from "userContext";
import { UserProfile } from "./profile";

export default function ProfileEdit() {
	const usrCtx = useContext(userContext);

	const [prof, setProf] = useState(undefined as UserProfile | undefined);
	const [svd, setSvd] = useState(false);

	const [newPhoto, setNewPhoto] = useState(null as File | null | undefined);

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
						pictureUrl: x.pictureUrl,
						me: true,
					});
				} else {
					usrCtx.setUser({ ...usrCtx.user, isLogged: false });
				}
			});
	}, []);

	function handleSave() {
		if (usrCtx.user.authToken !== null)
			editProfile(
				{
					name: prof?.name,
					surname: prof?.surname,
					city: prof?.city,
					description: prof?.description,
				},
				usrCtx.user.authToken
			).then((x) => {
				if (x && usrCtx.user.authToken) {
					if (newPhoto && newPhoto !== null) {
						editProfilePhotoWeb(
							newPhoto,
							usrCtx.user.authToken
						).then((s) => setSvd(true));
					} else if (newPhoto === undefined) {
						deleteProfilePhoto(usrCtx.user.authToken).then((s) =>
							setSvd(true)
						);
					} else {
						setSvd(true);
					}
				}
			});
	}

	function saveImg(file: File) {
		setNewPhoto(file);
	}

	function delImg() {
		setNewPhoto(undefined);
		if (prof) setProf({ ...prof, pictureUrl: "" });
	}

	if (svd === true) return <Navigate to="/profile"></Navigate>;
	if (prof === undefined) return <div>...</div>;
	return (
		<>
			<ProfileEditInner
				profile={prof}
				setProfile={setProf}
				handleSave={() => handleSave()}
				saveImg={(x) => saveImg(x)}
				delImg={() => delImg()}
			/>
		</>
	);
}

export function ProfileEditInner({
	profile,
	setProfile,
	handleSave,
	saveImg,
	delImg,
}: {
	profile: UserProfile;
	setProfile: (x: UserProfile) => void;
	handleSave: () => void;
	saveImg?: (x: File) => void;
	delImg?: () => void;
}) {
	const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
		setProfile({
			...profile,
			[event.target.name]: event.target.value,
		});
	};
	const handleChangeArea = (
		event: React.ChangeEvent<HTMLTextAreaElement>
	) => {
		setProfile({
			...profile,
			[event.target.name]: event.target.value,
		});
	};

	return (
		<div
			data-testid="profileInner"
			className="w-50 border border-1 border-dark m-auto rounded-4 bg-light p-3 shadow-lg text-end container"
		>
			<div className="text-start h2">
				Edycja:{" "}
				<div className="d-inline ms-3 h4">{profile.username}</div>
			</div>
			{saveImg && delImg && (
				<UploadAndDisplayImage
					currentImg={profile.pictureUrl}
					onSave={(x) => {
						return saveImg(x);
					}}
					onDelete={() => delImg()}
				/>
			)}
			<div className="p-3  row align-items-end">
				<div className="form-label col-3">Imię:</div>
				<div className="col-7">
					<input
						className="form-control"
						name="name"
						type="text"
						placeholder="Imię"
						value={profile.name}
						onChange={(e) => handleChange(e)}
					/>
				</div>
			</div>
			<div className="p-3  row align-items-end">
				<span className="form-label col-3">Nazwisko:</span>
				<div className="col-7">
					<input
						className=" form-control col"
						name="surname"
						type="text"
						placeholder="Nazwisko"
						value={profile.surname}
						onChange={(e) => handleChange(e)}
					/>
				</div>
			</div>
			<div className="p-3  row align-items-end ">
				<span className="form-label col-3">Miasto:</span>
				<div className="col-7">
					<input
						className=" form-control col"
						name="city"
						type="text"
						placeholder="Miasto"
						value={profile.city}
						onChange={(e) => handleChange(e)}
					/>
				</div>
			</div>
			<div className="p-3  row ">
				<span className="form-label col-3 ">Opis:</span>
				<div className="col-7">
					<textarea
						rows={5}
						className="form-control col"
						name="description"
						value={profile.description}
						placeholder="Opis"
						onChange={(e) => handleChangeArea(e)}
					></textarea>
				</div>
			</div>

			<button
				className="btn btn-primary m-auto text-center"
				onClick={() => handleSave()}
			>
				Zapisz
			</button>
		</div>
	);
}
