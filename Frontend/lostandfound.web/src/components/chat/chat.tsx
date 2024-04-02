import { chatContext } from "chatContext";
import {
	addChatMessage,
	BaseProfileType,
	ChatBaseResponseType,
	getBaseProfiles,
	getChatMessages,
	MessageResponseType,
	readChat,
} from "commons";
import { useContext, useEffect, useRef, useState } from "react";
import { userContext } from "userContext";

export default function Chat({
	userId,
	refr,
	setRef,
}: {
	userId: string | undefined;
	refr: boolean;
	setRef: (newref: boolean) => void;
}) {
	const [msg, setMsg] = useState(
		undefined as MessageResponseType[] | undefined
	);
	const [newmsg, setNewmsg] = useState("");
	const usrCtx = useContext(userContext);
	const chatCtx = useContext(chatContext);
	const [user, setUser] = useState(undefined as BaseProfileType | undefined);
	useEffect(() => {
		if (userId && refr)
			getChatMessages(userId, usrCtx.user.authToken ?? "")
				.then((x) => {
					setMsg(x.messages.reverse());
					return x;
				})
				.then((x) => setRef(false));
	}, [usrCtx.user, userId, user, refr]);

	useEffect(() => {
		if (userId)
			getBaseProfiles([userId], usrCtx.user.authToken ?? "").then((x) =>
				setUser(x?.at(0))
			);
	}, [userId]);

	function Send() {
		if (userId)
			addChatMessage(
				userId,
				{ content: newmsg },
				usrCtx.user.authToken ?? ""
			).then((x) => {
				if (msg && x) setMsg([...msg, x]);
				setRef(true);
				setNewmsg("");
			});
	}

	const divRef = useRef<HTMLDivElement>(null);

	useEffect(() => {
		if (divRef?.current)
			divRef.current.scrollIntoView({ behavior: "auto" });
	});

	if (!userId) return <div></div>;
	const handleKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
		if (e.key === "Enter") {
			Send();
		}
	};
	return (
		<div className=" ms-auto col-7">
			<div className="p-3 bg-light border border-dark rounded-5">
				<div className="h4 text-start mx-2 border-bottom border-dark border-1">
					{user?.username}
				</div>
				<div className="overflow-scroll p-3" style={{ height: "80vh" }}>
					{msg?.map((x) => (
						<Message msg={x} isMe={x.authorId !== user?.userId} />
					))}
					<div ref={divRef}></div>
				</div>
				<div className="d-flex">
					<input
						value={newmsg}
						className="form-control rounded-5"
						type="text"
						onChange={(e) => setNewmsg(e.target.value)}
						onKeyDown={(e) => handleKeyDown(e)}
					></input>
					<button
						className="btn btn-primary ms-1 rounded-5"
						onClick={() => Send()}
					>
						Wyślij
					</button>
				</div>
			</div>
		</div>
	);
}

function Message({ msg, isMe }: { msg: MessageResponseType; isMe: boolean }) {
	return (
		<div className="d-flex m-1 mx-2">
			<div
				className={isMe ? " ms-auto " : ""}
				style={{ maxWidth: "70%" }}
			>
				<div
					className={"" + (isMe ? " text-end " : " text-start")}
					style={{ fontSize: "12px" }}
				>
					{formatTime(msg.creationTime)}
				</div>
				<div
					className={
						"text-start border border-dark p-2 rounded-4" +
						(isMe ? " ms-auto " : " me-auto")
					}
					style={{ width: "fit-content" }}
				>
					{msg.content}
				</div>
			</div>
		</div>
	);
}
function formatTime(date: Date) {
	date = new Date(date);
	return (
		date.getMonth() +
		1 +
		"-" +
		date.getDate() +
		" " +
		date.getHours() +
		":" +
		(date.getMinutes() > 9 ? date.getMinutes() : "0" + date.getMinutes())
	);
}
