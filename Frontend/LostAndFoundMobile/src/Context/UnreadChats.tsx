import { getUnreadNotifications } from 'commons';
import { getAccessToken } from '../SecureStorage';

export const setUnreadChatsCount = async (
  updateUnreadChatsCount: (arg: number) => void,
) => {
  const accessToken = await getAccessToken();
  if (accessToken) {
    const unreadChatNotifications = await getUnreadNotifications(accessToken);
    if (unreadChatNotifications)
      updateUnreadChatsCount(unreadChatNotifications.unreadChatsCount);
  }
};
