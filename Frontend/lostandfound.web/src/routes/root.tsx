import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import {
	getUnreadNotifications,
	MessageResponseType,
	refreshToken,
} from "commons";
import Navbar from "components/navbar";
import Relog from "components/register/relog";
import { useEffect, useState } from "react";
import { Outlet } from "react-router-dom";
import { userContext, UsrCont } from "userContext";
import { chatContext } from "chatContext";

export default function Root() {
	const [user, setUser] = useState(new UsrCont());
	const [loading, setIsLoading] = useState(true);

	const [connection, setConnection] = useState<HubConnection>();
	const [newMsg, setNewMsg] = useState(false);
	const [newMsgCount, setNewMsgCount] = useState(0);

	useEffect(() => {
		var token = localStorage.getItem("accessToken");
		var reftoken = localStorage.getItem("refreshToken");
		var date = localStorage.getItem("expiration");

		if (token)
			setUser({
				authToken: token,
				isLogged: token && reftoken ? true : false,
				refreshToken: reftoken,
				expirationDate: date ? new Date(date) : null,
			});
		setIsLoading(false);
	}, []);

	useEffect(() => {
		if (user.authToken !== null)
			localStorage.setItem("accessToken", user.authToken);
		else {
			localStorage.removeItem("accessToken");
		}
		if (user.refreshToken !== null)
			localStorage.setItem("refreshToken", user.refreshToken);
		else {
			localStorage.removeItem("refreshToken");
		}
		if (user.expirationDate !== null)
			localStorage.setItem(
				"expiration",
				user.expirationDate?.toISOString()
			);
		else {
			localStorage.removeItem("expiration");
		}
	}, [user]);

	useEffect(() => {
		if (user.refreshToken) {
			if (
				!user.isLogged ||
				(user.expirationDate?.getTime() ?? Number.MIN_SAFE_INTEGER) <
					Date.now()
			)
				refreshToken(user.refreshToken).then((x) => {
					if (x)
						setUser({
							authToken: x?.accessToken,
							expirationDate: x?.accessTokenExpirationTime,
							isLogged: true,
							refreshToken: x.refreshToken,
						});
					else {
						setUser({
							authToken: null,
							expirationDate: null,
							isLogged: false,
							refreshToken: null,
						});
					}
				});
		}
	}, [user]);

	useEffect(() => {
		const connectToSocket = async () => {
			const accessToken = user.authToken;
			if (
				accessToken &&
				user.isLogged &&
				(!connection || connection.state === "Disconnected")
			) {
				setConnection(
					new HubConnectionBuilder()
						.withUrl(
							`${process.env["REACT_APP_API_GATEWAY_URL"]}/hubs/chat`,
							{
								accessTokenFactory: () => accessToken,
							}
						)
						.withAutomaticReconnect()
						.build()
				);
			} else if (connection) {
				await connection.stop();
				setConnection(undefined);
			}
		};

		connectToSocket();
	}, [user.authToken, connection?.state]);

	useEffect(() => {
		if (connection?.state === "Disconnected") {
			connection.on(
				"ReceiveMessage",
				async (data: MessageResponseType) => {
					setNewMsg(true);
					getUnread();
				}
			);
			connection.start();
		}
	}, [connection]);

	useEffect(() => {
		if (user.authToken && newMsg == true) getUnread();
	}, [newMsg]);

	useEffect(() => {
		if (user.authToken) getUnread();
	}, [user]);

	async function getUnread() {
		await new Promise((x) => setTimeout(x, 200));
		getUnreadNotifications(user.authToken ?? "").then((x) => {
			if (x) setNewMsgCount(x.unreadChatsCount);
		});
	}

	if (loading === true) return <div>...</div>;

	return (
		<userContext.Provider
			value={{ user: user, setUser: (arg: UsrCont) => setUser(arg) }}
		>
			<chatContext.Provider
				value={{
					newMsg: newMsg,
					setMewMsg: (newMsg) => setNewMsg(newMsg),
					newMsgCount: newMsgCount,
					readMsg: () => {
						if (user.authToken)
							getUnreadNotifications(user.authToken).then((x) => {
								if (x) setNewMsgCount(x.unreadChatsCount);
							});
					},
				}}
			>
				<Navbar></Navbar>
				<Outlet></Outlet>
			</chatContext.Provider>
		</userContext.Provider>
	);
}
