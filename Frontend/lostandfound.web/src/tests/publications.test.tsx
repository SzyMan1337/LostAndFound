import { render, screen } from "@testing-library/react";
import { Publication, PublicationCom } from "components/publications/publicationsList";

test("renders publications title", () => {
	var pub = new Publication();
	pub.title = "test title";
	render(<PublicationCom pub={pub} />);
	const titleElement = screen.getByText(/test title/i);
	expect(titleElement).toBeInTheDocument();
});

test("renders publications address", () => {
	var pub = new Publication();
	pub.incidentAddress = "incident address";
	render(<PublicationCom pub={pub} />);
	const incidentElement = screen.getByText(/incident address/i);
	expect(incidentElement).toBeInTheDocument();
});

test("renders publications description", () => {
	var pub = new Publication();
	pub.description = "desc";
	render(<PublicationCom pub={pub} />);
	const desc = screen.getByText(/desc/i);
	expect(desc).toBeInTheDocument();
});

test("renders publications rating", () => {
	var pub = new Publication();
	pub.rating = 223;
	render(<PublicationCom pub={pub} />);
	const rat = screen.getByText(/223/i);
	expect(rat).toBeInTheDocument();
});
