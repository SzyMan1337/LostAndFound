export interface Chat {
  _id: string;
  chatMemberId: string;
  username: string;
  lastMessage: Message;
  unreadMessages: boolean;
}

export interface Message {
  userId: string;
  content: string;
  sentDateTime: Date;
}

export const GetChats = () => {
  const chats: Chat[] = [
    {
      _id: '1',
      chatMemberId: '1',
      username: 'Rafał',
      lastMessage: {
        userId: '1',
        content:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.',
        sentDateTime: new Date(),
      },
      unreadMessages: true,
    },
    {
      _id: '2',
      chatMemberId: '2',
      username: 'Nazwa użytkownika',
      lastMessage: {
        userId: '2',
        content: 'Lorem ipsum.',
        sentDateTime: new Date(),
      },
      unreadMessages: false,
    },
  ];

  return chats;
};

export const GetMessages = () => {
  const messages: Message[] = [
    {
      userId: '1',
      content: `Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.`,
      sentDateTime: new Date(),
    },
    {
      userId: '2',
      content: `Lorem ipsum dolor sit amet.`,
      sentDateTime: new Date(),
    },
    {
      userId: '2',
      content: `Lorem.`,
      sentDateTime: new Date(),
    },
    {
      userId: '1',
      content: `Lorem ipsum dolor sit amet, 
consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.`,
      sentDateTime: new Date(),
    },
    {
      userId: '1',
      content: `Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.`,
      sentDateTime: new Date(),
    },
    {
      userId: '2',
      content: `Lorem ipsum dolor sit amet.`,
      sentDateTime: new Date(),
    },
    {
      userId: '2',
      content: `Lorem.`,
      sentDateTime: new Date(),
    },
    {
      userId: '1',
      content: `Lorem ipsum dolor sit amet, 
consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.`,
      sentDateTime: new Date(),
    },
  ];

  return messages;
};
