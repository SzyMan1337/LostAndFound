import { createContext } from "react";

export class ChatContextType {
	newMsg: boolean = false;
	setMewMsg: (msg: boolean) => void = () => {};
	newMsgCount: number = 0;
	readMsg: () => void = () => {};
}

export const chatContext = createContext(new ChatContextType());
