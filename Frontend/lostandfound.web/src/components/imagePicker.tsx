import { useState } from "react";
import { AiFillDelete } from "react-icons/ai";
import { FiChevronsRight, FiSave, FiTrash, FiTrash2 } from "react-icons/fi";

export default function UploadAndDisplayImage({
	currentImg,
	onSave,
	onDelete,
}: {
	currentImg: string | undefined;
	onSave: (file: File) => void;
	onDelete: () => void;
}) {
	const [selectedImage, setSelectedImage] = useState(null as File | null);
	return (
		<>
			<div className="row container justify-content-start">
				<div className="col-3 pt-2">Zdjęcie:</div>
				<div className="col-7">
					<input
						className="form-control mb-3"
						type="file"
						name="test"
						onChange={(e) => {
							if (e?.target?.files) {
								setSelectedImage(e.target.files[0]);
								onSave(e.target.files[0]);
							}
						}}
					/>
					<div className="row">
						{currentImg && (
							<div className="text-center col-6">
								<img
									className="m-auto img-fluid rounded-5 d-block"
									style={{ width: "200px" }}
									src={currentImg}
								></img>

								<button
									className="btn btn-danger m-3"
									onClick={() => onDelete()}
								>
									<AiFillDelete />
								</button>
							</div>
						)}

						{selectedImage && (
							<div className="text-center col-6">
								<img
									className="m-auto img-fluid rounded-5 d-block"
									alt="not found"
									width={"200px"}
									src={URL.createObjectURL(selectedImage)}
								/>
							</div>
						)}
					</div>
				</div>
			</div>
		</>
	);
}
