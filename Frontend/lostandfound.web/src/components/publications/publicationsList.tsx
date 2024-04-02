import {
	CategoryType,
	editPublicationRating,
	getCategories,
	getPublicationsUndef,
	Order,
	PublicationResponseType,
	PublicationSearchRequestType,
	PublicationSortType,
	PublicationType,
	SinglePublicationVote,
	UserType,
} from "commons";
import { useContext, useEffect, useState } from "react";
import { userContext } from "userContext";
import { NewPublication } from "./newPublication";
import Pagination from "../pagination";
import PublicationModal from "./publicationDetails";
import { Link } from "react-router-dom";
import { FiChevronDown, FiChevronUp, FiEdit } from "react-icons/fi";
import {
	AiFillDelete,
	AiFillDislike,
	AiFillLike,
	AiOutlineDislike,
	AiOutlineLike,
} from "react-icons/ai";

export default function PublicationsList() {
	const usrCtx = useContext(userContext);
	const [pub, setPub] = useState([] as Publication[]);

	const [page, setPage] = useState(1 as number);
	const [maxpg, setMaxpg] = useState(1 as number);
	const [filter, setFilter] = useState({} as PublicationSearchRequestType);
	const [sort, setSort] = useState({
		order: Order.Descending,
	} as PublicationSortType);

	const [ldg, setLdg] = useState(true);
	const [los, setLos] = useState(undefined as boolean | undefined);

	const [details, setDetails] = useState(undefined as string | undefined);

	useEffect(() => {
		setLdg(true);
	}, [filter, sort, los]);

	useEffect(() => {
		if (maxpg < page) {
			setPage(1);
			setLdg(true);
		}
	}, [maxpg]);

	useEffect(() => {
		if (ldg) {
			setLdg(false);
			let srt = sort?.type
				? {
						firstArgumentSort: sort,
						secondArgumentSort: undefined,
				  }
				: undefined;
			getPublicationsUndef(
				page,
				usrCtx.user.authToken ?? "",
				{
					...filter,
					publicationType:
						los === true
							? PublicationType.FoundSubject
							: los === false
							? PublicationType.LostSubject
							: undefined,
				},
				srt
			).then((x) => {
				if (x === undefined)
					usrCtx.setUser({ ...usrCtx.user, isLogged: false });
				else {
					setPub(x.publications.map((y) => new Publication(y)));
					setMaxpg(x.pagination?.TotalPageCount ?? 100);
				}
			});
		}
	}, [page, usrCtx.user, usrCtx, ldg]);

	const [cats, setCats] = useState([] as (CategoryType | undefined)[]);

	useEffect(() => {
		if (usrCtx.user.authToken !== null)
			getCategories(usrCtx.user.authToken).then((x) => {
				setCats(x);
			});
	}, [usrCtx]);

	function like(pubId: string) {
		setPubVote(pubId, 1);
	}
	function dislike(pubId: string) {
		setPubVote(pubId, -1);
	}
	function setPubVote(pubId: string, newVote: number) {
		let pubs = [...pub];
		let p = pubs.find((x) => x.publicationIdentifier == pubId);
		if (p == undefined) return;
		if (p.userVote === newVote) {
			p.rating -= p.userVote;
			p.userVote = 0;
		} else {
			p.rating += newVote - p.userVote;
			p.userVote = newVote;
		}
		setPub(pubs);
		return editPublicationRating(
			pubId,
			p.userVote === 1
				? SinglePublicationVote.Up
				: p.userVote === 0
				? SinglePublicationVote.NoVote
				: SinglePublicationVote.Down,
			usrCtx.user.authToken ?? ""
		);
	}
	return (
		<div className="">
			{
				<PublicationModal
					pubId={details}
					onClose={() => setDetails(undefined)}
				/>
			}
			<NewPublication refresh={() => setLdg(true)}></NewPublication>

			<div className="row justify-content-start align-items-start">
				<div className="col-lg-3 col-12 p-5">
					<FiltersForm
						filters={filter}
						setFilter={(x) => {
							setFilter(x);
						}}
					></FiltersForm>
				</div>
				<div className="row col-md-6 col-12">
					<SortForm setSort={(x) => setSort(x)} sort={sort} />
					<div className="btn-group w-75 ms-auto me-auto mt-2 ">
						<div
							className={
								"btn  " +
								(los === true
									? "btn-primary"
									: "btn-outline-primary")
							}
							onClick={() => {
								if (los === true) setLos(undefined);
								else setLos(true);
							}}
						>
							Znalezione
						</div>
						<div
							className={
								"btn  " +
								(los === false
									? "btn-primary"
									: "btn-outline-primary")
							}
							onClick={() => {
								if (los === false) setLos(undefined);
								else setLos(false);
							}}
						>
							Zgubione
						</div>
					</div>
					{ldg && (
						<div>
							<div
								className=" spinner-border col-12 ms-auto me-auto"
								role="status"
							></div>
						</div>
					)}
					{!ldg &&
						pub.map((x, i) => (
							<PublicationCom
								pub={x}
								key={i}
								like={() => like(x.publicationIdentifier)}
								dislike={() => dislike(x.publicationIdentifier)}
								select={(x: string) => setDetails(x)}
								cats={cats}
							/>
						))}
				</div>
			</div>
			<Pagination
				page={page}
				setPage={(p: number) => {
					setPage(p);
					setLdg(true);
				}}
				maxPages={maxpg}
			/>
		</div>
	);
}

export function PublicationCom({
	pub,
	like,
	dislike,
	select,
	edit,
	del,
	cats,
}: {
	pub: Publication;
	like?: () => Promise<any> | void;
	dislike?: () => Promise<any> | void;
	select?: (pubId: string) => void;
	edit?: boolean;
	del?: () => void;
	cats?: (CategoryType | undefined)[];
}) {
	return (
		<div className="border border-dark bg-light shadow-lg my-3 p-3 pe-0 rounded-4 container row">
			{pub.subjectPicture && (
				<div className="col-4 align-self-center">
					<img
						className="w-100"
						style={{ maxHeight: "400px" }}
						src={pub.subjectPicture}
						data-bs-toggle="modal"
						data-bs-target="#staticBackdrop"
						onClick={() => {
							if (select) select(pub.publicationIdentifier);
						}}
					></img>
				</div>
			)}
			<div
				className="col text-start p-2 ps-3"
				data-bs-toggle="modal"
				data-bs-target="#staticBackdrop"
				onClick={() => {
					if (select) select(pub.publicationIdentifier);
				}}
			>
				<h4>{pub.title} </h4>
				<div className="fst-italic">
					{pub.incidentDate.toLocaleDateString()}
				</div>
				<div className="fst-italic">{pub.incidentAddress}</div>
				<div className=" fst-italic">
					{pub.lostorfnd === true ? "Zgubione" : "Znalezione"}
				</div>
				{cats && (
					<div className=" fst-italic">
						{cats.find((x) => x?.id === pub.cat)?.displayName}
					</div>
				)}
				<div className="p-2"> {pub.description}</div>
			</div>

			<div className="col-1 text-center align-self-center ms-auto">
				{like !== undefined && pub.userVote === 1 && (
					<AiFillLike
						size={20}
						onClick={() => like()}
						style={{ cursor: "pointer" }}
					/>
				)}
				{like !== undefined && pub.userVote !== 1 && (
					<AiOutlineLike
						size={20}
						onClick={() => like()}
						style={{ cursor: "pointer" }}
					/>
				)}
				<div
					className={
						"text-center h3 m-1 " +
						(pub.rating > 0 ? "text-success" : "") +
						(pub.rating < 0 ? "text-danger" : "")
					}
				>
					{pub.rating}
				</div>
				{dislike !== undefined && pub.userVote === -1 && (
					<AiFillDislike
						size={20}
						onClick={() => dislike()}
						style={{ cursor: "pointer" }}
					/>
				)}
				{dislike !== undefined && pub.userVote !== -1 && (
					<AiOutlineDislike
						size={20}
						onClick={() => dislike()}
						style={{ cursor: "pointer" }}
					/>
				)}
			</div>
			<div className="row m-auto align-self-end mt-2">
				<div className="col"></div>
				{edit && (
					<Link
						className="btn btn-warning text-black col-2 me-auto ms-1"
						to={`/posts/edit/${pub.publicationIdentifier}`}
					>
						<FiEdit size="20" className="mb-1 mr-1" /> Edytuj
					</Link>
				)}
				{del && (
					<button
						className="btn btn-danger col-2 ms-1"
						onClick={() => del()}
					>
						<AiFillDelete size="20" className="mb-1 mr-1" /> Usuń
					</button>
				)}
			</div>
		</div>
	);
}

function SortForm({
	sort,
	setSort,
}: {
	sort: PublicationSortType;
	setSort: (newSort: PublicationSortType) => void;
}) {
	return (
		<div className="ms-auto w-50 d-flex align-items-center mt-2">
			Sortuj:{" "}
			<select
				value={sort.type}
				className="form-select w-50 mx-2"
				onChange={(e) => {
					setSort({ order: sort.order, type: e.target.value });
				}}
			>
				<option selected label="Brak" value={undefined} />
				<option label="Tytuł" value={"Title"} />
				<option label="Kategoria" value={"SubjectCategoryId"} />
				<option label="Data zdarzenia" value={"IncidentDate"} />
				<option label="Średnia ocena" value={"AggregateRating"} />
				<option label="Stan ogłoszenia" value={"PublicationState"} />
				<option label="Typ ogłoszenia" value={"PublicationType"} />
			</select>
			<button
				className="btn btn-primary rounded-5"
				onClick={() => {
					setSort({
						...sort,
						order:
							sort.order === Order.Ascending
								? Order.Descending
								: Order.Ascending,
					});
				}}
			>
				{" "}
				{sort.order === Order.Ascending ? (
					<span>
						Rosnąco <FiChevronUp />
					</span>
				) : (
					<span>
						Malejąco <FiChevronDown />
					</span>
				)}
			</button>
		</div>
	);
}

function FiltersForm({
	filters,
	setFilter,
}: {
	filters: PublicationSearchRequestType;
	setFilter: (arg: PublicationSearchRequestType) => void;
}) {
	const usrCtx = useContext(userContext);
	const [cats, setCats] = useState([] as (CategoryType | undefined)[]);
	const [filt, setFiltLoc] = useState(
		filters as PublicationSearchRequestType
	);
	useEffect(() => {
		if (usrCtx.user.authToken !== null)
			getCategories(usrCtx.user.authToken).then((x) => {
				setCats(x);
			});
	}, [usrCtx]);

	const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
		setFiltLoc({
			...filt,
			[event.target.name]: event.target.value,
		});
	};
	const handleChangeDate = (event: React.ChangeEvent<HTMLInputElement>) => {
		setFiltLoc({
			...filt,
			[event.target.name]: event.target.valueAsDate,
		});
	};

	function filter() {
		setFilter(filt);
	}

	return (
		<div className="border border-dark rounded-5 ps-3 pe-3 p-1 bg-light">
			<div className="text-start pt-2 h5">Filtrowanie:</div>
			<div className="pt-2">
				<div className="form-label text-start">Tekst:</div>
				<input
					value={filt.title || ""}
					type="text"
					className="form-control"
					name="title"
					onChange={(e) => handleChange(e)}
				></input>
			</div>
			<div className="pt-2">
				<div className="form-label text-start">Lokalizacja:</div>
				<input
					type="text"
					className="form-control"
					name="incidentAddress"
					value={filt.incidentAddress || ""}
					onChange={(e) => handleChange(e)}
				></input>
			</div>
			<div className="pt-2">
				<div className="form-label text-start">Dystans:</div>
				<select
					value={filt.incidentDistance || 1}
					defaultValue={1}
					className="form-select w-100"
					onChange={(e) =>
						setFiltLoc({
							...filt,
							incidentDistance: Number(e.target.value),
						})
					}
				>
					<option value={1}>1km</option>
					<option value={2}>2km</option>
					<option value={3}>3km</option>
					<option value={5}>5km</option>
					<option value={50}>50km</option>
				</select>
			</div>
			<div className="form-label text-start pt-2">Zakres dat:</div>
			<div className="pb-1 w-100 ">
				<span className="form-label  me-3 ">Od:</span>
				<input
					value={
						filt.incidentFromDate?.toISOString().substring(0, 10) ||
						""
					}
					className="w-75 form-control d-inline"
					name="incidentFromDate"
					type="date"
					placeholder="Data"
					onChange={(e) => handleChangeDate(e)}
				/>
			</div>
			<div className="pb-1 w-100 ">
				<span className="form-label  me-3 ">Do:</span>
				<input
					value={
						filt.incidentToDate?.toISOString().substring(0, 10) ||
						""
					}
					className="w-75 form-control d-inline"
					name="incidentToDate"
					type="date"
					placeholder="Data"
					onChange={(e) => handleChangeDate(e)}
				/>
			</div>
			<div className="pt-2">
				<div className="form-label text-start">Kategoria:</div>
				<select
					value={filt.subjectCategoryId ?? ""}
					className="form-select w-100"
					onChange={(e) =>
						setFiltLoc({
							...filt,
							subjectCategoryId: e.target.value,
						})
					}
				>
					<option value={""}></option>
					{cats.map((x, i) => (
						<option key={i} value={x?.id}>
							{x?.displayName}
						</option>
					))}
				</select>
			</div>
			<div className="w-100 m-2 d-flex justify-content-evenly">
				<button
					className="btn btn-primary rounded-5"
					onClick={() => filter()}
				>
					Filtruj
				</button>
				<button
					className=" btn btn-danger rounded-5 "
					onClick={() => {
						setFiltLoc({} as PublicationSearchRequestType);
						setFilter({});
					}}
				>
					Wyczyść
				</button>
			</div>
		</div>
	);
}

export class Publication {
	constructor(pub?: PublicationResponseType) {
		this.publicationIdentifier = pub?.publicationId ?? "";
		this.cat = pub?.subjectCategoryId;
		this.description = pub?.description ?? "";
		this.title = pub?.title ?? "";
		this.incidentAddress = pub?.incidentAddress ?? "";
		this.rating = pub?.aggregateRating ?? 0;
		this.incidentDate = pub?.incidentDate ?? new Date();
		this.cat = pub?.subjectCategoryId;
		this.userVote =
			pub?.userVote === SinglePublicationVote.Up
				? 1
				: pub?.userVote === SinglePublicationVote.Down
				? -1
				: 0;
		this.user = pub?.author;
		this.subjectPicture = pub?.subjectPhotoUrl;
		this.lostorfnd =
			pub?.publicationType === PublicationType.LostSubject
				? true
				: pub?.publicationType === PublicationType.FoundSubject
				? false
				: undefined;
	}
	publicationIdentifier: string;
	title: string = "";
	description: string = "";
	subjectPicture: string | undefined = undefined;
	incidentAddress: string = "";
	incidentDate: Date = new Date();
	rating: number = 0;
	userVote: number = 0;
	lostorfnd: boolean | undefined;
	cat: string | undefined;
	user: UserType | undefined;
}
