import Chats from "components/chat/chats";
import Profile, { ProfileOther } from "components/profile/profile";
import ProfileEdit from "components/profile/profileEdit";
import PwdChange from "components/profile/pwdChange";
import { EditPublication } from "components/publications/editPublication";
import MyPublications from "components/publications/myPublications";
import PublicationsList from "components/publications/publicationsList";
import Landing from "components/register/landing";
import Login from "components/register/login";
import Register from "components/register/register";
import { Navigate, Route, Routes } from "react-router-dom";
import Auth from "./auth";
import NoAuth from "./noauth";
import Root from "./root";

export default function MainRouter() {
	return (
		<Routes>
			<Route path="/" element={<Root></Root>}>
				<Route path="/" index element={<Landing></Landing>} />
				<Route element={<Auth></Auth>}>
					<Route path="profile">
						<Route index element={<Profile />} />
						<Route path="edit" element={<ProfileEdit />} />
						<Route path="password" element={<PwdChange />} />
						<Route
							path=":userId"
							element={<ProfileOther />}
						></Route>
					</Route>
					<Route
						path="posts/edit/:pubId"
						element={<EditPublication />}
					/>
					<Route path="posts" element={<PublicationsList />} />
					<Route path="posts/mine" element={<MyPublications />} />
					<Route path="chats" element={<Chats />} />
					<Route path="chats/:userId" element={<Chats />} />
				</Route>

				<Route element={<NoAuth></NoAuth>}>
					<Route path="login" element={<Login />} />
					<Route path="register" element={<Register />} />
				</Route>
				<Route path="*" element={<Navigate to={""} />} />
			</Route>
		</Routes>
	);
}
