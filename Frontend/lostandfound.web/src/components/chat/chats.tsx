import { chatContext } from "chatContext";
import {
	addChatMessage,
	BaseProfileType,
	ChatBaseResponseType,
	getBaseProfiles,
	getChats,
	getProfileDetails,
	readChat,
} from "commons";
import { useContext, useEffect, useState } from "react";
import { AiFillExclamationCircle } from "react-icons/ai";
import { FiBell } from "react-icons/fi";
import { useNavigate, useParams } from "react-router-dom";
import { userContext } from "userContext";
import Chat from "./chat";

export default function Chats() {
	var { userId } = useParams();

	const usrCtx = useContext(userContext);
	const [chats, setChats] = useState(
		undefined as ChatBaseResponseType[] | undefined
	);
	const [users, setUsers] = useState(
		undefined as BaseProfileType[] | undefined
	);
	const chatCtx = useContext(chatContext);
	const [refCur, setRefCur] = useState(false);
	const [refList, setRefList] = useState(false);

	useEffect(() => {
		getChats(usrCtx.user.authToken ?? "", 1)
			.then((x) => {
				if (x === undefined) {
					usrCtx.setUser({ ...usrCtx.user, isLogged: false });
				}
				setChats(x.chats);
			})
			.then((x) => {
				setRefList(false);
			});
	}, [chatCtx.newMsg, refList]);

	useEffect(() => {
		if (chatCtx.newMsg == true) {
			chatCtx.setMewMsg(false);
			setRefCur(true);
			setRefList(true);
		}
	}, [chatCtx.newMsg]);

	useEffect(() => {
		setRefCur(true);
		setRefList(true);
	}, [userId]);

	useEffect(() => {
		if (chats)
			getBaseProfiles(
				chats.map((x) => x.chatMember.id),
				usrCtx.user.authToken ?? ""
			).then((x) => setUsers(x));
	}, [chats]);

	useEffect(() => {
		if (userId) {
			readChat(userId, usrCtx.user.authToken ?? "").then((x) =>
				chatCtx.readMsg()
			);
		}
	}, [userId, usrCtx]);

	useEffect(() => {
		setRefList(true);
	}, [refCur]);

	return (
		<div className=" mx-5 container row" style={{ height: "90vh" }}>
			<ChatsList
				chats={chats}
				users={users}
				refr={refList}
				setRef={setRefList}
			/>
			<Chat userId={userId} refr={refCur} setRef={setRefCur} />
		</div>
	);
}

function ChatsList({
	chats,
	users,
	refr,
	setRef,
}: {
	chats: ChatBaseResponseType[] | undefined;
	users: BaseProfileType[] | undefined;
	refr: boolean;
	setRef: (x: boolean) => void;
}) {
	var { userId } = useParams();
	return (
		<div
			className="col-4 border border-dark bg-light  p-3 rounded-5 text-start"
			style={{ height: "90vh" }}
		>
			<div style={{ height: "100%", overflow: "scroll" }}>
				<span className="h5 text-start me-auto">Czaty:</span>{" "}
				{users &&
					chats?.map((x, i) => (
						<ChatRow
							chat={x}
							user={users.find(
								(y) => y.userId == x.chatMember.id
							)}
							isSelected={x.chatMember.id === userId}
						></ChatRow>
					))}
			</div>
		</div>
	);
}

function ChatRow({
	chat,
	user,
	isSelected,
}: {
	chat: ChatBaseResponseType;
	user: BaseProfileType | undefined;
	isSelected: boolean;
}) {
	const nav = useNavigate();

	return (
		<div
			className={"border border-dark rounded-5 container mt-2 row py-1 mx-auto ".concat(
				isSelected ? " border-1" : "border-0"
			)}
			onClick={() => nav(`/chats/${chat.chatMember.id}`)}
		>
			<div className="col-3 text-center">
				<img
					className="rounded-5"
					style={{ width: "100%" }}
					src={
						user?.pictureUrl ??
						"https://avatars.dicebear.com/api/bottts/stefan.svg"
					}
				/>
			</div>
			<div className="col">
				<div className="d-flex">
					<div className="me-auto">{user?.username}</div>

					<div className=" fst-italic ms-auto">
						<span>{formatTime(chat.lastMessage.creationTime)}</span>{" "}
					</div>
				</div>
				<div className="d-flex mt-1">
					<div className="me-auto fst-italic">
						{chat.lastMessage.content}
					</div>
					{chat.containsUnreadMessage && <FiBell />}
				</div>
			</div>
		</div>
	);
}
function formatTime(date: Date) {
	date = new Date(date);
	return (
		date.getFullYear() +
		"-" +
		(date.getMonth() + 1) +
		"-" +
		date.getDate() +
		" " +
		date.getHours() +
		":" +
		(date.getMinutes() > 9 ? date.getMinutes() : "0" + date.getMinutes())
	);
}
