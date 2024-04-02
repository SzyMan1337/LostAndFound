import {
	deletePublication,
	editPublicationRating,
	getPublicationsUndef,
	PublicationSearchRequestType,
	SinglePublicationVote,
} from "commons";
import Pagination from "components/pagination";
import { useContext, useEffect, useState } from "react";
import { userContext } from "userContext";
import { Publication, PublicationCom } from "./publicationsList";

export default function MyPublications() {
	const usrCtx = useContext(userContext);
	const [pub, setPub] = useState([] as Publication[]);

	const [page, setPage] = useState(1 as number);
	const [maxpg, setmaxpg] = useState(1 as number);
	const [filter, setFilter] = useState(
		undefined as PublicationSearchRequestType | undefined
	);
	const [ldg, setldg] = useState(true);

	useEffect(() => {
		if (ldg) {
			setldg(false);
			getPublicationsUndef(page, usrCtx.user.authToken ?? "", {
				onlyUserPublications: true,
			}).then((x) => {
				if (x === undefined)
					usrCtx.setUser({ ...usrCtx.user, isLogged: false });
				else {
					setPub(x.publications.map((y) => new Publication(y)));
					setmaxpg(x.pagination?.TotalPageCount ?? 100);
				}
			});
		}
	}, [page, usrCtx.user, usrCtx, filter, ldg]);

	function rem(pubId: string) {
		if (pubId)
			return deletePublication(pubId, usrCtx.user.authToken ?? "").then(
				(x) => setldg(true)
			);
		return Promise.resolve();
	}
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
		<>
			<div className="container">
				<div className=" m-auto w-75">
					{pub.map((x, i) => (
						<PublicationCom
							pub={x}
							key={i}
							like={() => like(x.publicationIdentifier)}
							dislike={() => dislike(x.publicationIdentifier)}
							edit={true}
							del={() => rem(x.publicationIdentifier)}
						/>
					))}
				</div>
			</div>
			<Pagination
				page={page}
				setPage={(p: number) => setPage(p)}
				maxPages={maxpg}
			/>
		</>
	);
}
