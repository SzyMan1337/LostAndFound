import { MessageResponseType } from 'commons';
import React from 'react';

export const AuthContext = React.createContext({
  signIn: async () => {},
  signOut: async () => {},
});

let chatMessage: MessageResponseType | undefined;
export const ProfileContext = React.createContext({
  updatePhotoUrl: async () => {},
  updatePhotoUrlValue: false,
  updateChats: async () => {},
  updateChatsValue: false,
  updateUnreadChatsCount: async () => {},
  unreadChatsCount: 0,
  chatMessage,
});
