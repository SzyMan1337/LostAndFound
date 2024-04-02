import {
	addPublication,
	CategoryType,
	getCategories,
	PublicationState,
	PublicationType,
} from "commons";
import { useContext, useEffect, useState } from "react";
import { userContext } from "userContext";
import { Publication } from "./publicationsList";

export function NewPublication({ refresh }: { refresh?: () => void }) {
	const [exp, setExp] = useState(false);
	if (exp)
		return (
			<div className="col-6 m-auto">
				<button
					className="btn btn-primary rounded-5 px-5"
					onClick={() => setExp(!exp)}
				>
					-
				</button>

				<NewPublicationInner
					refresh={() => {
						setExp(false);
						if (refresh) refresh();
					}}
				></NewPublicationInner>
			</div>
		);
	return (
		<button
			className="btn btn-primary rounded-5 px-5"
			onClick={() => setExp(!exp)}
		>
			+
		</button>
	);
}
export function NewPublicationInner({ refresh }: { refresh?: () => void }) {
	const usrCtx = useContext(userContext);
	const [pub, setPub] = useState(new Publication());
	const [cats, setCats] = useState([] as CategoryType[]);
	const [selectedImage, setSelectedImage] = useState(null as File | null);
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

	const handleChangeArea = (
		event: React.ChangeEvent<HTMLTextAreaElement>
	) => {
		setPub({
			...pub,
			[event.target.name]: event.target.value,
		});
	};

	function onValidate() {
		console.log(pub.incidentDate);
		var nerr = [] as valErr[];
		if (pub.lostorfnd === undefined) nerr.push(valErr.type);
		if (!pub.title) nerr.push(valErr.title);
		if (!pub.cat) nerr.push(valErr.cat);
		if (!pub.description) nerr.push(valErr.desc);

		if (!pub.incidentAddress) nerr.push(valErr.place);
		if (!pub.incidentDate) nerr.push(valErr.date);
		setVal(nerr);
		if (nerr.length > 0) return false;
		return true;
	}

	function add() {
		if (onValidate())
			addPublication(
				{
					incidentDate: new Date(pub.incidentDate),
					publicationType: pub.lostorfnd
						? PublicationType.FoundSubject
						: PublicationType.LostSubject,
					description: pub.description,
					incidentAddress: pub.incidentAddress,
					publicationState: PublicationState.Open,
					title: pub.title,
					subjectCategoryId: pub.cat,
				},
				usrCtx.user.authToken ?? "",
				undefined,
				selectedImage ?? undefined
			).then((x) => {
				if (refresh && x) refresh();
			});
	}

	return (
		<div className="mt-4 p-3 border border-dark rounded-4 bg-light text-start">
			<div className="text-left p-2 h5">Tworzenie nowego ogłoszenia:</div>

			<div className="text-end">
				<div className="p-1 w-100 pb-2">
					<span className="form-label  me-3 ">Zdjęcie:</span>
					<input
						className="w-75 form-control d-inline"
						type="file"
						name="test"
						onChange={(e) => {
							if (e?.target?.files) {
								setSelectedImage(e.target.files[0]);
							}
						}}
					/>
					{selectedImage && (
						<div className="w-50">
							<img
								className=" ms-auto img-fluid rounded-5 d-block mt-2"
								style={{ width: "200px" }}
								src={URL.createObjectURL(selectedImage)}
							></img>
						</div>
					)}
				</div>
				<span className="form-label  me-3 ">Typ ogłoszenia:</span>
				<div className="btn-group w-75 p-1">
					<button
						className={
							"btn text-dark " +
							(pub.lostorfnd === true
								? "btn-primary"
								: "btn-outline-primary ")
						}
						onClick={() => setPub({ ...pub, lostorfnd: true })}
					>
						Znalezione
					</button>
					<button
						className={
							"btn text-dark " +
							(pub.lostorfnd === false
								? "btn-primary"
								: "btn-outline-primary ")
						}
						onClick={() => setPub({ ...pub, lostorfnd: false })}
					>
						Zgubione
					</button>
				</div>
				{val.includes(valErr.type) && (
					<div className="w-75 ms-auto text-start text-danger">
						typ musi być wybrany
					</div>
				)}
				<div className="p-1 w-100 ">
					<span className="form-label  me-3 ">Tytuł:</span>
					<input
						className="w-75 form-control d-inline"
						name="title"
						type="text"
						placeholder="Tytuł"
						onChange={(e) => handleChange(e)}
					/>
				</div>
				{val.includes(valErr.title) && (
					<div className="w-75 ms-auto text-start text-danger">
						tytuł nie może być pusty
					</div>
				)}
				<div className="p-1 w-100 ">
					<span className="form-label  me-3 align-top">Opis:</span>
					<textarea
						rows={5}
						className="w-75 form-control d-inline"
						name="description"
						placeholder="Opis"
						onChange={(e) => handleChangeArea(e)}
					></textarea>
				</div>
				{val.includes(valErr.desc) && (
					<div className="w-75 ms-auto text-start text-danger">
						opis nie może być pusty
					</div>
				)}
				<div className="p-1 w-100 ">
					<span className="form-label  me-3 ">Miejsce:</span>
					<input
						className="form-control w-75 d-inline"
						name="incidentAddress"
						type="text"
						placeholder="Miejsce"
						onChange={(e) => handleChange(e)}
					/>
				</div>
				{val.includes(valErr.place) && (
					<div className="w-75 ms-auto text-start text-danger">
						miejsce nie może być puste
					</div>
				)}
				<div className="p-1 w-100 ">
					<span className="form-label  me-3 ">Data:</span>
					<input
						className="w-75 form-control d-inline"
						name="incidentDate"
						type="date"
						placeholder="Data"
						onChange={(e) => {
							handleChange(e);
						}}
					/>
				</div>

				<div className="p-1 w-100 ">
					<span className="form-label  me-3 ">Kategoria:</span>
					<select
						className="form-select w-75 d-inline"
						onChange={(e) => {
							setPub({ ...pub, cat: e.target.value });
						}}
					>
						<option selected value="none"></option>
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
				<button
					className="btn btn-primary ms-auto mt-3"
					onClick={() => add()}
				>
					Utwórz
				</button>
			</div>
		</div>
	);
}

enum valErr {
	type,
	title,
	place,
	date,
	cat,
	desc,
}
