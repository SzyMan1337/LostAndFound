import {
	CategoryType,
	getCategories,
	getPublication,
	PublicationResponseType,
	PublicationType,
} from "commons";
import { useContext, useEffect, useState } from "react";
import { NavigateFunction, useNavigate } from "react-router-dom";
import { userContext } from "userContext";

export default function PublicationModal({
	pubId,
	onClose,
}: {
	pubId?: string;
	onClose: () => void;
}) {
	const navigate = useNavigate();
	const [cat, setCat] = useState([] as CategoryType[]);
	const [pubDet, setPubDet] = useState(
		undefined as PublicationResponseType | undefined
	);
	const usrCtx = useContext(userContext);
	useEffect(() => {
		getCategories(usrCtx.user.authToken ?? "").then((x) => setCat(x));
		if (pubId !== undefined)
			getPublication(pubId, usrCtx.user.authToken ?? "").then((x) =>
				setPubDet(x)
			);
	}, [pubId]);
	return (
		<>
			<div
				className="modal fade"
				id="staticBackdrop"
				data-bs-backdrop="static"
				data-bs-keyboard="false"
				tabIndex={-1}
				aria-labelledby="staticBackdropLabel"
				aria-hidden="true"
			>
				<div
					className="modal-dialog"
					style={{ maxWidth: "100%", width: "40%" }}
				>
					<div className="modal-content bg-light rounded-5 border-3 border-dark shadow-lg w-100">
						<div className="modal-header border-bottom border-dark border-1">
							<h1
								className="modal-title fs-5"
								id="staticBackdropLabel"
							>
								{pubDet?.title}
							</h1>
							<button
								type="button"
								className="btn-close"
								data-bs-dismiss="modal"
								aria-label="Close"
								onClick={() => onClose()}
							></button>
						</div>
						<div className="modal-body">
							<Body cat={cat} pubDet={pubDet} nav={navigate} />
						</div>
					</div>
				</div>
			</div>
		</>
	);
}
function Body({
	pubDet,
	nav,
	cat,
}: {
	pubDet?: PublicationResponseType;
	nav?: NavigateFunction;
	cat: CategoryType[];
}) {
	return (
		<>
			{pubDet?.subjectPhotoUrl && (
				<img
					className="img-fluid"
					style={{ width: "400px" }}
					src={
						pubDet?.subjectPhotoUrl ??
						"https://xsgames.co/randomusers/avatar.php?g=pixel"
					}
				></img>
			)}
			<div className="container row m-1 mt-3">
				<div className="col-3 text-start">
					<div className="mb-1">
						<strong>Autor: </strong>
						<a
							className="link-dark pe-auto"
							onClick={() => {
								document
									?.getElementById("myModal")
									?.classList.remove("show", "d-block");
								document
									?.querySelectorAll(".modal-backdrop")
									?.forEach((el) =>
										el.classList.remove("modal-backdrop")
									);
								if (nav)
									nav(`/profile/${pubDet?.author.id}`, {
										replace: false,
									});
							}}
						>
							{pubDet?.author.username}
						</a>
					</div>
					<div className="mb-1">
						<strong>Adres: </strong>
						<span className="fst-italic">
							{pubDet?.incidentAddress}
						</span>
					</div>
					<div className="mb-1">
						<strong>Typ: </strong>
						<span className="fst-italic">
							{pubDet?.publicationType ==
							PublicationType.FoundSubject
								? "Znalezione"
								: "Zgubione"}
						</span>
					</div>
					<div className="mb-1">
						<strong>Kategoria: </strong>
						<span className="fst-italic">
							{
								cat.find(
									(x) => x.id == pubDet?.subjectCategoryId
								)?.displayName
							}
						</span>
					</div>
					<div className="mb-1">
						<strong>Data: </strong>
						<span className="fst-italic">
							{pubDet?.incidentDate.toLocaleDateString()}
						</span>
					</div>
				</div>
				<div className="col-8 text-start">
					<strong className="d-block">Opis:</strong>
					<div>{pubDet?.description}</div>
				</div>
				<Rating pubDet={pubDet} />
			</div>
		</>
	);
}

function Rating({ pubDet }: { pubDet?: PublicationResponseType }) {
	if (pubDet)
		return (
			<div className="col-1 text-center  p-0 float-end">
				<div
					className={
						"text-center h3 m-1 " +
						((pubDet?.aggregateRating ?? 0) > 0
							? "text-success"
							: "") +
						((pubDet?.aggregateRating ?? 0) < 0
							? "text-danger"
							: "")
					}
				>
					{pubDet?.aggregateRating ?? 0}
				</div>
			</div>
		);
	else return <></>;
}
