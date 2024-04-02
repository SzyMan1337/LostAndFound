import {
	CategoryType,
	deletePublicationPhoto,
	editPublication,
	editPublicationPhotoWeb,
	getCategories,
	getPublication,
	PublicationResponseType,
	PublicationState,
	PublicationType,
} from "commons";
import UploadAndDisplayImage from "components/imagePicker";
import { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useParams } from "react-router-dom";
import { userContext } from "userContext";

export function EditPublication() {
	const usrCtx = useContext(userContext);
	const nav = useNavigate();
	const { pubId } = useParams();
	const [pub, setPub] = useState(
		undefined as PublicationResponseType | undefined
	);
	const [photo, setPhoto] = useState(null as File | null | undefined);
	function save() {
		if (pub)
			editPublication(
				pub?.publicationId ?? "",
				pub,
				usrCtx.user.authToken ?? ""
			).then((x) => {
				if (photo && photo !== null) {
					editPublicationPhotoWeb(
						pub?.publicationId,
						photo,
						usrCtx.user.authToken ?? ""
					).then((y) => {
						if (x) nav("/posts/mine");
					});
				} else if (photo === undefined) {
					deletePublicationPhoto(
						pub?.publicationId,
						usrCtx.user.authToken ?? ""
					).then((y) => {
						if (x) nav("/posts/mine");
					});
				} else {
					nav("/posts/mine");
				}
			});
	}
	useEffect(() => {
		if (pubId)
			getPublication(pubId, usrCtx.user.authToken ?? "").then((x) =>
				setPub(x)
			);
	}, [pubId]);

	if (!pub) return <div>...</div>;
	return (
		<EditPublicationInner
			pub={pub}
			setPub={(pub) => setPub(pub)}
			save={() => save()}
			saveImg={(file: File) => setPhoto(file)}
			delImg={() => {
				setPub({ ...pub, subjectPhotoUrl: undefined });
				setPhoto(undefined);
			}}
		/>
	);
}
export function EditPublicationInner({
	pub,
	setPub,
	save,
	saveImg,
	delImg,
}: {
	pub: PublicationResponseType;
	setPub: (newPub: PublicationResponseType) => void;
	save: () => void;
	saveImg: (file: File) => void;
	delImg: () => void;
}) {
	enum valErr {
		type,
		title,
		place,
		date,
		cat,
		desc,
	}

	const usrCtx = useContext(userContext);
	const [cats, setCats] = useState([] as CategoryType[]);
	const nav = useNavigate();
	const [val, setVal] = useState([] as valErr[]);

	useEffect(() => {
		if (usrCtx.user.authToken !== null)
			getCategories(usrCtx.user.authToken).then((x) => {
				setCats(x);
			});
	}, [usrCtx]);

	const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
		setPub({
			...pub,
			[event.target.name]: event.target.value,
		});
	};
	const handleChangeDate = (event: React.ChangeEvent<HTMLInputElement>) => {
		setPub({
			...pub,
			[event.target.name]: event.target.valueAsDate,
		});
	};
	const handleChangeArea = (
		event: React.ChangeEvent<HTMLTextAreaElement>
	) => {
		setPub({
			...pub,
			[event.target.name]: event.target.value,
		});
	};

	function onValidate() {
		var nerr = [] as valErr[];
		if (!pub.title || pub.title.length === 0) nerr.push(valErr.title);
		if (!pub.incidentAddress) nerr.push(valErr.place);
		if (!pub.incidentDate) nerr.push(valErr.date);
		if (!pub.description) nerr.push(valErr.desc);

		if (!pub.subjectCategoryId || pub.subjectCategoryId === "none")
			nerr.push(valErr.cat);
		setVal(nerr);
		if (nerr.length > 0) return false;
		return true;
	}

	return (
		<div className="mt-4 p-3 w-50 m-auto border border-dark rounded-4 bg-light text-end">
			<div className="text-left p-2 h5 text-start">
				Edycja ogłoszenia: <span>{pub.title}</span>
			</div>

			{saveImg && delImg && (
				<UploadAndDisplayImage
					currentImg={pub.subjectPhotoUrl}
					onSave={(x) => {
						return saveImg(x);
					}}
					onDelete={() => delImg()}
				/>
			)}
			<div className="text-end">
				<div className="p-1 w-100 ">
					<span className="form-label  me-3 ">Typ ogłoszenia:</span>
					<div className="btn-group w-75">
						<button
							className={
								"btn text-dark col-1 " +
								(pub.publicationType ===
								PublicationType.FoundSubject
									? "btn-primary"
									: "btn-outline-primary ")
							}
							onClick={() =>
								setPub({
									...pub,
									publicationType:
										PublicationType.FoundSubject,
								})
							}
						>
							Znalezione
						</button>
						<button
							className={
								"btn text-dark col-1 " +
								(pub.publicationType ===
								PublicationType.LostSubject
									? "btn-primary"
									: "btn-outline-primary ")
							}
							onClick={() =>
								setPub({
									...pub,
									publicationType:
										PublicationType.LostSubject,
								})
							}
						>
							Zgubione
						</button>
					</div>
				</div>

				<div className="p-1 w-100">
					<span className="form-label  me-3 ">Stan:</span>
					<div className="btn-group w-75">
						<button
							className={
								"btn text-dark col-1 " +
								(pub.publicationState === PublicationState.Open
									? "btn-primary"
									: "btn-outline-primary ")
							}
							onClick={() =>
								setPub({
									...pub,
									publicationState: PublicationState.Open,
								})
							}
						>
							Otwarte
						</button>
						<button
							className={
								"btn text-dark col-1 " +
								(pub.publicationState ===
								PublicationState.Closed
									? "btn-danger"
									: "btn-outline-danger ")
							}
							onClick={() =>
								setPub({
									...pub,
									publicationState: PublicationState.Closed,
								})
							}
						>
							Zamknięte
						</button>
					</div>
				</div>
				<div className="p-1 w-100 ">
					<span className="form-label  me-3 ">Tytuł:</span>
					<input
						className="w-75 form-control d-inline"
						name="title"
						type="text"
						value={pub.title}
						placeholder="Tytuł"
						onChange={(e) => handleChange(e)}
					/>
					{val.includes(valErr.title) && (
						<div className="w-75 ms-auto text-start text-danger">
							tytuł nie może być pusty
						</div>
					)}
				</div>
				<div className="p-1 w-100 ">
					<span className="form-label  me-3 align-top">Opis:</span>
					<textarea
						rows={5}
						className="w-75 form-control d-inline"
						name="description"
						value={pub.description}
						placeholder="Opis"
						onChange={(e) => handleChangeArea(e)}
					></textarea>
					{val.includes(valErr.desc) && (
						<div className="w-75 ms-auto text-start text-danger">
							opis nie może być pusty
						</div>
					)}
				</div>
				<div className="p-1 w-100 ">
					<span className="form-label  me-3 ">Miejsce:</span>
					<input
						className="form-control w-75 d-inline"
						name="incidentAddress"
						value={pub.incidentAddress}
						type="text"
						placeholder="Miejsce"
						onChange={(e) => handleChange(e)}
					/>
					{val.includes(valErr.place) && (
						<div className="w-75 ms-auto text-start text-danger">
							miejsce nie może być puste
						</div>
					)}
				</div>
				<div className="p-1 w-100 ">
					<span className="form-label  me-3 ">Data:</span>
					<input
						value={pub.incidentDate
							?.toISOString()
							?.substring(0, 10)}
						className="w-75 form-control d-inline"
						name="incidentDate"
						type="date"
						placeholder="Data"
						onChange={(e) => {
							handleChangeDate(e);
						}}
					/>
					{val.includes(valErr.date) && (
						<div className="w-75 ms-auto text-start text-danger">
							data nie może być pusta
						</div>
					)}
				</div>
				<div className="p-1 w-100 ">
					<span className="form-label  me-3 ">Kategoria:</span>
					<select
						defaultValue="none"
						value={pub.subjectCategoryId}
						className="form-select w-75 d-inline"
						onChange={(e) => {
							setPub({
								...pub,
								subjectCategoryId: e.target.value,
							});
						}}
					>
						<option value="none"></option>
						{cats.map((x, i) => (
							<option key={i} value={x.id}>
								{x.displayName}
							</option>
						))}
					</select>
					{val.includes(valErr.cat) && (
						<div className="w-75 ms-auto text-start text-danger">
							kategoria nie może być pusta
						</div>
					)}
				</div>

				<div className="row align-self-center d-flex p-3">
					<div className="col"></div>
					<button
						className="btn btn-primary mt-3 col-3 ms-1"
						onClick={() => {
							if (onValidate()) save();
						}}
					>
						Zapisz
					</button>
					<button
						className="btn btn-danger mt-3 col-3 ms-1"
						onClick={() => nav("/posts/mine")}
					>
						Anuluj
					</button>
				</div>
			</div>
		</div>
	);
}
