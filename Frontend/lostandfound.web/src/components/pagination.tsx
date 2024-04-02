import { FiChevronLeft, FiChevronRight } from "react-icons/fi";

export default function Pagination({
	page,
	setPage,
	maxPages,
}: {
	page: number;
	setPage: (newPage: number) => void;
	maxPages: number;
}) {
	function setPageIfCan(page: number) {
		if (page > 0 && page <= maxPages) setPage(page);
	}
	return (
		<div className="">
			{page - 1 > 0 && (
				<button
					className="btn btn-primary"
					onClick={() => setPageIfCan(page - 1)}
				>
					<FiChevronLeft />
				</button>
			)}
			<div className="mx-2 btn-group">
				{page - 1 > 0 && (
					<button
						className="btn btn-primary"
						onClick={() => setPageIfCan(page - 1)}
					>
						{page - 1}
					</button>
				)}
				<button className="btn btn-primary active">{page}</button>
				{page + 1 <= maxPages && (
					<button
						className="btn btn-primary"
						onClick={() => setPageIfCan(page + 1)}
					>
						{page + 1}
					</button>
				)}
			</div>
			{page + 1 <= maxPages && (
				<button
					className="btn btn-primary"
					onClick={() => setPageIfCan(page + 1)}
				>
					<FiChevronRight />
				</button>
			)}
		</div>
	);
}
